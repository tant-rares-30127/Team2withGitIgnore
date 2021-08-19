using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using Team2Application.Data;
using Team2Application.Models;

namespace Team2Application.Controllers
{
    public class LibraryResourcesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibraryResourcesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IEnumerable<UdemyCoursesRecord> Get()
        {
            var client = new RestClient($"https://www.udemy.com/api-2.0/courses/?search=C#");
            client.Authenticator = new HttpBasicAuthenticator("CkaIqUMDHO4Dp96Xc2z1Lwg9BcwS3etRvtHHuGUE", "0iS2boCGNqVoTap046T1r9UzJsVMXxxu4WOwTQDhWpaGrnZCRrwFSlL7YraegarBLM5Qcwq5bm9tAnVRQ2Yh60OExsVZRdXnVrwDub26yLdO0If4ieZ9sBWDmajn7Qq4");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            IEnumerable<UdemyCoursesRecord> coursesList = this.ConvertResponseToCourseRecord(response.Content);
            return coursesList;
        }

        [NonAction]
        public IEnumerable<UdemyCoursesRecord> ConvertResponseToCourseRecord(string content)
        {
            var json = JObject.Parse(content);
            if (json["results"] == null)
            {
                throw new Exception("Apikey not valid.");
            }

            var jsonArray = json["results"].Take(7);
            return jsonArray.Select(CreatingUdemyCoursesRecordFromJToken);
        }

        private UdemyCoursesRecord CreatingUdemyCoursesRecordFromJToken(JToken item)
        {
            string courseTitle = (string)item.SelectToken("title");
            string description = (string)item.SelectToken("headline");
            string specificUrl = (string)item.SelectToken("url");
            string clickableUrl = $"https://www.udemy.com{specificUrl}";

            UdemyCoursesRecord udemyCoursesRecord = new UdemyCoursesRecord(courseTitle, clickableUrl, description);
            return udemyCoursesRecord;
        }

        // GET: LibraryResources
        public async Task<IActionResult> Index()
        {
            return View(await _context.LibraryResource.ToListAsync());
        }

        // GET: LibraryResources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryResource = await _context.LibraryResource
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libraryResource == null)
            {
                return NotFound();
            }

            return View(libraryResource);
        }

        // POST: LibraryResources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create()
        {
            IEnumerable<UdemyCoursesRecord> coursesList = this.Get();
            UdemyCoursesRecord record = coursesList.First();
            LibraryResource libraryResource = new LibraryResource(record.Title, record.Description, record.Url);
            _context.Add(libraryResource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: LibraryResources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryResource = await _context.LibraryResource.FindAsync(id);
            if (libraryResource == null)
            {
                return NotFound();
            }
            return View(libraryResource);
        }

        // POST: LibraryResources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,Recommandation,Url")] LibraryResource libraryResource)
        {
            if (id != libraryResource.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libraryResource);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryResourceExists(libraryResource.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(libraryResource);
        }

        // GET: LibraryResources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryResource = await _context.LibraryResource
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libraryResource == null)
            {
                return NotFound();
            }

            return View(libraryResource);
        }

        // POST: LibraryResources/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libraryResource = await _context.LibraryResource.FindAsync(id);
            _context.LibraryResource.Remove(libraryResource);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryResourceExists(int id)
        {
            return _context.LibraryResource.Any(e => e.Id == id);
        }
    }
}
