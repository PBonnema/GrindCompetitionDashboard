using DataAccess;
using DataAccess.Repository;
using GrindCompetitionAPI.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Linq;

namespace GrindCompetitionAPI
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
            services.AddHealthChecks();

            // TODO doet het nog niet. Zet ook goeie website url in the config
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        var allowedOrigins = Configuration.GetSection("CorsAllowedOrigins").GetChildren().Select(c => c.Value).ToArray();
                        builder.WithOrigins(allowedOrigins).WithHeaders("Content-Type").AllowAnyMethod();
                    });
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GrindCompetitionAPI", Version = "v1" });
            });

            services.AddAutoMapper(typeof(MainProfile));

            services.AddSingleton(Log.Logger);
            services.AddTransient(sp => Configuration.GetSection("BlockTanksStatsDatabaseSettings").Get<BlockTanksStatsDatabaseSettings>());
            services.AddTransient<IDateTimeProvider, DateTimeProvider>();
            services.AddTransient<IGrindCompetitionPlayerRepository, GrindCompetitionPlayerRepository>();
            services.AddTransient<IGrindCompetitionConfigRepository, GrindCompetitionConfigRepository>();
            services.AddTransient<IGrindCompetitionService, GrindCompetitionService>();
            services.AddTransient<IGrindCompetitionConfigService, GrindCompetitionConfigService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GrindCompetitionAPI v1"));
            }

            app.UseSerilogRequestLogging();

            app.UseRouting();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHealthChecks("/health");
            });
        }
    }
}
