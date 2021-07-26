using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TicketManagementSystem.Data;
using TicketManagementSystem.Models.Tables;

namespace TicketManagementSystem.Controllers
{
    public class InProgressTicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<TicketManagementUser> _userManager;

        public InProgressTicketsController(ApplicationDbContext context, UserManager<TicketManagementUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        private Task<TicketManagementUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);

        // GET: InProgressTickets
        public async Task<IActionResult> Index()
        {

            if (_userManager.GetUserId(User) == null) // if we try to log out while in "görev listesi" tab,program will throw argumentNullException because there arent any logged in users.So we prevent it by adding this logic
            {
                return Redirect("~/Home/Index");
            }

            List<Ticket> filteredTickets= new List<Ticket>();
 
            TicketManagementUser currentUser = await GetCurrentUserAsync();

           
            filteredTickets = await _context.Tickets.Include("AssignedTo").Where(x => x.AssignedTo != null &&  x.AssignedTo.Id == currentUser.Id).ToListAsync();
            filteredTickets = await _context.Tickets.Include("CreatedBy").Where(x => x.AssignedTo != null && x.AssignedTo.Id == currentUser.Id).ToListAsync();
      //get all ticket info if it is assigned to the current user(employee).Include helped with the null reference exceptions


      ViewBag.Logs = await _context.ActivityLogs.Include(c => c.Ticket).ToListAsync();

      return View(filteredTickets);
        }

        // GET: InProgressTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: InProgressTickets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: InProgressTickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Statement,DateCreated,TicketStatus,DateModified,Priority,ConclusionText")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }

        // GET: InProgressTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        // POST: InProgressTickets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Statement,DateCreated,TicketStatus,DateModified,Priority,ConclusionText")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TicketExists(ticket.Id))
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
            return View(ticket);
        }

        // GET: InProgressTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // POST: InProgressTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}
