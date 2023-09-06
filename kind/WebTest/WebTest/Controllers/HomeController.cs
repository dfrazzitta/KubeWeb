using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
//using WebTest.Models;
using AspNetCore.Unobtrusive.Ajax;
using Newtonsoft.Json;
using System.Data;
using System;

namespace WebTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        public IActionResult SideBar()
        {
            return View();
        }


        public IActionResult Accordian()
        {
            return View();
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }


        public string GetMeSomeData()
        {
            List<Product> value = new List<Product>();
            Product pd = new Product();
            pd.Description = "Electronics";
            pd.Name = "Computer";
            pd.Price = 23M;
            pd.myname = "hitme";
            pd.myname1 = "https://github.com/dfrazzitta/net/blob/master/Controllers/HomeController.cs";

            value.Add(pd);
            value.Add(new Product { Name = "Laptop", Description = "Electronics", Price = 33.75M, myname = "hitme",
                 myname1 = "shit.com"
            });
            value.Add(new Product { Name = "iPhone4", Description = "Phone", Price = 16.99M, myname = "hitme",
                 myname1 = "nubbles.com"
            });

            string json = JsonConvert.SerializeObject(value);

            return json;
            // var jj = Json(value.ToArray());
            // return Json(value.ToArray()); //
            // var categoriesList =  new JavaScriptSerializer().Serialize(new[] { "value1", "value2" });
            // return value;    //propa // View();
        }


        [HttpPost]
        [AjaxOnly]
        [ValidateAntiForgeryToken]
        public string AntiForgeryMethod( string name)
        {
            List<PersonModelY> my = new List<PersonModelY>();
            
            List < RoleModel > rmlist = new List<RoleModel>();

            RoleModel rm1 = new RoleModel();
            rm1.RoleName = "rm1";
            rm1.Description = "description1";

             

            RoleModel rm2 = new RoleModel();
            rm2.RoleName = "rm2";
            rm2.Description = "description2";

            rmlist.Add(rm1);
            rmlist.Add(rm2);

            Account account = new Account
            {
                Email = "james@example.com",
                Active = true,
                CreatedDate = new DateTime(2013, 1, 20, 0, 0, 0, DateTimeKind.Utc),
                Roles = new List<string>
                {
                    "User",
                    "Admin"
                }
            };
            string json = JsonConvert.SerializeObject(account, Formatting.Indented);

            /*
             * 
             * 

          List<RoleModelY> rmy = new List<RoleModelY>();

          RoleModelY rmy1 = new RoleModelY();
          rmy1.RoleNameY = "rmy1Name1";
          rmy1.DescriptionY = "rmy1Description1";

          RoleModelY rmy2 = new RoleModelY();
          rmy2.RoleNameY = "rmy1Name2";
          rmy2.DescriptionY = "rmy1Description2";

          rmy.Add(rmy1); rmy.Add(rmy2);


          List<Clients> clients = new List<Clients>();
          Clients c1 = new Clients();
          c1.Name = "dave fraz";  
          c1.Address = "980 7th Street NW";
          c1.Age = "53";
          c1.State = "Fla";
          c1.Married = "Y";
          clients.Add(c1);

          Clients c2 = new Clients();
          c2.Name = "dave fraz2";  
          c2.Address = "980 7th Street NW2";
          c2.Age = "52";
          c2.State = "Fla2";
          c2.Married = "F";
          clients.Add(c2);
          */
            ClientsJSON pd = new ClientsJSON();

            Clients1 p1 = new Clients1();
            p1.Name = "dave Otto";
            p1.Age = 25;
            p1.Country = 1;
            p1.Address = "987 7th Street NW";
            p1.Married = false;
            pd.clients.Add(p1);


            Clients1 p2 = new Clients1();
            p2.Name = "2dave Otto";
            p2.Age = 22;
            p2.Country = 1;
            p2.Address = "2987 7th Street NW";
            p2.Married = false;
            pd.clients.Add(p2);

            string output = JsonConvert.SerializeObject(pd);

            var uiop = Json(pd);



            return output; // uiop;
            //var hdr = this.ControllerContext.HttpContext.Request.Headers;
            // if (string.IsNullOrEmpty(name))
            {
                //return SweetAlert("Oops!", "Please enter your name.", "warning");
            }
            //string output = JsonConvert.SerializeObject(person);


            //return clients; // SweetAlert($"Hello {name}", "Message returned from Server!", "success");
        }
    }
}