using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Team2Application.Data;
using Team2Application.Models;
using Team2Application.Services;

namespace Team2Application.Controllers
{
    [Authorize(Roles = "User, Operator")]
    public class InternsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IInternBroadcastService broadcastService;

        public InternsController(ApplicationDbContext context, IInternBroadcastService broadcastService)
        {
            _context = context;
            this.broadcastService = broadcastService;
        }

        // GET: Interns
        public async Task<IActionResult> Index()
        {
            return View(await _context.Intern.ToListAsync());
        }

        // GET: Interns/Details/5
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern = await _context.Intern
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intern == null)
            {
                return NotFound();
            }

            return View(intern);
        }

        // GET: Interns/Create
        [Authorize(Roles = "Operator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Interns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Create([Bind("Name,Id,Birthdate,EmailAddress")] Intern intern)
        {
            if (ModelState.IsValid)
            {
                _context.Add(intern);
                await _context.SaveChangesAsync();
                broadcastService.InternAdded(intern.Id, intern.Name, intern.Birthdate, intern.EmailAddress);
                return RedirectToAction(nameof(Index));
            }
            return View(intern);
        }

        // GET: Interns/Edit/5
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern = await _context.Intern.FindAsync(id);
            if (intern == null)
            {
                return NotFound();
            }
            return View(intern);
        }

        // POST: Interns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,Birthdate,EmailAddress")] Intern intern)
        {
            if (id != intern.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(intern);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InternExists(intern.Id))
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
            return View(intern);
        }

        // GET: Interns/Delete/5
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var intern = await _context.Intern
                .FirstOrDefaultAsync(m => m.Id == id);
            if (intern == null)
            {
                return NotFound();
            }

            return View(intern);
        }

        // POST: Interns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var intern = await _context.Intern.FindAsync(id);
            _context.Intern.Remove(intern);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public int GetAge(Intern intern)
        {
            return intern.GetAge();
        }

        private bool InternExists(int id)
        {
            return _context.Intern.Any(e => e.Id == id);
        }
    }
}
