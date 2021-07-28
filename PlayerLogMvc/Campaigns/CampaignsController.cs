using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlayerLogMvc.Campaigns
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
            var actionName = "Campaigns - Index:";
            try
            {
                _logger.LogInformation($"{actionName} Called");
                var items = await _repo.FindAllAsync();

                var model = items.Select(i => new CampaignVM
                {
                    CampaignId = i.CampaignId,
                    CampaignName = i.CampaignName
                });

                _logger.LogInformation($"{actionName} Success");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
            

        }

        // GET: CampaignsController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var actionName = "Campaigns - Details:";
            try
            {
                _logger.LogInformation($"{actionName} Called - Id: {id}");
                if (id < 1)
                {
                    _logger.LogWarning($"{actionName} Invalid Id - {id}");
                    return RedirectToPage("/BadRequest");
                }
                var item = await _repo.FindByIdAsync(id);
                if (item is null)
                {
                    _logger.LogWarning($"{actionName} Item not found - Id: {id}");
                    return RedirectToPage("/NotFound");
                }
                var model = new CampaignDetailsVM
                {
                    CampaignName = item.CampaignName,
                    Npcs = item.Npcs
                };

                _logger.LogInformation($"{actionName} Success");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        // GET: CampaignsController/Create
        public ActionResult Create()
        {
            var actionName = "Campaigns - Create:";
            _logger.LogInformation($"{actionName} Called");
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        // POST: CampaignsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CampaignVM model)
        {
            var actionName = "Campaigns - Create(Post):";
            _logger.LogInformation($"{actionName} Called");
            try
            {
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("x", "Please fix validation errors");
                    return View(model);
                }

                var item = new Campaign
                {
                    CampaignName = model.CampaignName
                };

                var success = await _repo.CreateAsync(item);

                if (!success)
                {
                    _logger.LogWarning($"{actionName} Failed");
                    return RedirectToPage("/InternalServerError");
                }

                _logger.LogInformation($"{actionName} Success");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        // GET: CampaignsController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var actionName = "Campaigns - Edit:";
            try
            {
                _logger.LogInformation($"{actionName} Called - Id: {id}");
                if (id < 1)
                {
                    _logger.LogWarning($"{actionName} Invalid Id - {id}");
                    return RedirectToPage("/BadRequest");
                }

                var item = await _repo.FindByIdAsync(id);

                if (item is null)
                {
                    _logger.LogWarning($"{actionName} Item not Found - Id: {id}");
                    return RedirectToPage("/NotFound");
                }

                var model = new CampaignVM
                {
                    CampaignId = item.CampaignId,
                    CampaignName = item.CampaignName
                };

                _logger.LogInformation($"{actionName} Success");
                return View("Edit", model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        // POST: CampaignsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CampaignVM model)
        {
            var actionName = "Campaigns - Edit(Post):";
            try
            {
                _logger.LogInformation($"{actionName} Called");

                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("x", "Please fix validation errors");
                    return View(model);
                }

                if (model is null)
                {
                    _logger.LogWarning($"{actionName} Empty model");
                    return RedirectToPage("/BadRequest");
                }

                var item = new Campaign
                {
                    CampaignId = model.CampaignId,
                    CampaignName = model.CampaignName
                };

                var success = await _repo.UpdateAsync(item);

                if (!success)
                {
                    _logger.LogInformation($"{actionName} Failed");
                    return RedirectToPage("/InternalServerError");
                }

                _logger.LogInformation($"{actionName} Success");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return View(model);
            }
        }

        // POST: CampaignsController/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var actionName = "Campaigns - Delete:";
            try
            {
                _logger.LogInformation($"{actionName} Called - Id: {id}");
                if (id < 1)
                {
                    _logger.LogWarning($"{actionName} Invalid Id - {id}");
                    return RedirectToPage("/BadRequest");
                }

                var item = await _repo.FindByIdAsync(id);

                if (item is null)
                {
                    _logger.LogWarning($"{actionName} Item Not Found - Id: {id}");
                    return RedirectToPage("/NotFound");
                }

                var success = await _repo.DeleteAsync(item);

                if (!success)
                {
                    _logger.LogError($"{actionName} Failed");
                    return RedirectToPage("/InternalServerError");
                }

                _logger.LogInformation($"{actionName} Success");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        // Will need to figure out how to mock Controller and Action names in Unit tests
        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;
            return $"{controller} - {action}:";
        }
    }
}
