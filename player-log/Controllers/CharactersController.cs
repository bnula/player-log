using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using player_log.Contracts;
using player_log.Data;
using player_log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Controllers
{
    public class CharactersController : Controller
    {
        private readonly ICharacterRepository _repo;
        private readonly IMapper _mapper;

        public CharactersController(
            ICharacterRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: CharactersController
        public ActionResult Index()
        {
            // call the functions to retrieve all the records from the Db
            var characters = _repo.FindAll().ToList();
            // map the list of items to the ViewModel
            var model = _mapper.Map<List<Character>, List<ListCharactersVM>>(characters);
            // return the View using the mapped items
            return View(model);
        }

        // GET: CharactersController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CharactersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CharactersController/Create
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

        // GET: CharactersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CharactersController/Edit/5
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

        // GET: CharactersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CharactersController/Delete/5
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
