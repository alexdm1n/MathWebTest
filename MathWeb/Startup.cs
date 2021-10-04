using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MathWeb.Models;
using MathWeb.Controllers;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;
using MathWeb.Data;

namespace MathWeb
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
            services.AddDbContext<ConnectionStringClass>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));    
            services.AddControllersWithViews();

            services.AddTransient<TasksToTable>();

            services.AddControllersWithViews();
            services.AddRazorPages();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = FacebookDefaults.AuthenticationScheme;
            })
                .AddCookie()

                .AddGoogle(Options =>
                {
                    Options.ClientId = "824844844718-kqnhok3r9dkjjhaqufv1v0mb3v85dgja.apps.googleusercontent.com";
                    Options.ClientSecret = "DmLjyzhKRvnvUfJf1ONYY7ej";
                })

                .AddFacebook(Options =>
                {
                    Options.AppId = "883664962573389";
                    Options.AppSecret = "31583dda32a0ec019b1ef764933d1706";
                });

        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                
                app.UseHsts();
            }

            
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();
            
            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=RecordTask}/{action=Create}/{id?}");
            });
        }
    }
}
