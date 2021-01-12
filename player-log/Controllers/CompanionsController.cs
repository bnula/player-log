using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using player_log.Contracts;
using player_log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Controllers
{
    public class CompanionsController : Controller
    {
        private readonly ICompanionRepository _repo;
        private readonly IMapper _mapper;

        public CompanionsController(
            ICompanionRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: CompanionController
        public ActionResult Index()
        {
            // load all of the records in the Db table
            var items = _repo.FindAll();
            // map the records to a ViewModel
            var model = _mapper.Map<List<CompanionListVM>>(items);
            // return the view with the data
            return View(model);
        }

        // GET: CompanionController/Details/5
        public ActionResult Details(int id)
        {
            // retrieve the item from the db based on id
            var item = _repo.FindById(id);
            // map the item to the ViewModel
            var model = _mapper.Map<CompanionDetailsVM>(item);
            // return the view with the model data
            return View(model);
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
