using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace WebNet6.Swagger
{
    public class SwaggerVersion : IConfigureNamedOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public SwaggerVersion(
            IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.OperationFilter<ApiVersionFilter>();
            foreach (var description in provider.ApiVersionDescriptions)
            {
                var info = new OpenApiInfo()
                {
                    Version = description.ApiVersion.ToString(),
                    Title = "Swagger",
                    Description = $"A simple example ASP.NET Core Web API"
                };
                options.SwaggerDoc(description.GroupName, info);

            }
        }

        public void Configure(string name, SwaggerGenOptions options)
        {
            Configure(options);
        }

        private class ApiVersionFilter : IOperationFilter
        {
            public void Apply(OpenApiOperation operation, OperationFilterContext context)
            {
                operation.Parameters = operation.Parameters.Where(x => x.Name != "api-version").ToList();
            }
        }
    }
}
