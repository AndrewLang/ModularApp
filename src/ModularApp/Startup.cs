﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Account.Models;
using Account.Services;
using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Razor;
using Microsoft.AspNet.Routing;
using Microsoft.Data.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using ModularApp.Common;

namespace ModularApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            // Set up configuration sources.
            var builder = new ConfigurationBuilder()
                                .AddJsonFile("appsettings.json")
                                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddEntityFramework()
                    .AddSqlServer()
                    .AddDbContext<ApplicationDbContext>(options =>options.UseSqlServer(Configuration["Data:DefaultConnection:ConnectionString"]));

            services.AddIdentity<User,IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();


            services.AddMvc()                    
                    .AddRazorOptions(options => {
                        options.ViewLocationExpanders.Add(new ModuleViewLocationExpander());
                    });

            services.AddExtensions()
                    .ConfigureExtensions();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseIISPlatformHandler(options => options.AuthenticationDescriptions.Clear());

            app.UseStaticFiles();

            app.UseIdentity();

            app.UseExtensions();

            app.UseMvc(routes =>
            {
                // allow extension to configure routes
                app.ConfigureExtensionRoutes(routes);

                // Support area
                routes.MapRoute(name: "areaRoute",
                   template: "{area:exists}/{controller}/{action}",
                   defaults: new { controller = "Admin",action = "Index" });

                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }

    public class ModuleViewLocationExpander:IViewLocationExpander
    {
        public ModuleViewLocationExpander()
        {
          
        }
        public IEnumerable<string> ExpandViewLocations(ViewLocationExpanderContext context,IEnumerable<string> viewLocations)
        {
            foreach(var location in viewLocations)
            {
                yield return location;
            }

            if(IsArea(context))
            {
                yield return "/Areas/ModularApp.{2}/Views/{1}/{0}.cshtml";
                yield return "/Areas/ModularApp.{2}/Views/Shared/{0}.cshtml";
            }
        }

        public void PopulateValues(ViewLocationExpanderContext context)
        {
            var name = context.ViewName;
        }

        bool IsArea( ViewLocationExpanderContext context)
        {
            if(context.ActionContext.ActionDescriptor.RouteConstraints.Any(x => x.RouteKey == "area" && !string.IsNullOrEmpty(x.RouteValue)))
                return true;
            return false;
        }
    }
}
