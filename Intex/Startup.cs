using Intex.Data;
using Intex.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.ML.OnnxRuntime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intex
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
            //Database:

            services.AddDbContext<crashDbContext>(options =>
            {
                options.UseMySql(Configuration["ConnectionStrings:crashDbConnection"]);
            });


            //Admin Connections and Security Stuff:
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(
                    Configuration["ConnectionStrings:ApplicationDbConnection"]);
            });
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequiredUniqueChars = 1;
            });

            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential 
                // cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<InferenceSession>(
                new InferenceSession("wwwroot/crash_model.onnx")
                );

            services.AddControllersWithViews();

            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddScoped<IcrashRepository, EFcrashRepository>();

            // Enables Authentication via Google

            //services.AddAuthentication()
            //    .AddGoogle(options =>
            //    {
            //        IConfigurationSection googleAuthNSection =
            //            Configuration.GetSection("Authentication:Google");

            //        options.ClientId = googleAuthNSection["ClientId"];
            //        options.ClientSecret = googleAuthNSection["ClientSecret"];
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Add("Content-Security-Policy", "default-src 'self';");
                await next();
            });

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "CountySeverityPage",
                //    pattern: "Severity{severity}/Page{pageNum}/County{countyName}");

                //endpoints.MapControllerRoute(
                //    name: "SeverityPage",
                //    pattern: "Severity{severity}/Page{pageNum}",
                //    defaults: new {Controller = "Home", action = "Summary"});

                //endpoints.MapControllerRoute(
                //    name: "Severity",
                //    pattern: "Severity{severity}",
                //    defaults: new { Controller = "Home", action = "Summary", pageNum = 1});

                //endpoints.MapControllerRoute(
                //    name: "Filter",
                //    pattern: "Severity{severity}/County{countyName}/CitySearch{cityName}/Page{pageNum}",
                //    defaults: new { Controller = "Home", action = "Summary" });
                
                //endpoints.MapControllerRoute(
                //    name: "Paging",
                //    pattern: "Page{pageNum}",
                //    defaults: new { Controller = "Home", action = "Summary"});

                
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{crashId?}");

                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("admin/(*catchall)", "/Admin/Index");
            });
        }
    }
}
