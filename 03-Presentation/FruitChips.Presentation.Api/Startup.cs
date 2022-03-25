using Utilities;
using Core.Contracts;
using Serilog.Events;
using Presentation.Api;
using System.Reflection;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FruitChips.Persistance.SqlData;
using FruitChips.Presentation.Api.Identity;
using FruitChips.Persistance.SqlData.Context;
using FruitChips.Persistance.Identity.SqlData;
using FruitChips.Core.Domain.Identity.Entities;
using FruitChips.Core.Domain.Customers.Entities;
using Presentation.Api.Middlewares.ExceptionHandling;
using System.Xml.Linq;
using Persistance.SqlData.Context;

public class Startup
{


    public Startup(IConfiguration configuration,IHostEnvironment environment)
    {
        Configuration = configuration;
        Environment = environment;
    }

    public IConfiguration Configuration { get; }
    public IHostEnvironment Environment { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        var assemblies = AssemblyLoad(Environment);
        var setting = Configuration.GetSection("AppSettings").Get<AppSettings>();
        services
        .AddBaseServices(new List<Assembly> { Assembly.Load("FruitChips.Core.Application") })

        .AddDbContext<CommandDbContext>(config =>
        {
            //config.UseInMemoryDatabase("DataBase");
            config.UseSqlServer(Configuration.GetConnectionString("cnn"));
        })
        .AddDbContext<QueryDbContext>(config =>
        {
            //config.UseInMemoryDatabase("DataBase");
            config.UseSqlServer(Configuration.GetConnectionString("cnn"));
        })
        .AddPersistanceServices()
        .AddEndpointsApiExplorer()
        .AddJwtAuthentication(setting.Jwt)
        .AddIdentityDbContext(Configuration)
        .AddSingleton(Configuration.GetSection("AppSettings").Get<AppSettings>())
        .AddControllers(option =>
        {
            //option.Filters.Add(new AuthorizeFilter());
        }).AddCustomFluentValidation(assemblies);
        services.Scan(s => s.FromAssemblies(AssemblyLoad(Environment))
            .AddClasses(classes => classes.Where(type => typeof(IScopeLifeTime).IsAssignableFrom(type)))
            .AsImplementedInterfaces()
            .WithScopedLifetime());


    }

    public static IList<Assembly> AssemblyLoad(IHostEnvironment env)
    {
        IList<Assembly> assemblies = new List<Assembly>();
        string fileName = @$"{env.ContentRootPath}\{env.ApplicationName}.csproj";
        XDocument projDefinition = XDocument.Load(fileName);
        var projectReferences = projDefinition
            .Element("Project")
            .Elements("ItemGroup")
            .Elements("ProjectReference");
        foreach (var v in projectReferences)
        {
            var includeValue = v.Attribute("Include").Value.ToString();
            var startSubstringIndex = includeValue.LastIndexOf(@"\");
            var projectName = includeValue.Substring(startSubstringIndex + 1, includeValue.Length - (startSubstringIndex + 1));
            var assemblyName = projectName.Replace(".csproj", "");
            assemblies.Add(Assembly.Load(assemblyName));
        }
        return assemblies;
    }
    public void Configure(
        IApplicationBuilder app,
        IHostEnvironment hostEnvironment,
        UserManager<Customer> userManager,
        RoleManager<AppRole> roleManeger
        )
    {

        var q = hostEnvironment.ContentRootPath;
        if (hostEnvironment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseApiExceptionHandler(options =>
        {
            options.AddResponseDetails = (context, ex, error) =>
            {
                if (ex.GetType().Name == typeof(SqlException).Name)
                {
                    error.Detail = "Exception was a database exception!";
                }
            };
            options.DetermineLogLevel = ex =>
            {
                if (ex.Message.StartsWith("cannot open database", StringComparison.InvariantCultureIgnoreCase) ||
                    ex.Message.StartsWith("a network-related", StringComparison.InvariantCultureIgnoreCase))
                {
                    return LogEventLevel.Warning;
                }
                return LogEventLevel.Error;
            };
        });
        ApplicationDbInitializer.SeedAdminUser(userManager, roleManeger);
        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });


    }
}