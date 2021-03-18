
using System;
using System.IO;
using System.Reflection;
using System.Text;
using AutoMapper;
using Web.API.Dotz.Data;
using Web.API.Dotz.Data.RepoUser;
using Web.API.Dotz.Helpers;
using Web.API.Dotz.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using Web.API.Dotz.Data.RepoProduct;
using Web.API.Dotz.Data.RepoUserAddress;
using Web.API.Dotz.Data.RepoUserDotz;
using Web.API.Dotz.Data.RepoOrder;
using Web.API.Dotz.Data.RepoOrderItems;

namespace Web.API.Dotz
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //configuracao banco de dados 
            services.AddDbContext<DotzContext>(
               context => context.UseMySql(Configuration.GetConnectionString("MySqlConnection"))
           );

            services.AddControllers().AddNewtonsoftJson(
                        opt => opt.SerializerSettings.ReferenceLoopHandling =
                            Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
            });

            var apiProviderDescription = services.BuildServiceProvider()
                                                 .GetService<IApiVersionDescriptionProvider>();

            services.AddSwaggerGen(options =>
            {
                foreach (var description in apiProviderDescription.ApiVersionDescriptions)
                {
                    options.SwaggerDoc(
                        description.GroupName,
                        new Microsoft.OpenApi.Models.OpenApiInfo()
                        {
                            Title = "Dotz API",
                            Version = description.ApiVersion.ToString(),
                            TermsOfService = new Uri("http://com.br"),
                            Description = "Dotz API",
                            License = new Microsoft.OpenApi.Models.OpenApiLicense
                            {
                                Name = "Dotz License",
                                Url = new Uri("http://com.br")
                            },
                            Contact = new Microsoft.OpenApi.Models.OpenApiContact
                            {
                                Name = "Anderson Oliveira",
                                Email = "",
                                Url = new Uri("http://com.br")
                            }
                        }
                    );

                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type = SecuritySchemeType.ApiKey,
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Description = "JWT Autenticação usando Bearer. Obs: Usar Bearer(com espaço) antes do token"
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
                                Scheme = "oauth3",
                                Name = "Bearer",
                                In = ParameterLocation.Header,
                            },
                            Array.Empty<string>()
                        }
                    });


                }

                var xmlCommentsFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentsFullPath = Path.Combine(AppContext.BaseDirectory, xmlCommentsFile);

                options.IncludeXmlComments(xmlCommentsFullPath);
            });

            // definir objetos de configurações fortemente tipados
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // configurar autenticação jwt
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // configurar DI para serviços de aplicativo
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IRepositoryUser, RepositoryUser>();
            services.AddScoped<IRepositoryProduct, RepositoryProduct>();
            services.AddScoped<IRepositoryUserAddress, RepositoryUserAddress>();
            services.AddScoped<IRepositoryUserDotz, RepositoryUserDotz>();
            services.AddScoped<IRepositoryOrder, RepositoryOrder>();
            services.AddScoped<IRepositoryOrderItems, RepositoryOrderItems>();

            services.AddCors();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiProviderDescription)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            app.UseSwagger()
           .UseSwaggerUI(options =>
           {
               foreach (var description in apiProviderDescription.ApiVersionDescriptions)
               {
                   options.SwaggerEndpoint(
                           $"/swagger/{description.GroupName}/swagger.json",
                           description.GroupName.ToUpperInvariant());
               }
               options.RoutePrefix = "";
           });

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
