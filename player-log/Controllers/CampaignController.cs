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
    public class CampaignController : Controller
    {
        private readonly ICampaignRepository _repo;
        private readonly IMapper _mapper;
        public CampaignController(
            ICampaignRepository repo,
            IMapper mapper
            )
        {
            _repo = repo;
            _mapper = mapper;
        }

        // GET: CampaignsController
        public ActionResult Index()
        {
            var campaigns = _repo.FindAll().ToList();
            var model = _mapper.Map<List<Campaign>, List<CampaignListVM>>(campaigns);
            return View(model);
        }

        // GET: CampaignsController/Details/5
        public ActionResult Details(int id)
        {
            // check if an item with a given id exists
            if (!_repo.RecordExists(id))
            {
                return NotFound();
            }

            // retrieve the item from the Db using the id
            var campaignDetail = _repo.FindById(id);
            // map the item to the ViewModel
            var model = _mapper.Map<CampaignDetailsVM>(campaignDetail);

            // return the view
            return View(model);
        }

        // GET: CampaignsController/Create
        public ActionResult Create()
        // GET function to display Create page
        {
            return View();
        }

        // POST: CampaignsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CampaignDetailsVM model)
        // POST function to send the data from Create page to the Db
        {
            // check if there are any validation errors and return the same view if there are
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // take the CampaignDetailsVM and map it to Camapign DataModel
            var campaign = _mapper.Map<Campaign>(model);

            // run Create Db function and create a variable for whether the operation was successful
            var isSuccess = _repo.Create(campaign);

            // validate the succes of the operation
            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }

            //return to the Index page
            return RedirectToAction(nameof(Index));
        }

        // GET: CampaignsController/Edit/5
        public ActionResult Edit(int id)
        // GET method to display the Edit page
        {
            // check if an item with the id exists
            if (!_repo.RecordExists(id))
            {
                return NotFound();
            }

            // find the item with the given id
            var campaign = _repo.FindById(id);
            // map the item to the CampaignDetailsVM
            var model = _mapper.Map<CampaignDetailsVM>(campaign);
            // display the view with the model data
            return View(model);
        }

        // POST: CampaignsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CampaignDetailsVM model)
        // POST method for updating the Db
        {
            try
            {
                // check if the model is valid
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // map the CampaignDetailsVM to the data model
                var campaign = _mapper.Map<Campaign>(model);
                // run Update Db function and create a variable for whether the operation was successful
                var isSuccess = _repo.Update(campaign);

                if (!isSuccess)
                {
                    ModelState.AddModelError("", "Something went wrong..");
                    return View(model);
                }
                // return to Index
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
        }

        public ActionResult Delete(int id)
        {
            // check if the record with a given id exists
            if (!_repo.RecordExists(id))
            {
                return NotFound();
            }

            // find the record in the Db
            var campaign = _repo.FindById(id);

            // check whether the operation was successful
            var isSuccess = _repo.Delete(campaign);

            if (!isSuccess)
            {
                return BadRequest();
            }
            return RedirectToAction(nameof(Index));
        }

    }
}