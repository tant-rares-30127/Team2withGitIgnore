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
    [Authorize(Roles = "User, Operator")]
    public class SkillsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ISkillBroadcastService broadcastService;

        public SkillsController(ApplicationDbContext context, ISkillBroadcastService broadcastService)
        {
            _context = context;
            this.broadcastService = broadcastService;
        }

        [Authorize(Roles = "User, Operator")]
        [HttpPost]
        public void AddingSkills(string skillName)
        {
            string skillNameForUrl = skillName.Replace(" ", "+");
            skillNameForUrl = skillNameForUrl.Replace("#", "%23");
            string url = $"https://www.udemy.com/courses/search/?src=ukw&q={skillNameForUrl}";
            Skill skill = new Skill(skillName, url, $"{skillName} courses");
            skill.Id = _context.Skill.ToList().Count + 1;
            broadcastService.SkillAdded(skill.Id, skill.Name, skill.Description, skill.SkillMatrixUrl);
            _context.Add(skill);
            _context.SaveChanges();
        }

        // GET: Skills
        public async Task<IActionResult> Index()
        {
            return View(await _context.Skill.ToListAsync());
        }

        // GET: Skills/Edit/5
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skill.FindAsync(id);
            if (skill == null)
            {
                return NotFound();
            }
            return View(skill);
        }

        // POST: Skills/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Operator")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,Description,SkillMatrixUrl")] Skill skill)
        {
            if (id != skill.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(skill);
                    broadcastService.SkillUpdated(skill.Id, skill.Name, skill.Description, skill.SkillMatrixUrl);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SkillExists(skill.Id))
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
            return View(skill);
        }

        // GET: Skills/Delete/5
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var skill = await _context.Skill
                .FirstOrDefaultAsync(m => m.Id == id);
            if (skill == null)
            {
                return NotFound();
            }

            return View(skill);
        }

        // POST: Skills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Operator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var skill = await _context.Skill.FindAsync(id);
            _context.Skill.Remove(skill);
            broadcastService.SkillDeleted(id);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SkillExists(int id)
        {
            return _context.Skill.Any(e => e.Id == id);
        }
    }
}
