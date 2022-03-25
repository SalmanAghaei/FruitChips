using System.Reflection;
using Utilities.DapperService;
using Core.Application.Queries;
using Microsoft.OpenApi.Models;
using Core.Application.Commands;
using Microsoft.AspNetCore.Http;
using I6.Utilities.Services.Commands;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;

namespace Presentation.Api
{
    public static class EndpointExtensions
    {

        public static ICommandDispatcher CommandDispatcher(this HttpContext httpContext) =>
    (ICommandDispatcher)httpContext.RequestServices.GetService(typeof(ICommandDispatcher));

        public static IQueryDispatcher QueryDispatcher(this HttpContext httpContext) =>
    (IQueryDispatcher)httpContext.RequestServices.GetService(typeof(IQueryDispatcher));


        public static IServiceCollection AddBaseServices(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch)
             =>
            services
                .AddCommandDispatcherDecorators()
                .AddDapper()
                .AddQueryHandlers(assembliesForSearch)
                .AddCommandHandlers(assembliesForSearch)
                .AddSwagger();
        private static IServiceCollection AddQueryHandlers(this IServiceCollection services,
        IEnumerable<Assembly> assembliesForSearch) =>
        services.AddWithTransientLifetime(assembliesForSearch, typeof(IQueryHandler<,>), typeof(IQueryDispatcher));

        public static IServiceCollection AddSwagger(this IServiceCollection services) =>
            services.AddSwaggerGen(options =>
            {
                options.EnableAnnotations();
                //options.ExampleFilters();
                //options.OperationFilter<UnauthorizedResponsesOperationFilter>(true, "Bearer");
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                                new OpenApiSecurityScheme
                                    {
                                        Reference = new OpenApiReference
                                        {
                                            Type=ReferenceType.SecurityScheme,
                                            Id="Bearer"
                                        }
                                    },

                                new string[]{}
                        }
                    });
            });

        public static IServiceCollection AddCommandDispatcherDecorators(this IServiceCollection services)
        {
            services.AddTransient<CommandDispatcher, CommandDispatcher>();
            services.AddTransient<QueryDispatcher, QueryDispatcher>();
            services.AddTransient<IQueryDispatcher, QueryDispatcher>();
            services.AddTransient<CommandDispatcherDomainExceptionHandlerDecorator, CommandDispatcherDomainExceptionHandlerDecorator>();
            services.AddTransient<CommandDispatcherValidationDecorator, CommandDispatcherValidationDecorator>();
            services.AddTransient<ICommandDispatcher, CommandDispatcherValidationDecorator>();
            return services;
        }
        public static IServiceCollection AddCommandHandlers(this IServiceCollection services, IEnumerable<Assembly> assembliesForSearch) =>
                          services.AddWithTransientLifetime(assembliesForSearch, typeof(ICommandHandler<>), typeof(ICommandHandler<,>));
        private static IServiceCollection AddWithTransientLifetime(this IServiceCollection services,
            IEnumerable<Assembly> assembliesForSearch,
            params Type[] assignableTo)
        {
            services.Scan(s => s.FromAssemblies(assembliesForSearch)
                .AddClasses(c => c.AssignableToAny(assignableTo))
                .AsImplementedInterfaces()
                .WithTransientLifetime());

            return services;
        }

        public static IServiceCollection AddDapper(this IServiceCollection services) =>
            services.AddScoped<IDapper, Utilities.DapperService.Dapper>();


        public static IMvcBuilder AddCustomFluentValidation(this IMvcBuilder mvcBuilder, IList<Assembly> assemblies)
        {
            return mvcBuilder.AddFluentValidation(x=>x.RegisterValidatorsFromAssemblies(assemblies));
        }
        #region Bearer


        #endregion Bearer
    }
}
