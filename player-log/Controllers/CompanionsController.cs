using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using player_log.Contracts;
using player_log.Data;
using player_log.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace player_log.Controllers
{
    public class CompanionsController : Controller
    {
        private readonly ICompanionRepository _npcRepo;
        private readonly ICampaignRepository _campRepo;
        private readonly IMapper _mapper;

        public CompanionsController(
            ICompanionRepository npcRepo,
            ICampaignRepository campRepo,
            IMapper mapper)
        {
            _npcRepo = npcRepo;
            _campRepo = campRepo;
            _mapper = mapper;
        }
        // GET: CompanionController
        public ActionResult Index()
        {
            // load all of the records in the Db table
            var items = _npcRepo.FindAll();
            // map the records to a ViewModel
            var model = _mapper.Map<List<CompanionListVM>>(items);
            // return the view with the data
            return View(model);
        }

        // GET: CompanionController/Details/5
        public ActionResult Details(int id)
        {
            // check wheter record with the given id exists
            if (!_npcRepo.RecordExists(id))
            {
                return NotFound();
            }
            // retrieve the item from the db based on id
            var item = _npcRepo.FindById(id);
            // retrieve the campaign
            var camp = _campRepo.FindById(item.CampaignId);
            // map the item to the ViewModel
            var model = _mapper.Map<CompanionViewDetailsVM>(item);
            // add the campaign to the model
            model.Campaign = camp;
            // return the view with the model data
            return View(model);
        }

        // GET: CompanionController/Create
        public ActionResult Create()
        {
            // retrieve all campaigns
            var campaigns = _campRepo.FindAll().ToList();
            // create list of campaigns
            var campList = campaigns.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            // create view model with the campaigns lsit
            var model = new CompanionDetailsVM
            {
                Campaigns = campList
            };
            return View(model);
        }

        // POST: CompanionController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CompanionDetailsVM model)
        {
            // check whether there are any validation errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // convert the data into the DataModel
            var item = _mapper.Map<Companion>(model);
            // check whether the operation was successful
            var isSuccess = _npcRepo.Create(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            // return to the Index
            return RedirectToAction(nameof(Index));
        }

        // GET: CompanionController/Edit/5
        public ActionResult Edit(int id)
        {
            // retrieve all campaigns
            var campaigns = _campRepo.FindAll().ToList();
            // create list of campaigns
            var campList = campaigns.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            // check if the record with the given id exists
            if (!_npcRepo.RecordExists(id))
            {
                return NotFound();
            }
            // retrieve the item from the db based on id
            var item = _npcRepo.FindById(id);
            // map the item to the ViewModel
            var model = _mapper.Map<CompanionDetailsVM>(item);
            // add the campaign list to the model
            model.Campaigns = campList;
            // return the view with the data
            return View(model);
        }

        // POST: CompanionController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CompanionDetailsVM model)
        {
            // check if there are any validation errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            // map the item to the DataModel
            var item = _mapper.Map<Companion>(model);
            // check if the operation was successful
            var isSuccess = _npcRepo.Update(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            // return to Index
            return RedirectToAction(nameof(Index));
        }

        // GET: CompanionController/Delete/5
        public ActionResult Delete(int id)
        {
            // check whether the record with the given id exists
            if (!_npcRepo.RecordExists(id))
            {
                return NotFound();
            }
            // find the item based on the id
            var item = _npcRepo.FindById(id);
            // check whether the operation was successful
            var isSuccess = _npcRepo.Delete(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View();
            }
            
            return View();
        }
    }
}
