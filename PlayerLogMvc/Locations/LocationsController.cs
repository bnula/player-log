using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Npcs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Locations
{
    public class LocationsController : Controller
    {
        private readonly ILocationRepository _repo;
        private readonly ILogger<LocationsController> _logger;
        private readonly IMapper _mapper;
        private readonly ICampaignRepository _campRepo;
        private readonly INpcRepository _npcRepo;

        public LocationsController(
            ILocationRepository repo,
            ILogger<LocationsController> logger,
            IMapper mapper,
            ICampaignRepository campRepo,
            INpcRepository npcRepo
            )
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
            _campRepo = campRepo;
            _npcRepo = npcRepo;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var actionName = "Locations - Index:";
            try
            {
                _logger.LogInformation($"{actionName} Called");
                var items = await _repo.FindAllAsync();
                var model = _mapper.Map<IEnumerable<Location>, IEnumerable<LocationVM>>(items);
                _logger.LogInformation($"{actionName} Success");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Camps = await _campRepo.FindAllAsync();
            var actionName = "Locations - Create:";
            try
            {
                _logger.LogInformation($"{actionName} Called");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        public async Task<IActionResult> Create(LocationDetailsVM model)
        {
            var actionName = "Locations - Create(Post):";
            try
            {
                _logger.LogInformation($"{actionName} Called");
                if (model is null)
                {
                    _logger.LogWarning($"{actionName} Failed - empty model");
                    return RedirectToPage("/BadRequest");
                }

                var item = _mapper.Map<LocationDetailsVM, Location>(model);
                var success = await _repo.CreateAsync(item);

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

        public async Task<IActionResult> Details(int id)
        {
            ViewBag.HomeNpcs = await _npcRepo.FindByHomeLocationId(id);
            ViewBag.CurrentNpcs = await _npcRepo.FindByCurrentLocationId(id);

            var actionName = "Locations - Details:";
            try
            {
                _logger.LogInformation($"{actionName} Called - Id: {id}");

                if (id < 1)
                {
                    _logger.LogWarning($"{actionName} Bad Request - Invalid Id: {id}");
                    return RedirectToPage("/BadRequest");
                }

                var item = await _repo.FindByIdAsync(id);

                if (item is null)
                {
                    _logger.LogWarning($"{actionName} Item Not Found - Id: {id}");
                    return RedirectToPage("/NotFound");
                }

                var model = _mapper.Map<Location, LocationDetailsVM>(item);

                _logger.LogInformation($"{actionName} Success - Id: {id}");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.Camps = await _campRepo.FindAllAsync();
            var actionName = "Locations - Edit:";
            try
            {
                _logger.LogInformation($"{actionName} Called - Id: {id}");

                if (id < 1)
                {
                    _logger.LogWarning($"{actionName} Bad Request - Invalid Id: {id}");
                    return RedirectToPage("/BadRequest");
                }

                var item = await _repo.FindByIdAsync(id);

                if (item is null)
                {
                    _logger.LogWarning($"{actionName} Item Not Found - Id: {id}");
                    return RedirectToPage("/NotFound");
                }

                var model = _mapper.Map<Location, LocationDetailsVM>(item);

                _logger.LogInformation($"{actionName} Success - Id: {id}");
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogError($"{actionName} Failed - {ex}");
                return RedirectToPage("/InternalServerError");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(LocationDetailsVM model)
        {
            var actionName = "Locations - Edit(Post):";
            try
            {
                _logger.LogInformation($"{actionName} Called");
                if (model is null)
                {
                    _logger.LogWarning($"{actionName} Bad Request - empty model");
                    return RedirectToPage("/BadRequest");
                }

                var item = _mapper.Map<LocationDetailsVM, Location>(model);

                var success = await _repo.UpdateAsync(item);

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

        public async Task<IActionResult> Delete(int id)
        {
            var actionName = "Locations - Delete:";
            try
            {
                if (id < 1)
                {
                    _logger.LogWarning($"{actionName} Bad Request - Invalid Id: {id}");
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
                    _logger.LogError($"{actionName} Failed - Id: {id}");
                    return RedirectToPage("/InternalServerError");
                }

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
