using System;
using System.IO;
using System.Linq;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace OasysNet.Api.Configurations
{
    //TODO: Move to SDK
    public static class SwaggerConfiguration
    {
        public static void AddSwagger(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(options =>
            {
                options.OperationFilter<SwaggerDefaultValues>();
                options.OperationFilter<ResponseContentTypeOperationFilter>();

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Insert the JWT this way: Bearer {your token}",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header
                        },
                        new string[] {}
                    }
                });

                options.CustomSchemaIds(x => x.FullName);
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });

            services.AddSwaggerGenNewtonsoftSupport();
        }

        public static void UseSwaggerApi(this IApplicationBuilder app)
        {
            var environment = app.ApplicationServices.GetRequiredService<IWebHostEnvironment>();
            if (environment.IsProduction())
                return;

            var configuration = app.ApplicationServices.GetRequiredService<IConfiguration>();
            var provider = app.ApplicationServices.GetRequiredService<IApiVersionDescriptionProvider>();

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                foreach (var description in provider.ApiVersionDescriptions)
                    options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName);

                options.DocExpansion(DocExpansion.None);
            });
        }
    }

    public class SwaggerDefaultValues : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var apiDescription = context.ApiDescription;
            operation.Deprecated = apiDescription.IsDeprecated();
            if (operation.Parameters is null)
                return;

            foreach (var parameter in operation.Parameters)
            {
                var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);
                if (parameter.Description is null)
                    parameter.Description = description.ModelMetadata?.Description;
                parameter.Required |= description.IsRequired;
            }
        }
    }

    public class ResponseContentTypeOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            if (operation.Responses != null)
            {
                foreach (var response in operation.Responses)
                    response.Value.Content = response.Value.Content.OrderBy(c => c.Key).ToDictionary(k => k.Key, v => v.Value);
            }
        }
    }

    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;
        private readonly IWebHostEnvironment _environment;
        private readonly IConfiguration _configuration;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider, IWebHostEnvironment environment, IConfiguration configuration)
        {
            _provider = provider;
            _environment = environment;
            _configuration = configuration;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in _provider.ApiVersionDescriptions)
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        private OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var apiInfo = _configuration.GetSection(nameof(ApiInfo)).Get<ApiInfo>();

            var info = new OpenApiInfo
            {
                Title = $"{apiInfo.Title} - {_environment.EnvironmentName}",
                Version = description.ApiVersion.ToString(),
                Description = apiInfo.Description,
                Contact = new OpenApiContact { Name = apiInfo.Contact.Name, Email = apiInfo.Contact.Email }
            };

            if (description.IsDeprecated)
                info.Description += "This version is deprecated";

            return info;
        }
    }

    //TODO: Convert to Records in .NET5
    public partial class ApiInfo
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public Contact Contact { get; set; }
    }

    //TODO: Convert to Records in .NET5
    public partial class Contact
    {
        public string Name { get; set; }

        public string Email { get; set; }
    }
}