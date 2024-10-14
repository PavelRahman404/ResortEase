using Microsoft.AspNetCore.Mvc;
using ResortEase.Domain.Entities;
using ResortEase.Infrastructure.Data;

namespace ResortEase.Web.Controllers
{
    public class VillaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillaController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var villas = _context.Villas.ToList();
            return View(villas);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Villa villa)
        {
            if (villa.Name == villa.Description)
            {
                ModelState.AddModelError("name", "The description cannot exactly match the Name.");
            }

            if (ModelState.IsValid)
            {
                _context.Villas.Add(villa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Update(int villaId)
        {
            Villa? villa = _context.Villas.FirstOrDefault(u => u.Id == villaId);
            if(villa == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villa);
        }

        [HttpPost]
        public IActionResult Update(Villa villa)
        {
            if (ModelState.IsValid && villa.Id >0)
            {
                _context.Villas.Update(villa);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

    }
}
