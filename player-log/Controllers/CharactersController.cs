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
            var model = _mapper.Map<List<Character>, List<CharacterListVM>>(characters);
            // return the View using the mapped items
            return View(model);
        }

        // GET: CharactersController/Details/5
        public ActionResult Details(int id)
        {
            // retrieve the item from the db based oin id
            var item = _repo.FindById(id);
            // map the item to the view model
            var model = _mapper.Map<CharacterDetailsVM>(item);
            // return view with the model data
            return View(model);
        }

        // GET: CharactersController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CharactersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CharacterCreateVM model)
        {
            // check if there are any validation errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // map the viewmodel to datamodel
            var item = _mapper.Map<Character>(model);
            // check if the operation was successful
            var isSuccess = _repo.Create(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            // return to index
            return RedirectToAction(nameof(Index));
        }

        // GET: CharactersController/Edit/5
        public ActionResult Edit(int id)
        {
            // check if the item with the given id exists
            if (!_repo.RecordExists(id))
            {
                return NotFound();
            }
            // retrieve the item from the db based on id
            var item = _repo.FindById(id);
            // map the data to a view model
            var model = _mapper.Map<CharacterEditVM>(item);
            // return the view with the model data
            return View(model);
        }

        // POST: CharactersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CharacterEditVM model)
        {
            // check for any validation errors
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            // map the item to data model
            var item = _mapper.Map<Character>(model);
            // check if the operation was successful
            var isSuccess = _repo.Update(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            // return to Index
            return RedirectToAction(nameof(Index));
        }

        // GET: CharactersController/Delete/5
        public ActionResult Delete(int id)
        {
            // check if the item with given id exists
            if (!_repo.RecordExists(id))
            {
                return NotFound();
            }
            // find the item with the given id
            var item = _repo.FindById(id);
            // remove the item
            var isSuccess = _repo.Delete(item);
            // check whether the operation was successful
            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View();
            }

            return View();
        }

    }
}
