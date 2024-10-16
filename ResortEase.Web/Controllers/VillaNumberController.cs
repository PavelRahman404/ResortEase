using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ResortEase.Domain.Entities;
using ResortEase.Infrastructure.Data;
using ResortEase.Web.ViewModels;

namespace ResortEase.Web.Controllers
{
    public class VillaNumberController : Controller
    {
        private readonly ApplicationDbContext _context;

        public VillaNumberController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var villaNumbers = _context.VillaNumbers.Include(u=>u.Villa).ToList();
            return View(villaNumbers);
        }

        public IActionResult Create()
        {
            VillaNumberVM villaNumberVM = new ()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                })
            };
            return View(villaNumberVM);
        }
        [HttpPost]
        public IActionResult Create(VillaNumberVM obj)
        {
            //ModelState.Remove("Villa");

            bool villaNumberExists = _context.VillaNumbers.Any(u => u.Villa_Number == obj.VillaNumber.Villa_Number);

            if (ModelState.IsValid && !villaNumberExists)
            {
                _context.VillaNumbers.Add(obj.VillaNumber);
                _context.SaveChanges();
                TempData["success"] = "The villa Number  has been created successfully.";
                return RedirectToAction(nameof(Index));
            }

            if (villaNumberExists)
            {
                TempData["error"] = "The villa Number already exists.";
            }
            obj.VillaList = _context.Villas.ToList().Select(u=>new SelectListItem
            {
                Text=u.Name,
                Value = u.Id.ToString(),    
            });
            return View(obj);
        }

        public IActionResult Update(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                VillaNumber = _context.VillaNumbers.FirstOrDefault(u=>u.Villa_Number== villaNumberId)

            };
            if(villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Update(VillaNumberVM villaNumberVM)
        {

            if (ModelState.IsValid)
            {
                _context.VillaNumbers.Update(villaNumberVM.VillaNumber);
                _context.SaveChanges();
                TempData["success"] = "The villa Number  has been updated successfully.";
                return RedirectToAction(nameof(Index));
            }

            villaNumberVM.VillaList = _context.Villas.ToList().Select(u => new SelectListItem
            {
                Text = u.Name,
                Value = u.Id.ToString(),
            });
            return View(villaNumberVM);
        }

        public IActionResult Delete(int villaNumberId)
        {
            VillaNumberVM villaNumberVM = new()
            {
                VillaList = _context.Villas.ToList().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                VillaNumber = _context.VillaNumbers.FirstOrDefault(u => u.Villa_Number == villaNumberId)

            };
            if (villaNumberVM.VillaNumber == null)
            {
                return RedirectToAction("Error", "Home");
            }
            return View(villaNumberVM);
        }

        [HttpPost]
        public IActionResult Delete(VillaNumberVM villaNumberVM)
        {
            VillaNumber? villaFromDb = _context.VillaNumbers
                .FirstOrDefault(u=>u.Villa_Number == villaNumberVM.VillaNumber.Villa_Number);
            if (villaFromDb is not null)
            {
                _context.VillaNumbers.Remove(villaFromDb);
                _context.SaveChanges();
                TempData["success"] = "The villa number has been deleted successfully.";
                return RedirectToAction(nameof(Index));
            }
            TempData["error"] = "The villa number could not be deleted.";
            return View();
        }
    }
}
