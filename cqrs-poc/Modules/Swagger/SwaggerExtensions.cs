using Microsoft.OpenApi.Models;

namespace cqrs_poc.Modules.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "Swagger UI Personalized", 
                    Version = "Version .Net 6.0",
                    Description = "Thanks for sharing.",
                    TermsOfService = new Uri("https://cvmendozacr.com"),
                    Contact = new OpenApiContact
                    {
                        Name = "Omri Shapira",
                        Email = "omri.shapira@gmail.com"
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT",
                        Url = new Uri("https://opensource.org/licenses/MIT")
                    }
                });

                foreach (var name in Directory.GetFiles(AppContext.BaseDirectory, ".XML", SearchOption.TopDirectoryOnly))
                {
                    c.IncludeXmlComments(name);
                }
            });
            
            return services;
        }
    }
}
