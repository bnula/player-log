using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Controllers
{
    public class QuestsController : Controller
    {
        // GET: QuestsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: QuestsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: QuestsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestsController/Create
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

        // GET: QuestsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: QuestsController/Edit/5
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

        // GET: QuestsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: QuestsController/Delete/5
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
