using AspNetCore.Unobtrusive.Ajax;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prometheus;
using WebTest.Data;



namespace WebTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
             var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddUnobtrusiveAjax();
            /*
            builder.Services.AddMvc(options => 
            { 
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()); 
            });
            */

            // builder.Services.AddUnobtrusiveAjax();
            builder.Services.AddDbContext<SchoolContext>(options =>
                  options.UseSqlServer(builder.Configuration.GetConnectionString("sqldata"))); // Vmsql")));

            builder.Services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;

            });
            builder.Services.AddSession();

            builder.Services.AddMiniProfiler(options =>
            {
                options.PopupRenderPosition = StackExchange.Profiling.RenderPosition.BottomLeft;
                options.PopupShowTimeWithChildren = true;
                options.ColorScheme = StackExchange.Profiling.ColorScheme.Auto;
                options.EnableMvcFilterProfiling = true;
                options.EnableDebugMode = true;
            }).AddEntityFramework();



            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("sqldata") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddSingleton<MetricReporter>();

            builder.Services.AddControllersWithViews();

            // kestrel region
            #region  
            builder.WebHost.ConfigureKestrel(serverOptions =>
            {
                //serverOptions.Limits.KeepAliveTimeout = TimeSpan.FromSeconds(60);
            });




            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });


            /*
                builder.WebHost.ConfigureKestrel(serverOptions =>
                {

                    serverOptions.ConfigureEndpointDefaults(listenOptions =>
                    {

                        //listenOptions.UseHttps("/app/k8svc.pfx", "tvxs721#3");
                        listenOptions.UseHttps("/app/k8svc.pfx", "");

                    });
                    serverOptions.Listen(IPAddress.Any, 443, listenOptions =>
                    {
                        listenOptions.UseConnectionLogging();

                    });
                });  */
            #endregion

            var app = builder.Build();

            // using IServiceScope scope = host.Services.CreateScope();
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                var schoolcontext = services.GetRequiredService<SchoolContext>();

                schoolcontext.Database.Migrate();

                DbInitializer.Initialize(schoolcontext);

                var appcontext = services.GetRequiredService<ApplicationDbContext>();

                appcontext.Database.Migrate();

                // DbInitializer.Initialize(appcontext);

            }

            app.UseCookiePolicy();
            app.UseSession();
            app.UseUnobtrusiveAjax();

            app.UseMiniProfiler();
            app.UseMetricServer(); // url: "/metrics");
            app.UseMiddleware<ResponseMetricMiddleware>();
            app.UseMiddleware<CustomMiddleware>();
            app.UseMiddleware<HttpContextItemsMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

/*
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebTest.Data;

namespace WebTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

*/
/*    
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebTest.Data;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AspNetCore.Unobtrusive.Ajax;

namespace WebTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddUnobtrusiveAjax();

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
               // app.UseHsts();
            }

            //app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseUnobtrusiveAjax();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}

*/