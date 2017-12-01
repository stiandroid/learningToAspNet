﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using MyStore.Models;

namespace MyStore
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
			services.AddDbContext<MyStoreContext>(options => 
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddMvc();
//				.AddSessionStateTempDataProvider();

			// Adds a default in-memory implementation of IDistributedCache.
			services.AddDistributedMemoryCache();

			services.AddSession(options =>
			{
				// Set a short timeout for easy testing. (.FromSeconds(10))
				options.IdleTimeout = TimeSpan.FromHours(24);
				options.Cookie.HttpOnly = true;
			});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

			app.UseSession();

			app.UseMvcWithDefaultRoute();

			//app.UseMvc(routes =>
			//         {
			//             routes.MapRoute(
			//                 name: "default",
			//                 template: "{controller=Home}/{action=Index}/{id?}");
			//         });
		}
    }
}
