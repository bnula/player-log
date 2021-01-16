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
    public class QuestsController : Controller
    {
        private readonly IQuestRepository _questRepo;
        private readonly ICampaignRepository _campRepo;
        private readonly IMapper _mapper;
        public QuestsController(
            IQuestRepository questRepo,
            ICampaignRepository campRepo,
            IMapper mapper)
        {
            _questRepo = questRepo;
            _campRepo = campRepo;
            _mapper = mapper;
        }
        // GET: QuestsController
        public ActionResult Index()
        {
            // retrieve all the data from the db
            var items = _questRepo.FindAll().ToList();
            // map the data to view model
            var model = _mapper.Map<List<QuestListVM>>(items);
            // return the view with the data
            return View(model);
        }

        // GET: QuestsController/Details/5
        public ActionResult Details(int id)
        {
            // check whether the record with the given id exists
            if (!_questRepo.RecordExists(id))
            {
                return NotFound();
            }
            // find the record with the given id
            var item = _questRepo.FindById(id);
            // retrieve the campaign
            var camp = _campRepo.FindById(item.CampaignId);
            // convert the item to the data model
            var model = _mapper.Map<QuestViewDetailsVM>(item);
            // add the campaign to the model
            model.Campaign = camp;
            // return the view with the data
            return View(model);
        }

        // GET: QuestsController/Create
        public ActionResult Create()
        {
            // retrieve all campaigns from the db
            var campaigns = _campRepo.FindAll().ToList();
            // create a list using id and name
            var campList = campaigns.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            // initialize the view model with campaign list as campaigns attribute
            var model = new QuestDetailsVM
            {
                Campaigns = campList
            };
            return View(model);
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
            var isSuccess = _questRepo.Create(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            // redirect to index
            return RedirectToAction(nameof(Index));
        }

        // GET: QuestsController/Edit/5
        public ActionResult Edit(int id)
        {
            // retrieve all campaigns from the db
            var campaigns = _campRepo.FindAll().ToList();
            // create a list using id and name
            var campList = campaigns.Select(q => new SelectListItem
            {
                Text = q.Name,
                Value = q.Id.ToString()
            });
            // check if the item with a given id exists
            if (!_questRepo.RecordExists(id))
            {
                return NotFound();
            }
            // retrieve the item with the given id
            var item = _questRepo.FindById(id);
            // map the item to the view model
            var model = _mapper.Map<QuestDetailsVM>(item);
            // add the campaigns list to the model
            model.Campaigns = campList;
            // return the view with the model
            return View(model);
        }

        // POST: QuestsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(QuestDetailsVM model)
        {
            // check if there are any validation errors
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something went wrong..");
            }
            // convert the view model to a data model
            var item = _mapper.Map<Quest>(model);
            // update the item in the db and check if the operation was a success
            var isSuccess = _questRepo.Update(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
                return View(model);
            }
            // redirect back to index
            return RedirectToAction(nameof(Index));

        }

        // GET: QuestsController/Delete/5
        public ActionResult Delete(int id)
        {
            // check if the item with a given id exists
            if (!_questRepo.RecordExists(id))
            {
                return NotFound();
            }
            // find the item using the given id
            var item = _questRepo.FindById(id);
            // delete the item and check if the operation was successful
            var isSuccess = _questRepo.Delete(item);

            if (!isSuccess)
            {
                ModelState.AddModelError("", "Something went wrong..");
            }
            return View();
        }

    }
}
