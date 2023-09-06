using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace faweb.Controllers
{
    public class kubespecifics : Controller
    {
        // GET: kubespecifics
        public ActionResult Index()
        {
            return View();
        }

        // GET: kubespecifics/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: kubespecifics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: kubespecifics/Create
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

        // GET: kubespecifics/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: kubespecifics/Edit/5
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

        // GET: kubespecifics/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: kubespecifics/Delete/5
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
