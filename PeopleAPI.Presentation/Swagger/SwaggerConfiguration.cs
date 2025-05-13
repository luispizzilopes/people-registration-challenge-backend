using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace PeopleAPI.Presentation.Swagger;

public static class SwaggerConfiguration
{
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(options =>
        {
            options.CustomSchemaIds(type => type.FullName);

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "PeopleAPI - Version 1.0",
                Version = "v1",
                Description = "Documentação da versão 1.0 da PeopleAPI",
            });

            options.SwaggerDoc("v2", new OpenApiInfo
            {
                Title = "PeopleAPI - Version 2.0",
                Version = "v2",
                Description = "Documentação da versão 2.0 da PeopleAPI",
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Header de autorização JWT usando Bearer.\r\n\r\nInforme 'Bearer'[espaço] e o seu token.\r\n\r\nExamplo: \'Bearer 12345abcdef\'",
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
                        }
                    },
                    new string[] {}
                }
            });

            var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
            options.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);
        });

        services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        return services;
    }
}
