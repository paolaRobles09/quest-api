using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Quest.Infrastructure.Data.EntityFramework;
using Quest.Core.Interfaces;
using Quest.Core.Services;
using Quest.Core.Mapping;

namespace Quest.API
{
    public class Startup(IConfiguration config)
    {
        public IConfiguration Configuration { get; } = config;

        public void ConfigureServices(IServiceCollection services)
        {
            // Adding services
            services.AddDbContext<QuestDbContext>(options =>
               options.UseSqlite(Configuration.GetConnectionString("QuestConnection"),
               migrations => migrations.MigrationsAssembly("Quest.Infrastructure")));

            services.AddAutoMapper(typeof(MappingProfile));
            
            // Add Swagger generator, defining one document
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Questing Engine", Version = "v1" });
            });

            services.AddControllers();
            

           
            services.AddTransient<IQuestService, QuestService>();
            services.AddMemoryCache();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Questing Engine v1");
            });

        }
    }

}
