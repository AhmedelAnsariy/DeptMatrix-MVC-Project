using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MVCRev.BLL;
using MVCRev.BLL.Interfaces;
using MVCRev.BLL.Repositories;
using MVCRev.DAL.Data;
using MVCRev.DAL.Models;
using MVCRev.PL.Helper;
using MVCRev.PL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCRev.PL
{
    public class Startup
    {
        public IConfiguration Configuration { get; }


        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); // add built-in mvc services to container // 3lshan ykon fahem routing


            services.AddDbContext<AppDbContext>(options =>
           options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))

        );

            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository , EmployeeRepository > ();


            services.AddScoped<IScopedServies, ScopedServies>(); // Per Request 
            services.AddTransient<ITransientServies, TransientServies>(); // Per Operation
            services.AddSingleton<ISingletonServies,SingeltonServices>(); // per APP 

            services.AddAutoMapper(m => m.AddProfile(new MappingProfile()));

            services.AddScoped<IUnitOfWork, UnitOfWork>();

			services.AddIdentity<ApplicationUser, IdentityRole>()
		            .AddEntityFrameworkStores<AppDbContext>()
                    .AddDefaultTokenProviders();


            services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles(); // css 

            app.UseRouting(); // for path in url

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute( // becuse this mvc project
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
