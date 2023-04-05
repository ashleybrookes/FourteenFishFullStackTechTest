using DAL;
using FullStackTechTest.Models.SpecialityViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace FullStackTechTest.Controllers
{
    public class SpecialityController : Controller
    {
        private readonly ILogger<SpecialityController> _logger;
        private readonly ISpecialityRepository _specialityRepository;
        
        public SpecialityController(ILogger<SpecialityController> logger, ISpecialityRepository specialityRepository)
        {
            _logger = logger;
            _specialityRepository = specialityRepository;
        }

        // GET: SpecialityController
        public async Task<IActionResult> Index()
        {
            var model = await IndexViewModel.CreateAsync(_specialityRepository);
            return View(model);
        }

        // GET: SpecialityController/Details/specialityId=5
        public async Task<IActionResult> Details(int specialityId)
        {
            var model = await DetailsViewModel.CreateAsync(specialityId, false, false, _specialityRepository);
            return View(model);
        }

        // GET: SpecialityController/Create
        public async Task<IActionResult> Create()
        {
            var model = await DetailsViewModel.CreateAsync(0, false, true, _specialityRepository);
            return View("Details", model);
        }

        // GET: SpecialityController/Edit/specialityId=5
        public async Task<IActionResult> Edit(int specialityId)
        {
            var model = await DetailsViewModel.CreateAsync(specialityId, true, false, _specialityRepository);
            return View("Details", model);
        }

        // POST: SpecialityController/Edit/specialityId=5
        [HttpPost]
        public async Task<IActionResult> Edit(int specialityId, [FromForm] DetailsViewModel model)
        {
            //TODO : model.IsEditing and model.IsInserting is always false! AH need to find out why

            if (model.speciality.Id > 0)
            {
                await _specialityRepository.SaveAsync(model.speciality);
            } else
            {
                model.speciality.Id = await _specialityRepository.InsertAsync(model.speciality);
            }

            return RedirectToAction("Details", new { specialityId = model.speciality.Id });
        }

        // GET: SpecialityController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: SpecialityController/Delete/5
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
