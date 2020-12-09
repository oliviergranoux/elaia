// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
using System.Text;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

// using Microsoft.AspNetCore.HttpsPolicy;
// using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using Elaia.Auth.Api.Services;
using Elaia.Auth.Api.Common.Services;
using Elaia.Auth.Api.Common.Interfaces;
using Elaia.Auth.Business.Interfaces;
using Elaia.Auth.Business.Managers;

namespace Elaia.Auth.Api
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
            // Transient services are created every time they are injected or requested.
            // Scoped services are created per scope. In a web application, every web request creates a new separated service scope. That means scoped services are generally created per web request.
            // Singleton services are created per DI container. That generally means that they are created only one time per application and then used for whole the application life time.

            // _logger.LogInformation($"Total Services Initially: {services.Count}");

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Auth.Api", Version = "v1" });
            });

            services.AddCors();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddHttpContextAccessor();
            services.AddTransient<IClaimsPrincipalService, ClaimsPrincipalService>();

            services.AddScoped<IUserManager, UserManager>();
           
            // var secret = Configuration.GetSection("AppSettings")["JwtSecret"];
            // var secret = "loremIpsum_loremIpsum_loremIpsum_loremIpsum_loremIpsum_loremIpsum_";

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key =  Encoding.ASCII.GetBytes(appSettings.JwtSecret);
            
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            if (env.IsDevelopment())
            {
                logger.LogInformation("In Development environment");
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Auth.Api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            /*
             * Encode: just UseAuthorization needed
             * Decode: UseAuthentication AND UseAuthorization are needed (order is important!) 
             */
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
