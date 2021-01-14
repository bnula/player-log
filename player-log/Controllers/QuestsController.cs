﻿using AutoMapper;
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
    public class QuestsController : Controller
    {
        private readonly IQuestRepository _repo;
        private readonly IMapper _mapper;
        public QuestsController(
            IQuestRepository repo,
            IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        // GET: QuestsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: QuestsController/Details/5
        public ActionResult Details(int id)
        {
            // check whether the record with the given id exists
            if (!_repo.RecordExists(id))
            {
                return NotFound();
            }
            // find the record with the given id
            var item = _repo.FindById(id);
            // convert the item to the data model
            var model = _mapper.Map<QuestDetailsVM>(item);
            // return the view with the data
            return View(model);
        }

        // GET: QuestsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: QuestsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(QuestDetailsVM model)
        {
            // check for any validation errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // convert the view model to data model
            var item = _mapper.Map<Quest>(model);
            // check whether the operation was successful
            var isSuccess = _repo.Create(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            return Index();
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
