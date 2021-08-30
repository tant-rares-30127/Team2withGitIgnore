using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using Team2Application.Data;
using Team2Application.Models;
using Team2Application.Services;

namespace Team2Application.Controllers
{
/*    [Route("api/[controller]")]
    [ApiController]*/
    public class LibraryResourcesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILibraryResourceBroadcastService broadcastService;

        public LibraryResourcesController(ApplicationDbContext context, ILibraryResourceBroadcastService broadcastService)
        {
            _context = context;
            this.broadcastService = broadcastService;
        }

        [HttpGet]
        public IEnumerable<LibraryResource> Get(int id)
        {
            string skillName = _context.Skill.ToList()[id].Name;
            string skillNameForUrl = skillName.Replace(" ", "%20");
            skillNameForUrl = skillNameForUrl.Replace("#", "%23");
            var client = new RestClient($"https://www.udemy.com/api-2.0/courses/?search={skillNameForUrl}");
            client.Authenticator = new HttpBasicAuthenticator("CkaIqUMDHO4Dp96Xc2z1Lwg9BcwS3etRvtHHuGUE", "0iS2boCGNqVoTap046T1r9UzJsVMXxxu4WOwTQDhWpaGrnZCRrwFSlL7YraegarBLM5Qcwq5bm9tAnVRQ2Yh60OExsVZRdXnVrwDub26yLdO0If4ieZ9sBWDmajn7Qq4");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            IEnumerable<LibraryResource> coursesList = this.ConvertResponseToCourseRecord(response.Content);
            return coursesList;
        }

        [NonAction]
        public IEnumerable<LibraryResource> ConvertResponseToCourseRecord(string content)
        {
            var json = JObject.Parse(content);
            if (json["results"] == null)
            {
                throw new Exception("Apikey not valid.");
            }

            var jsonArray = json["results"].Take(7);
            return jsonArray.Select(CreatingLibraryResourceFromJToken);
        }

        private LibraryResource CreatingLibraryResourceFromJToken(JToken item)
        {
            string courseTitle = (string)item.SelectToken("title");
            string description = (string)item.SelectToken("headline");
            string specificUrl = (string)item.SelectToken("url");
            string clickableUrl = $"https://www.udemy.com{specificUrl}";

            LibraryResource libraryResource = new LibraryResource(courseTitle, description, clickableUrl);
            return libraryResource;
        }

        // GET: LibraryResources
        public async Task<IActionResult> Index(int id)
        {
            if (id != 0)
            {
                List<LibraryResource> libraryResourcesList = this.Get(id - 1).ToList();
                _context.RemoveRange(_context.LibraryResource.ToList());
                _context.SaveChanges();
                int listNumber = 1;
                foreach (LibraryResource l in libraryResourcesList)
                {
                    l.Id = listNumber;
                    listNumber++;
                    broadcastService.LibraryResourceAdded(l.Id, l.Name, l.Recommandation, l.Url);
                    _context.Add(l);
                }
                _context.SaveChanges();
            }
            return View(await _context.LibraryResource.ToListAsync());
        }

        // GET: LibraryResources/Details/5
        [Authorize(Roles = "Operator, User")]
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

        // GET: LibraryResources/Edit/5
        [Authorize(Roles = "Operator")]
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
        [Authorize(Roles = "Operator")]
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
                    broadcastService.LibraryResourceUpdated(libraryResource.Id, libraryResource.Name, libraryResource.Recommandation, libraryResource.Url);
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
        [Authorize(Roles = "Operator")]
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
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libraryResource = await _context.LibraryResource.FindAsync(id);
            _context.LibraryResource.Remove(libraryResource);
            broadcastService.LibraryResourceDeleted(libraryResource.Id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryResourceExists(int id)
        {
            return _context.LibraryResource.Any(e => e.Id == id);
        }
    }
}
