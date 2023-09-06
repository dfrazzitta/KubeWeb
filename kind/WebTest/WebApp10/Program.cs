using AspNetCore.Unobtrusive.Ajax;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebApp10.Data;

namespace WebApp10
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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
             
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            //builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();
            //builder.Services.Configure<CookiePolicyOptions>(options =>
            //{
            //    options.CheckConsentNeeded = context => true;
            //    options.MinimumSameSitePolicy = SameSiteMode.None;

           // });
          //  builder.Services.AddSession();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseCookiePolicy();
           // app.UseSession();
            app.UseUnobtrusiveAjax();

            app.UseAuthorization();
            app.MapControllers();

            app.Run();
        }
    }
}