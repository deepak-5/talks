using AIHostedService.Cookie;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AIHostedService
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHostedService<CookieCacheRefresherHostedService>();

            services.AddControllers();
            
            services.AddDemoServices();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}