using AspNetCore.Unobtrusive.Ajax;
using KubeWeb.Data;
using KubeWeb.prometheus;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddHttpLogging(logging =>
{
    
    logging.RequestHeaders.Add("Cache-Control");
    logging.ResponseHeaders.Add("Server");
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.ResponseHeaders.Add("MyResponseHeader");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});



builder.Services.AddUnobtrusiveAjax();
builder.Services.AddMiniProfiler(options =>
{
    options.RouteBasePath = "/profiler";
    options.ColorScheme = StackExchange.Profiling.ColorScheme.Dark;
});


// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultKube");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();
builder.Services.AddHealthChecks();

/*
 *  builder.WebHost.ConfigureKestrel(options =>
            {
                var port = 443;
                var pfxFilePath = @"/app/testcustom.pfx";
                //var pfxFilePath = @"./davetest.pfx";
                // The password you specified when exporting the PFX file using OpenSSL.
                // This would normally be stored in configuration or an environment variable;
                // I've hard-coded it here just to make it easier to see what's going on.
                var pfxPassword = "";

                options.Listen(IPAddress.Any, port, listenOptions =>
                {
                    // Enable support for HTTP1 and HTTP2 (required if you want to host gRPC endpoints)
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    // Configure Kestrel to use a certificate from a local .PFX file for hosting HTTPS
                    listenOptions.UseHttps(pfxFilePath, pfxPassword);
                });


                options.Listen(IPAddress.Any, 5000, listenOptions =>
                {
                    // Enable support for HTTP1 and HTTP2 (required if you want to host gRPC endpoints)
                    listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                    // Configure Kestrel to use a certificate from a local .PFX file for hosting HTTPS

                    //listenOptions.UseHttps(pfxFilePath, pfxPassword);
                });

            });

*/
 
var app = builder.Build();


app.UseHttpLogging();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  //  app.UseHsts();
}

app.UseUnobtrusiveAjax();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var ctx = services.GetRequiredService<ApplicationDbContext>();
    ctx.Database.Migrate();
}


//app.UseRequestMiddleware();
//app.UseGaugeMiddleware();


//app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseMiniProfiler();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();


/*
   
 

public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Message = "I am Ziggy Rafiq";
        return View();
    }
}
@{
    ViewBag.Title = "Ziggy Rafiq Website";
}
< script >
    var message = '@ViewBag.Message';
alert(message);
</ script >










 
 * 
@{
    ViewBag.MyData = "Ziggy Rafiq Blog Post";
}

<script>
    $.ajax({
        url: '@Url.Action("GetData")',
        type: 'GET',
        data: { myData: '@ViewBag.MyData' },
        success: function (data) {
            console.log(data); // Output: "Ziggy Rafiq Blog Post"
        }
    });

</script>
// Controller action method
public ActionResult GetData(string myData)
{
    return Content(myData);
}
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 * 
 @{
    ViewBag.MyData = "Ziggy, Rafiq Blog Post";
}
<input type="hidden" id="my-data" value="@ViewBag.MyData" />
<script>
    var myData = document.getElementById("my-data").value;
    console.log(myData); // Output: "Ziggy, Rafiq Blog Post"
</script>
 * 
 * 
 * 
 * 
 @{
    ViewData["MyData"] = new { Name = "Lerzan", Age = 28 };
}
<script>
    var myData = @Html.Raw(Json.Serialize(ViewData["MyData"]));
    console.log(myData.Name); // Output: "Lerzan"
    console.log(myData.Age); // Output: 28
</script>

 
 public class HomeController : Controller
{
    public IActionResult Index()
    {
        ViewBag.Message = "I am Ziggy Rafiq";
        return View();
    }
}
@{
    ViewBag.Title = "Ziggy Rafiq Website";
}
<script>
    var message = '@ViewBag.Message';
    alert(message);
</script>
 
 
 
@{
    ViewBag.MyData = "Ziggy Rafiq Blog Post";
}

<script>
    $.ajax({
        url: '@Url.Action("GetData")',
        type: 'GET',
        data: { myData: '@ViewBag.MyData' },
        success: function (data) {
            console.log(data); // Output: "Ziggy Rafiq Blog Post"
        }
    });

</script>
// Controller action method
public ActionResult GetData(string myData)
{
    return Content(myData);
}
 
 
 
 
 
 
 
 
 */