using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;
using System.Reflection;
using University.API.Code.Filters;

namespace University.API.Code.Extensions
{
    public static class SwaggerExtension
    {
        public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                o.SwaggerDoc(
                    "v1.0",
                    new OpenApiInfo
                    {
                        Title = "University API",
                        Version = "1.0"
                    });
                o.SwaggerDoc(
                     "v1.1",
                     new OpenApiInfo
                     {
                         Title = "University API",
                         Version = "1.1"
                     });

                o.OperationFilter<RemoveVersionParameterFilter>();
                o.DocumentFilter<ReplaceVersionWithExactValueInPathFilter>();

                o.DocInclusionPredicate((version, apiDesc) =>
                {
                    if (!apiDesc.TryGetMethodInfo(out MethodInfo methodInfo)) return false;

                    var versions = methodInfo
                        .DeclaringType
                        .GetCustomAttributes(true)
                        .OfType<ApiVersionAttribute>()
                        .SelectMany(attr => attr.Versions);

                    return versions.Any(v => $"v{v}" == version);
                });

                o.EnableAnnotations();
            });

            return services;
        }
    }
}
