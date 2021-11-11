using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLOGN.SharedTools
{
     public class ConfigureSwaggerGenOptions : IConfigureOptions<SwaggerGenOptions>
     {
          public void Configure(SwaggerGenOptions options)
          {
               options.AddSecurityDefinition(
                    "Bearer", new OpenApiSecurityScheme
                    {
                         Description = "JWT Kimlik Doğrulama için bnu alanı kullana bilirsiniz. Bearer Ön ekinin girdikten sonra bir boşluk bırakaratk token girişi yapabilirsiniz . Örneğin - 'Bearer mytokenmytokenmytoken' ",
                         Name = "Authorization",
                         In = ParameterLocation.Header,
                         Type = SecuritySchemeType.ApiKey,
                         Scheme ="Bearer"
                    });

               options.AddSecurityRequirement(new OpenApiSecurityRequirement()
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
                              Name="Bearer",
                              In = ParameterLocation.Header,
                         },
                         new List<string>()
                    }
               });

               var xmlFileComment = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
               var xmlFullPath = Path.Combine(AppContext.BaseDirectory,xmlFileComment);
               options.IncludeXmlComments(xmlFullPath);


          }
     }
}
