using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayerLogMvc.Campaign;
using PlayerLogMvc.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Npc
{
    public class NpcsController : Controller
    {
        private readonly INpcRepository _repo;
        private readonly ICampaignRepository _campRepo;
        private readonly ILocationRepository _locRepo;
        private readonly ILogger<NpcRepository> _logger;

        public NpcsController(
            INpcRepository repo,
            ILogger<NpcRepository> logger,
            ICampaignRepository campRepo,
            ILocationRepository locRepo)
        {
            _repo = repo;
            _logger = logger;
            _campRepo = campRepo;
            _locRepo = locRepo;
        }
        public async Task<IActionResult> Index()
        {
            var actionName = "Npcs - Index:";
            try
            {
                _logger.LogInformation($"{actionName} Called");
                var items = await _repo.FindAllAsync();

                var model = items.Select(i => new NpcVM
                {
                    NpcId = i.NpcId,
                    NpcName = i.NpcName,
                    Allegiance = i.Allegiance,
                    Campaign = i.Campaign
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

        public async Task<IActionResult> Details(int id)
        {
            var actionName = "Npcs - Details:";
            try
            {
                _logger.LogInformation($"{actionName} Called - Id: {id}");
                if (id < 0)
                {
                    _logger.LogWarning($"{actionName} Invalid Id - {id}");
                    return RedirectToPage("/BadRequest");
                }

                var item = await _repo.FindByIdAsync(id);

                if (item  == null)
                {
                    _logger.LogWarning($"{actionName} Item Not Found - Id: {id}");
                    return RedirectToPage("/NotFound");
                }

                var model = new NpcDetailsVM
                {
                    NpcId = item.NpcId,
                    NpcName = item.NpcName,
                    Campaign = item.Campaign,
                    Description = item.Description,
                    Notes = item.Notes,
                    Allegiance = item.Allegiance,
                    HomeLocation = item.HomeLocation,
                    CurrentLocation = item.CurrentLocation
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Camps = await _campRepo.FindAllAsync();
            ViewBag.Locations = await _locRepo.FindAllAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NpcDetailsVM model)
        {
            var actionName = "Npcs - Create(Post):";
            try
            {
                //ViewBag.Camps = await _campRepo.FindAllAsync();
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("x", "Please fix validation errors");
                    return View(model);
                }

                var item = new Npc
                {
                    NpcName = model.NpcName,
                    Description = model.Description,
                    Allegiance = model.Allegiance,
                    Notes = model.Notes,
                    CampaignId = model.Campaign.CampaignId,
                    HomeLocationId = model.HomeLocation.LocationId,
                    CurrentLocationId = model.CurrentLocation.LocationId
                };

                var success = await _repo.CreateAsync(item);

                if (!success)
                {
                    ModelState.AddModelError("x", "Something went wrong, try again");
                    return View(model);
                }

                _logger.LogInformation($"{actionName} Success");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"{actionName} Failed - {ex}");
                ViewBag.Error = $"{ex.Message} - {ex.InnerException}";
                return RedirectToPage("/InternalServerError");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            var actionName = "Npcs - Edit:";
            try
            {
                _logger.LogInformation($"{actionName} Called - Id: {id}");
                ViewBag.Camps = await _campRepo.FindAllAsync();
                ViewBag.Locations = await _locRepo.FindAllAsync();
                if (id < 0)
                {
                    _logger.LogWarning($"{actionName} Invalid Id - {id}");
                    return RedirectToPage("/BadRequest");
                }

                var item = await _repo.FindByIdAsync(id);

                if (item == null)
                {
                    _logger.LogWarning($"{actionName} Item Not Found - Id: {id}");
                    return RedirectToPage("/NotFound");
                }

                var model = new NpcDetailsVM
                {
                    NpcId = item.NpcId,
                    NpcName = item.NpcName,
                    Campaign = item.Campaign,
                    Description = item.Description,
                    Notes = item.Notes,
                    Allegiance = item.Allegiance,
                    CurrentLocation = item.CurrentLocation,
                    HomeLocation = item.HomeLocation
                };

                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(NpcDetailsVM model)
        {
            var actionName = "Npcs - Edit(Post):";
            try
            {
                _logger.LogInformation($"{actionName} Called");
                ViewBag.Camps = await _campRepo.FindAllAsync();
                ViewBag.Locations = await _locRepo.FindAllAsync();
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("x", "Please fix validation errors");
                    return View(model);
                }

                var item = new Npc
                {
                    NpcName = model.NpcName,
                    NpcId = model.NpcId,
                    Description = model.Description,
                    Allegiance = model.Allegiance,
                    Notes = model.Notes,
                    CampaignId = model.Campaign.CampaignId,
                    HomeLocationId = model.HomeLocation.LocationId,
                    CurrentLocationId = model.CurrentLocation.LocationId
                };

                var success = await _repo.UpdateAsync(item);

                if (!success)
                {
                    ModelState.AddModelError("x", "Something went wrong, try again");
                    return View(model);
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

        public async Task<IActionResult> Delete(int id)
        {
            var actionName = "Npcs - Delete:";
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
                    return NotFound("/Not Found");
                }

                var success = await _repo.DeleteAsync(item);

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
    }
}
