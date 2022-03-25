using Swashbuckle.AspNetCore.SwaggerGen;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace FruitChips.Presentation.Api.Identity
{
    public class UnauthorizedResponsesOperationFilter : IOperationFilter
    {
        private readonly bool includeUnauthorizedAndForbiddenResponses;
        private readonly string schemeName;

        public UnauthorizedResponsesOperationFilter(bool includeUnauthorizedAndForbiddenResponses, string schemeName = "Bearer")
        {
            this.includeUnauthorizedAndForbiddenResponses = includeUnauthorizedAndForbiddenResponses;
            this.schemeName = schemeName;
        }

        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var filters = context.ApiDescription.ActionDescriptor.FilterDescriptors;

            var hasAnonymous = filters.Any(p => p.Filter is AllowAnonymousFilter);
            if (hasAnonymous) return;

            var hasAuthorize = filters.Any(p => p.Filter is AuthorizeFilter);
            if (!hasAuthorize) return;

            if (includeUnauthorizedAndForbiddenResponses)
            {
                operation.Responses.TryAdd("401", new OpenApiResponse { Description = "UnAuthorize" });
                operation.Responses.TryAdd("403", new OpenApiResponse { Description = "Forbidden" });
            }
            Dictionary<OpenApiSecurityScheme, IList<string>> pairs = new();
            operation.Security.Add(new OpenApiSecurityRequirement
            {
                 { new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = schemeName } },new List<string>() }
            });


        }
    }
}
