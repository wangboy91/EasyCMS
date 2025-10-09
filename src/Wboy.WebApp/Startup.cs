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
            //权限验证filter
            services.AddMvc(cfg => { cfg.Filters.Add(new RightFilter()); }).AddControllersAsServices()
                //ASP.NET Core 3.0 之前，Newtonsoft.Json 是 ASP.NET Core 默认的 JSON 序列化库。
                //如果你将 Newtonsoft.Json 添加到项目中，它将自动成为默认的 JSON 序列化器。
                //但是在 ASP.NET Core 3.0 及以后的版本中，Microsoft 将 System.Text.Json 作为默认的 JSON 序列化器。
                //需要显式设置序列化方式
                .AddNewtonsoftJson();

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(o =>
                {
                    o.ExpireTimeSpan = TimeSpan.FromMinutes(43200);
                    o.LoginPath = new PathString("/Home/Login");
                    o.LogoutPath = new PathString("/Home/Logout");
                    o.Cookie = new CookieBuilder
                    {
                        HttpOnly = true,
                        Name = ".Own.SXH.Core.Identity", //Cookie名字
                        Path = "/" //安全
                        //Path = ".sumxiang.com" //安全
                    };
                    //o.DataProtectionProvider = null;//如果需要做负载均衡，就需要提供一个Key
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
            
            app.UseMiddleware<VisitMiddleware>();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                );
                endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}