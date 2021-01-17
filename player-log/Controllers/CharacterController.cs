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
    public class CharacterController : Controller
    {
        private readonly ICharacterRepository _charRepo;
        private readonly ICampaignRepository _campRepo;
        private readonly IMapper _mapper;

        public CharacterController(
            ICharacterRepository charRepo,
            ICampaignRepository campRepo,
            IMapper mapper)
        {
            _charRepo = charRepo;
            _campRepo = campRepo;
            _mapper = mapper;
        }

        // GET: CharactersController
        public ActionResult Index()
        {
            // call the functions to retrieve all the records from the Db
            var characters = _charRepo.FindAll().ToList();
            // map the list of items to the ViewModel
            var model = _mapper.Map<List<Character>, List<CharacterListVM>>(characters);
            // add the campaign to each item based on CampaignId
            foreach (var item in model)
            {
                item.Campaign = _campRepo.FindById(item.CampaignId);
            };
            // return the View using the mapped items
            return View(model);
        }

        // GET: CharactersController/Details/5
        public ActionResult Details(int id)
        {
            // retrieve the item from the db based oin id
            var item = _charRepo.FindById(id);
            // retrieve the campaign
            var camp = _campRepo.FindById(item.CampaignId);
            // map the item to the view model
            var model = _mapper.Map<CharacterViewDetailsVM>(item);
            // add campaign to the model
            model.Campaign = camp;
            // return view with the model data
            return View(model);
        }

        // GET: CharactersController/Create
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
            // create new model using the campaign list
            var model = new CharacterDetailsVM
            {
                Campaigns = campList
            };
            return View(model);
        }

        // POST: CharactersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CharacterDetailsVM model)
        {
            // check if there are any validation errors
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // map the viewmodel to datamodel
            var item = _mapper.Map<Character>(model);
            // check if the operation was successful
            var isSuccess = _charRepo.Create(item);

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
            // retrieve all campaigns
            var campaigns = _campRepo.FindAll().ToList();
            // create list of campaigns
            var campList = campaigns.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            // check if the item with the given id exists
            if (!_charRepo.RecordExists(id))
            {
                return NotFound();
            }
            // retrieve the item from the db based on id
            var item = _charRepo.FindById(id);
            // map the data to a view model
            var model = _mapper.Map<CharacterDetailsVM>(item);
            // add the campaign list to the model
            model.Campaigns = campList;
            // return the view with the model data
            return View(model);
        }

        // POST: CharactersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CharacterDetailsVM model)
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
            var isSuccess = _charRepo.Update(item);

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
            if (!_charRepo.RecordExists(id))
            {
                return NotFound();
            }
            // find the item with the given id
            var item = _charRepo.FindById(id);
            // remove the item
            var isSuccess = _charRepo.Delete(item);
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
