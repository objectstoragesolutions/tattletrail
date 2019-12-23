using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Text;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.EmailService;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Infrastructure.MonitorsStatusService;
using TattleTrail.Infrastructure.MonitorStatusNotifyer;
using TattleTrail.Models;

namespace TattleTrail {
    public class Startup {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
            Configuration = configuration;
            CurrentEnvironment = env;
        }

        public IConfiguration Configuration { get; }
        private IWebHostEnvironment CurrentEnvironment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            String connectionString = String.Empty;
            var configurationOptions = new ConfigurationOptions {
                AbortOnConnectFail = false,
                ConnectRetry = 2,
                ConnectTimeout = 5000,
                SyncTimeout = 5000
            };

            connectionString = Configuration["REDIS_URL"];
            configurationOptions.EndPoints.Add(connectionString);

            var sharedKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(Configuration["SIGNING_KEY"]));

            services.AddScoped<IMonitorModelFactory, MonitorModelFactory>();
            services.AddScoped<IMonitorDetailsFactory, MonitorDetailsFactory>();
            services.AddSingleton<ICheckInModelFactory, CheckInModelFactory>();
            services.AddSingleton<IMonitorStatusNotifyer, MonitorStatusNotifyer>();
            services.AddSingleton<ICheckInRepository<CheckIn>, CheckInRepository>();
            services.AddSingleton<IMonitorRepository<MonitorProcess>, MonitorRepository>();
            services.AddSingleton<IRedisServerProvider, RedisServerProvider>();
            services.AddSingleton<IHostedService, MonitorsStatusService>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IConfiguration>(Configuration);
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(configurationOptions));
            services.AddAuthentication(x => { 
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;}
            ).AddJwtBearer(options => {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters {
                    IssuerSigningKey = sharedKey,
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidAudience = Configuration["VALID_AUDIENCE"],
                    ValidIssuer = Configuration["VALID_ISSUER"],
                    ValidateAudience = true,
                    RequireExpirationTime = false
                };
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app) {
            if (CurrentEnvironment.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
