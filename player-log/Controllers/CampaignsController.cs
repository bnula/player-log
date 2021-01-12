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
    public class CampaignsController : Controller
    {
        private readonly ICampaignRepository _repo;
        private readonly IMapper _mapper;
        public CampaignsController(
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
            var model = _mapper.Map<List<Campaign>, List<ListCampaignsVM>>(campaigns);
            return View(model);
        }

        // GET: CampaignsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
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
        public ActionResult Create(CreateCampaignVM model)
        // POST function to send the data from Create page to the Db
        {
            // check if there are any validation errors and return the same view if there are
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // take the CreateCampaignVM and map it to Camapign DataModel
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
            // map the item to the EditCampaignVM
            var model = _mapper.Map<EditCampaignVM>(campaign);
            // display the view with the model data
            return View(model);
        }

        // POST: CampaignsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditCampaignVM model)
        // POST method for updating the Db
        {
            try
            {
                // check if the model is valid
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // map the EditCampaignVM to the data model
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

        // GET: CampaignsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CampaignsController/Delete/5
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
