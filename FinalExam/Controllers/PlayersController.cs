using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FinalExam.Data;
using FinalExam.Models;
using System.Collections.Generic;

namespace FinalExam.Controllers
{
    public class PlayersController : Controller
    {
        private readonly SportsContext _context;

        public PlayersController(SportsContext context)
        {
            _context = context;
        }

        // GET: Players
        public async Task<IActionResult> Index(int id)
        {
            var sportsContext = _context.Players.Where(p => p.SportId == id);
            return View(await sportsContext.ToListAsync());
        }

        //[HttpPost]
        //public ActionResult Create(Players player, int id)
        //{
        //    if (ModelState.IsValid)
        //    {

        //        EmployeeContext employeeContext = new EmployeeContext();
        //        player.SportId = id;
        //        sportC.Players.Add(player);
        //        sportC.SaveChanges();
        //        employeeContext.Employees.Add(employee);
        //        employeeContext.SaveChanges();

        //        return RedirectToAction("Index", new { id = id });
        //    }
        //    return View(player);
        //}

        // GET: Players/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players
                .Include(p => p.Sport)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (players == null)
            {
                return NotFound();
            }

            return View(players);

        }

        // GET: Players/Create
        public IActionResult Create()
        {
            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "SportId");
            return View();
        }

        // POST: Players/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PlayerId,FullName,Age,Contry,SportId")] Players players)
        {
            if (ModelState.IsValid)
            {
                _context.Add(players);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "SportId", players.SportId);
            return View(players);
        }

        // GET: Players/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players.FindAsync(id);
            if (players == null)
            {
                return NotFound();
            }
            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "SportId", players.SportId);
            return View(players);
        }

        // POST: Players/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PlayerId,FullName,Age,Contry,SportId")] Players players)
        {
            if (id != players.PlayerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(players);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayersExists(players.PlayerId))
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
            ViewData["SportId"] = new SelectList(_context.Sport, "SportId", "SportId", players.SportId);
            return View(players);
        }

        // GET: Players/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var players = await _context.Players
                .Include(p => p.Sport)
                .FirstOrDefaultAsync(m => m.PlayerId == id);
            if (players == null)
            {
                return NotFound();
            }

            return View(players);
        }

        // POST: Players/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var players = await _context.Players.FindAsync(id);
            _context.Players.Remove(players);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlayersExists(int id)
        {
            return _context.Players.Any(e => e.PlayerId == id);
        }
    }
}
