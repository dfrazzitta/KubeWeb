using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace faweb.Controllers
{
    public class Test : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }

        public async Task<string> Pods(string KubeType)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *mywebapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();
                // request.RequestUri = new Uri("http://kapi/WeatherForecast/saveme"); // ASP.NET 3 (VS 2019 only)
                string uriApi = "http://faApi/kubernetessystem/" + "Get" + KubeType;
                request.RequestUri = new Uri(uriApi); // ASP.NET 3 (VS 2019 only)

                
                var response = await client.SendAsync(request);
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }

        }

        public async Task<string> SystemInfo(string KubeType)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *mywebapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();
                // request.RequestUri = new Uri("http://kapi/WeatherForecast/saveme"); // ASP.NET 3 (VS 2019 only)
                string uriApi = "http://faApi/kubernetessystem/" + "Get" + KubeType;
                request.RequestUri = new Uri(uriApi); // ASP.NET 3 (VS 2019 only)

                // request.RequestUri = new Uri("http://kapi/api/KubernetesSystem/GetSystemData"); // ASP.NET 3 (VS 2019 only)
                //request.RequestUri = new Uri("http://kapi/api/WeatherForecast/"); // ASP.NET 2.x
                var response = await client.SendAsync(request);
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }

        }

        //GetNamespacesOnly

        public async Task<string> GetNamespacesOnly(string KubeType)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *mywebapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();
                // request.RequestUri = new Uri("http://kapi/WeatherForecast/saveme"); // ASP.NET 3 (VS 2019 only)
                string uriApi = "http://faApi/kubernetessystem/" +  KubeType;
                request.RequestUri = new Uri(uriApi); // ASP.NET 3 (VS 2019 only)

                // request.RequestUri = new Uri("http://kapi/api/KubernetesSystem/GetSystemData"); // ASP.NET 3 (VS 2019 only)
                //request.RequestUri = new Uri("http://kapi/api/WeatherForecast/"); // ASP.NET 2.x
                var response = await client.SendAsync(request);
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }

        }


        public async Task<string> Daemons(string KubeType)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *mywebapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();
                // request.RequestUri = new Uri("http://kapi/WeatherForecast/saveme"); // ASP.NET 3 (VS 2019 only)
                string uriApi = "http://faApi/kubernetessystem/" + "Get" + KubeType;
                request.RequestUri = new Uri(uriApi); // ASP.NET 3 (VS 2019 only)

                // request.RequestUri = new Uri("http://kapi/api/KubernetesSystem/GetSystemData"); // ASP.NET 3 (VS 2019 only)
                //request.RequestUri = new Uri("http://kapi/api/WeatherForecast/"); // ASP.NET 2.x
                var response = await client.SendAsync(request);
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }

        }

        public async Task<string> Services(string KubeType)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *mywebapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();
                // request.RequestUri = new Uri("http://kapi/WeatherForecast/saveme"); // ASP.NET 3 (VS 2019 only)
                string uriApi = "http://faApi/kubernetessystem/" + "Get" + KubeType;
                request.RequestUri = new Uri(uriApi); // ASP.NET 3 (VS 2019 only)

                // request.RequestUri = new Uri("http://kapi/api/KubernetesSystem/GetSystemData"); // ASP.NET 3 (VS 2019 only)
                //request.RequestUri = new Uri("http://kapi/api/WeatherForecast/"); // ASP.NET 2.x
                var response = await client.SendAsync(request);
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }

        }

        public async Task<string> NameSpaces(string KubeType)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // Call *mywebapi*, and display its response in the page
                var request = new System.Net.Http.HttpRequestMessage();
                // request.RequestUri = new Uri("http://kapi/WeatherForecast/saveme"); // ASP.NET 3 (VS 2019 only)
                string uriApi = "http://faApi/kubernetessystem/" + "Get" + KubeType;
                request.RequestUri = new Uri(uriApi); // ASP.NET 3 (VS 2019 only)

                // request.RequestUri = new Uri("http://kapi/api/KubernetesSystem/GetSystemData"); // ASP.NET 3 (VS 2019 only)
                //request.RequestUri = new Uri("http://kapi/api/WeatherForecast/"); // ASP.NET 2.x
                var response = await client.SendAsync(request);
                var resp = await response.Content.ReadAsStringAsync();
                return resp;
            }
           
        }



        // GET: Test/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Test/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Test/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Test/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Test/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Test/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
