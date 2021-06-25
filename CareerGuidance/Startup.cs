using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using CareerGuidance.Models;
using Lizelaser0310.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

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
            var encryptionKey = Convert.FromBase64String(Configuration.GetValue<string>("EncrytionKey"));
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
            var keys = new Keys(encryptionKey,tokenKey,meiliKey,algoliaKey);
            var algolia = new SearchClient("DL3V95VJEU",algoliaKey);
            
            services.AddSingleton<IKeys>(keys);
            services.AddSingleton(algolia);
            services.AddSingleton(emailCredentials);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "CareerGuidance", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CareerGuidance v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}