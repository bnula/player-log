using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Controllers
{
    public class CompanionsController : Controller
    {
        // GET: CompanionController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CompanionController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CompanionController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CompanionController/Create
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

        // GET: CompanionController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CompanionController/Edit/5
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

        // GET: CompanionController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CompanionController/Delete/5
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
