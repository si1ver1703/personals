using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using NuGet.Protocol.Plugins;
using System.Diagnostics;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly AppDBContext _dbContext;

        public HomeController(ILogger<HomeController> logger, AppDBContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index(Guid Id)
        {
            // var personal = await _dbContext.Personal.FirstOrDefaultAsync(m =>m.Id == Id);
            //
            // LINQ

            List<PersonalInformations> person = (from m in _dbContext.Personal select m).ToList();

            return View(person);
        }

        public IActionResult Privacy()
        {
            return View();
        }


        //CRUD - create read update delete
        [HttpGet]
        public async Task<IActionResult> CreatePersonalInformation()
        {
            List<PersonalInformations> person = (from m in _dbContext.Personal select m).ToList();

            return View(person);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreatePersonalInformation([Bind("Login,Password,FirstName,LastName,Gender,YearOfBirth")] PersonalInformations information)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _dbContext.Add(information);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }

            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. " + "Try again or call system admin");
            }

            return View(information);
        }

        //Table
        public async Task<IActionResult> TableOfInformations(string searched)
        {

            var searchPerson = from m in _dbContext.Personal select m; //LINQ 

            if (!String.IsNullOrEmpty(searched))
            {
                searchPerson = searchPerson.Where(s => s.FirstName.Contains(searched));
            }
           
            return View(await searchPerson.ToListAsync());
        }

        //Edit or Update
        [HttpPost, ActionName("EditPersonalInformatios")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditPostPersonalInformatios(Guid? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var personalInformations = await _dbContext.Personal.FirstOrDefaultAsync(s => s.Id == Id);


            if (await TryUpdateModelAsync<PersonalInformations>(
                personalInformations, "", s => s.Login, s => s.Password, s => s.FirstName, s => s.LastName, s => s.Gender, s => s.YearOfBirth))
            {
                try
                {
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes. " + "Try again or call system admin");
                }
            }

            return View(personalInformations);
        }


        public async Task<IActionResult> EditPersonalInformatios(Guid Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            var person = await _dbContext.Personal.FirstOrDefaultAsync(m => m.Id == Id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        //Details
        public async Task<IActionResult> DetailsOfPerseon(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _dbContext.Personal.FirstOrDefaultAsync(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }


        //Delete 
        public async Task<IActionResult> DeletePerson(Guid id, bool? Savechangeserror = false)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = await _dbContext.Personal.AsNoTracking().FirstOrDefaultAsync(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            if (Savechangeserror.GetValueOrDefault())
            {
                ViewData["DeleteError"] = "Delete failed, please try again later ... ";
            }

            return View(person);
        }

        //Delete continue
        [HttpPost, ActionName("DeletePerson")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConfirmDeletePerson(Guid id)
        {
            var person = await _dbContext.Personal.FindAsync(id);

            if (person == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _dbContext.Personal.Remove(person);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException)
            {
                return RedirectToAction(nameof(DeletePerson), new { id = id, Savechangeserror = true });
            }

            //return View(person);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        // Partial View

        public async Task<IActionResult> AdditionalInfo()
        {
            return PartialView("AdditionalInfo");
        }


    }
}