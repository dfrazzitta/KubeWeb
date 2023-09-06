using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Polly;

namespace WebTest.Controllers
{
    public class WebApi : Controller
    {
        private readonly ILogger<WebApi> _logger;

        public WebApi(ILogger<WebApi> logger)
        {
            _logger = logger;
        }


        // GET: WebApi
        public async Task<string> Index()
        {
            string? str = string.Empty;

            using (var httpClient = new HttpClient())
            {
                var maxRetryAttempts = 3;
                var pauseBetweenFailures = TimeSpan.FromSeconds(2);
                 

                var retryPolicy = Policy
                    .Handle<HttpRequestException>()
                    .WaitAndRetryAsync(maxRetryAttempts, i => pauseBetweenFailures);

                await retryPolicy.ExecuteAsync(async () =>
                {
                    var response = await httpClient
                    .GetAsync("http://webapp10/weatherforecast");
                    //response.EnsureSuccessStatusCode();
                    str = await response.Content.ReadAsStringAsync();
                    //_logger.LogDebug(ty.Result);
                    
                    ViewData["json"] = response;
                    
                });
             }
            return str; // View();
        }

        // GET: WebApi/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: WebApi/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: WebApi/Create
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

        // GET: WebApi/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: WebApi/Edit/5
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

        // GET: WebApi/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: WebApi/Delete/5
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
