using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using player_log.Contracts;
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
            return View();
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
