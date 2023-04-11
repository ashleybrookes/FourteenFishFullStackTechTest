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
        private readonly IPersonSpecialityRepository _personSpecialityRepository;
        
        public SpecialityController(ILogger<SpecialityController> logger, ISpecialityRepository specialityRepository, IPersonSpecialityRepository personSpecialityRepository)
        {
            _logger = logger;
            _specialityRepository = specialityRepository;
            _personSpecialityRepository = personSpecialityRepository;
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
                if (model.speciality.SpecialityName.Length > 100) model.speciality.SpecialityName = model.speciality.SpecialityName.Substring(0, 100);
                await _specialityRepository.SaveAsync(model.speciality);
            } else
            {
                if (model.speciality.SpecialityName.Length > 100) model.speciality.SpecialityName = model.speciality.SpecialityName.Substring(0, 100);
                model.speciality.Id = await _specialityRepository.InsertAsync(model.speciality);
            }

            return RedirectToAction("Details", new { specialityId = model.speciality.Id });
        }

        // DELETE: SpecialityController/Delete/5
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int Id)
        {
            if (Id == 0)
            {
                return NoContent();
            }
            //Delete the PersonSpeciality data
            await _personSpecialityRepository.DeleteBySpecialityIdAsync(Id);
            //Delete the Speciality Data
            await _specialityRepository.DeleteByIdAsync(Id);
            //TODO: pass through a nice message that its been deleted
            return Ok();
        }
    }
}
