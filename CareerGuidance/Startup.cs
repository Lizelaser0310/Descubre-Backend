using System;
using System.Text;
using Algolia.Search.Clients;
using CareerGuidance.Models;
using Domain.Models;
using Lizelaser0310.Utilities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;

namespace CareerGuidance
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenKey = Encoding.ASCII.GetBytes(Configuration.GetValue<string>("TokenKey"));
            var encryptionKey =
                Convert.FromBase64String(Configuration.GetValue<string>("EncrytionKey"));
            var meiliKey = Configuration.GetValue<string>("MeiliKey");
            var algoliaKey = Configuration.GetValue<string>("AlgoliaKey");
            var smtpMail = Configuration.GetValue<string>("SmtpMail");
            var smtpPassword = Configuration.GetValue<string>("SmtpPassword");

            var emailCredentials = new EmailCredentials()
            {
                Email = smtpMail,
                Password = smtpPassword,
                Host = "smtp.zoho.com",
                Port = 465,
                UseSSL = true
            };
            var keys = new Keys(encryptionKey, tokenKey, meiliKey, algoliaKey);
            var algolia = new SearchClient("DL3V95VJEU", algoliaKey);

            services.AddSingleton<IKeys>(keys);
            services.AddSingleton(algolia);
            services.AddSingleton(emailCredentials);

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(jwt =>
            {
                jwt.RequireHttpsMetadata = false;
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(tokenKey),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddCors();
            services.AddControllers(options =>
            {
                var kebabConvention = new Utils.KebabCaseParameterTransformer();
                options.Conventions.Add(new RouteTokenTransformerConvention(kebabConvention));
                options.OutputFormatters.RemoveType<StringOutputFormatter>();
            }).AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ContractResolver =
                    new Utils.NonVirtualContractResolver();
                options.SerializerSettings.Converters.Add(new StringEnumConverter());
            });
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CareerGuidance", Version = "v1"});
            });

            services.AddDbContext<DescubreContext>(db =>
                db.UseNpgsql(Configuration.GetConnectionString("connectionDB")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "CareerGuidance v1"));
            }

            //app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(builder =>
                builder.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(_ => true)
                    .AllowCredentials());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}