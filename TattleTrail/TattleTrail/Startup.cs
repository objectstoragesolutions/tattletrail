using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StackExchange.Redis;
using TattleTrail.DAL.RedisServerProvider;
using TattleTrail.DAL.Repository;
using TattleTrail.Infrastructure.EmailService;
using TattleTrail.Infrastructure.Factories;
using TattleTrail.Models;

namespace TattleTrail {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddScoped<IMonitorRepository<MonitorProcess>, MonitorRepository>();
            services.AddScoped<ICheckInRepository<CheckIn>, CheckInRepository>();
            services.AddScoped<IMonitorModelFactory, MonitorModelFactory>();
            services.AddScoped<IMonitorDetailsFactory, MonitorDetailsFactory>();
            services.AddScoped<ICheckInModelFactory, CheckInModelFactory>();
            services.AddSingleton<IRedisServerProvider, RedisServerProvider>();
            services.AddSingleton<IEmailService, EmailService>();
            services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect("127.0.0.1:6379"));
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
