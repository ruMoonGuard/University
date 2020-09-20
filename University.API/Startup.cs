using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using University.API.Code.Extensions;
using University.Infrastructure.Data;

namespace University.API
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
            services
                .AddDbContext<UniversityContext>(o =>
                {
                    o.UseSqlServer(Configuration.GetConnectionString("UniversityContext"), x => x.MigrationsAssembly("University.Infrastructure.Data"));
                })
                .AddDependencyInjection()
                .AddApiVersioning()
                .ConfigureSwagger()
                .AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UniversityContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                context.Database.Migrate();
                app.SeedDb(context);
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(o =>
            {
                o.SwaggerEndpoint("/swagger/v1.0/swagger.json", "University API v1.0");
                o.SwaggerEndpoint("/swagger/v1.1/swagger.json", "University API v1.1");
            });
        }
    }
}
