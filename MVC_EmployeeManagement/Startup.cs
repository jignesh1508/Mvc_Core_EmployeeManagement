using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationCore.Interface;
using ApplicationCore.Services;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MVC_EmployeeManagement.Interface;

namespace MVC_EmployeeManagement
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<ApplicationDbContext>(options=>options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            //DI
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddTransient<IEmployeeService, EmployeeService>();

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
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            //////Default file name changes, pass it to UseDefaultFiles()
            ////FileServerOptions fileServerOptions = new FileServerOptions();
            ////fileServerOptions.DefaultFilesOptions.DefaultFileNames.Clear();
            ////fileServerOptions.DefaultFilesOptions.DefaultFileNames.Add("Hello.html");

            //////
            //app.UseFileServer(); // use inplace of UseDefaultFiles() and UseStaticFiles()

            ////app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseAuthentication();
            //for conventional routing
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=Employee}/{action=Index}/{id?}");
            });
           // app.UseMvc(); this is for attribute routing

            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
        }
    }
}
