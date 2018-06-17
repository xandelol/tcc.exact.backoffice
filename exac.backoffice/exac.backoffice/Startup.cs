using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using exac.backoffice.Api;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestEase;

namespace exac.backoffice
{
    public class Startup
    {
         public static string AuthenticationScheme = "CookieAuthentication";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.LoginPath = new PathString("/Account/Login");
                    o.AccessDeniedPath = new PathString("/Account/Forbidden/");
                });
            
            services.AddSingleton(RestClient.For<IExactApi>(Configuration["UrlBase"]));
    
            
            services.AddSingleton<ITempDataProvider, CookieTempDataProvider>();
            // Adds a default in-memory implementation of IDistributedCache.
            services.AddDistributedMemoryCache();

//            services.AddSession(options =>
//            {
//                options.Cookie.Name = ".Delivery.Api.Session";
//                options.IdleTimeout = TimeSpan.FromDays(9999);
//            });
            
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            
//            app.UseSession();
            
            app.UseAuthentication();
            
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}