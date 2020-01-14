using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebApp;
using WebApp.Extensions;

namespace Wboy.WebApp
{
    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();
        }
        public IConfigurationRoot Configuration { get; private set; }
        // called by the runtime before the Configure method, below.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc().AddJsonOptions(options =>
            {
                //json数据格式化排版
                options.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
                //使用驼峰命名法样式的key
                options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver();
                //设置时间格式
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            }).AddControllersAsServices();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
                    o.LoginPath = new PathString("/Home/Login");
                    o.LogoutPath = new PathString("/Home/Logout");
                    o.Cookie = new CookieBuilder
                    {
                        HttpOnly = true,
                        Name = ".Own.SXH.Core.Identity",//Cookie名字
                        Path = "/" //安全
                        //Path = ".sumxiang.com" //安全
                    };
                    //o.DataProtectionProvider = null;//如果需要做负载均衡，就需要提供一个Key
                });

            //权限验证filter
            services.AddMvc(cfg =>
            {
                cfg.Filters.Add(new RightFilter());
            });

            var builder = new ContainerBuilder();
            builder.Populate(services);
            Wboy.Infrastructure.Core.SampleContext.ConnectionString = Configuration.GetConnectionString("MySql");
            Wboy.Infrastructure.Core.SampleContext.Initialize(builder);
            //var container = builder.Build();
            //return new AutofacServiceProvider(container);
            //using (var scope = Wboy.Infrastructure.Core.SampleContext.Current.BeginScope())
            //{
            //    var deviceAppService = scope
            //        .Resolve<Wboy.Application.AdminModule.IAppService.IDatabaseInitAppService>();
            //    deviceAppService.InitAsync();
            //}


            return new AutofacServiceProvider(Wboy.Infrastructure.Core.SampleContext.Current.Container);

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
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                app.UseHsts();
            }


            app.UseStaticFiles();
            //app.UseStaticFiles(new StaticFileOptions()
            //{
            //    //下面设置可以下载apk和nupkg类型的文件
            //    ContentTypeProvider = new FileExtensionContentTypeProvider(new Dictionary<string, string>
            //    {
            //        { ".apk","application/vnd.android.package-archive"},
            //        { ".nupkg","application/zip"},
            //        { ".webp","image/webp"}
            //    })

            //});
            app.UseAuthentication();


            app.UseMiddleware<VisitMiddleware>();

            if (env.IsDevelopment())
            {
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "areas",
                        template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Index}/{id?}");
                });
            }
            else
            {
                app.UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "areas",
                        template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                    routes.MapRoute(name: "default", template: "{controller=Home}/{action=Front}/{id?}");
                });
            }


        }

    }
}
