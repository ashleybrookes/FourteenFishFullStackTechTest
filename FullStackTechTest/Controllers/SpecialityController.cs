using DAL;
using FullStackTechTest.Models.SpecialityViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        // GET: SpecialityController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: SpecialityController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: SpecialityController/Create
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

        // GET: SpecialityController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: SpecialityController/Edit/5
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
