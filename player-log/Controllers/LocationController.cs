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
    public class LocationController : Controller
    {
        private readonly ILocationRepository _locRepo;
        private readonly ICampaignRepository _campRepo;
        private readonly IMapper _mapper;

        public LocationController(
            ILocationRepository locRepo,
            ICampaignRepository campRepo,
            IMapper mapper)
        {
            _locRepo = locRepo;
            _campRepo = campRepo;
            _mapper = mapper;
        }
        // GET: LocationController
        public ActionResult Index()
        {
            // retrieve all items from db
            var items = _locRepo.FindAll().ToList();
            // convert them to the view model
            var model = _mapper.Map<List<LocationListVM>>(items);
            // return the view with the data
            return View(model);
        }

        // GET: LocationController/Details/5
        public ActionResult Details(int id)
        {
            // check if a record with the given id exists
            if (!_locRepo.RecordExists(id))
            {
                return NotFound();
            };
            // retrieve the item from the db
            var item = _locRepo.FindById(id);
            // retrieve the campaign for the item
            var camp = _campRepo.FindById(item.CampaignId);
            // convert the item to a view model
            var model = _mapper.Map<LocationViewDetailsVM>(item);
            // add the camapign to the view model
            model.Campaign = camp;
            // return the view with the data
            return View(model);
        }

        // GET: LocationController/Create
        public ActionResult Create()
        {
            // retrieve all campaigns
            var campaigns = _campRepo.FindAll().ToList();
            // create a list of the campaigns
            var campList = campaigns.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            // new view model for the create view
            var model = new LocationDetailsVM
            {
                Campaigns = campList
            };
            return View(model);
        }

        // POST: LocationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(LocationDetailsVM model)
        {
            // check if there are any validation errors
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            // convert the VM to a data model
            var item = _mapper.Map<Location>(model);
            // create the item and check if the operation was successfull
            var isSuccess = _locRepo.Create(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }

        // GET: LocationController/Edit/5
        public ActionResult Edit(int id)
        {
            // check if an item with the given id exists
            if (!_locRepo.RecordExists(id))
            {
                return NotFound();
            }

            // get all the campaigns to a list
            var campaigns = _campRepo.FindAll().ToList();
            var campList = campaigns.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            }
                );
            // retrieve the item from the db
            var item = _locRepo.FindById(id);
            // convert it to the ViewModel
            var model = _mapper.Map<LocationDetailsVM>(item);
            // add the campaign data to the item
            model.Campaigns = campList;
            // show the model
            return View(model);
        }

        // POST: LocationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(LocationDetailsVM model)
        {
            // check if there are any validation errors
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            // convert the view model to data model
            var item = _mapper.Map<Location>(model);
            // update the item and check whether the operation was a success
            var isSuccess = _locRepo.Update(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            // return to index
            return RedirectToAction(nameof(Index));
        }

        // GET: LocationController/Delete/5
        public ActionResult Delete(int id)
        {
            // check if the record with the given id exists
            if (!_locRepo.RecordExists(id))
            {
                return NotFound();
            }
            // find the item
            var item = _locRepo.FindById(id);
            // remove the item and check if the operation was a success
            var isSuccess = _locRepo.Delete(item);
            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View();
            };
            return View();
        }
    }
}
