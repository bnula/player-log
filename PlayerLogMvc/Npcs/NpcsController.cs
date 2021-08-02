using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using PlayerLogMvc.Campaigns;
using PlayerLogMvc.Locations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayerLogMvc.Npcs
{
    public class NpcsController : Controller
    {
        private readonly INpcRepository _repo;
        private readonly ICampaignRepository _campRepo;
        private readonly ILocationRepository _locRepo;
        private readonly IMapper _mapper;
        private readonly ILogger<NpcRepository> _logger;

        public NpcsController(
            INpcRepository repo,
            ILogger<NpcRepository> logger,
            ICampaignRepository campRepo,
            ILocationRepository locRepo,
            IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _campRepo = campRepo;
            _locRepo = locRepo;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            var actionName = "Npcs - Index:";
            try
            {
                _logger.LogInformation($"{actionName} Called");
                var items = await _repo.FindAllAsync();

                var model = _mapper.Map<IEnumerable<Npc>, IEnumerable<NpcVM>>(items);

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
                if (id < 1)
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

                var model = _mapper.Map<Npc, NpcDetailsVM>(item);

                _logger.LogInformation($"{actionName} Success");
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
                if (!ModelState.IsValid)
                {
                    ModelState.AddModelError("x", "Please fix validation errors");
                    return View(model);
                }

                var item = _mapper.Map<NpcDetailsVM, Npc>(model);

                var success = await _repo.CreateAsync(item);

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
                if (id < 1)
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

                var model = _mapper.Map<Npc, NpcDetailsVM>(item);

                return View("Edit", model);
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

                if (model is null)
                {
                    _logger.LogWarning($"{actionName} Model is Null");
                    return RedirectToPage("/BadRequest");
                }

                var item = _mapper.Map<NpcDetailsVM, Npc>(model);

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
                    return RedirectToPage("/NotFound");
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
