using System.Collections.Generic;
using System.Globalization;
using Backend.Shared.Constants;
using Backend.Shared.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Backend.Web
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
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            CultureInfo.DefaultThreadCurrentCulture = Configurations.CultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = Configurations.CultureInfo;
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture(Configurations.CultureInfo),
                SupportedCultures = new List<CultureInfo>
                {
                    Configurations.CultureInfo
                },
                SupportedUICultures = new List<CultureInfo>
                {
                    Configurations.CultureInfo
                }
            });

            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStaticFiles(new StaticFileOptions()
            {
                ContentTypeProvider = new FileExtensionContentTypeProvider
                {
                    Mappings = { [".webmanifest"] = "application/manifest+json" }
                }
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseMiddleware<ErrorHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "dist";

                if (env.IsDevelopment())
                {
                    spa.UseProxyToSpaDevelopmentServer("https://localhost:5003");
                }
            });
        }
    }
}

