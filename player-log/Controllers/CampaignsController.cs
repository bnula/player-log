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
        {
            return View();
        }

        // POST: CampaignsController/Create
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

        // GET: CampaignsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CampaignsController/Edit/5
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
