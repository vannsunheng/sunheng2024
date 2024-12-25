using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.OpenApi.Models;

namespace APIBackend.Extensions
{
    public static class SwaggerServiceExtentions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services){
               // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c=>{
                var securitySchema=new OpenApiSecurityScheme{
                    Description="JWT Auth Bearer Scheme",
                    Name="Authorisation",
                    In=ParameterLocation.Header,
                    Type=SecuritySchemeType.Http,
                    Scheme="Bearer",
                    Reference= new OpenApiReference{
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer",securitySchema);
                var securityrequirement=new OpenApiSecurityRequirement{
                    {
                        securitySchema, new[]  {"Bearer"}
                    }
                };
                c.AddSecurityRequirement(securityrequirement);
            });
            return services;
        } 
        public static IApplicationBuilder UseSwaggerDocumentation(this IApplicationBuilder app){
            // Configure the HTTP request pipeline.
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}