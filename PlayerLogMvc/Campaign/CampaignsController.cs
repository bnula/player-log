using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerLogMvc.Campaign
{
    public class CampaignsController : Controller
    {
        private readonly ICampaignRepository _repo;
        private readonly ILogger _logger;

        public CampaignsController(
            ICampaignRepository repo,
            ILogger<CampaignRepository> logger)
        {
            _repo = repo;
            _logger = logger;
        }
        // GET: CampaignsController
        public async Task<IActionResult> Index()
        {
            var items = await _repo.FindAllAsync();

            var model = items.Select(i => new CampaignVM
            {
                CampaignId = i.CampaignId,
                CampaignName = i.CampaignName
            });

            return View(model);

        }

        // GET: CampaignsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }
            var item = await _repo.FindByIdAsync(id);
            if (item is null)
            {
                return NotFound();
            }
            var model = new CampaignVM
            {
                CampaignId = item.CampaignId,
                CampaignName = item.CampaignName
            };

            return View(model);
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
