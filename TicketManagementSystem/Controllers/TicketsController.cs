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
using System.Security.Claims;

namespace TicketManagementSystem.Controllers
{
    public class TicketsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<TicketManagementUser> _userManager;

        public TicketsController(ApplicationDbContext context,UserManager<TicketManagementUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Tickets
        public async Task<IActionResult> Index()
        {
            var model = await _context.Tickets.Include(c=> c.AssignedTo).ToListAsync();// include's helped to solve the null reference exception errors while displaying CreatedBy and AssignedTo in corresponding view
            model = await _context.Tickets.Include(d => d.CreatedBy).ToListAsync();
           
            return View(model);
        }

        // GET: Tickets/Details/5
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

        public async Task<IActionResult> AssignToMe(int? id)
        {
            
            if (id == null)
            {
                return NotFound();
            }

            _context.Tickets.Include("AssignedTo");

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AssignToMe(int id, [Bind("Id,Title,Statement,DateCreated,TicketStatus,DateModified,Priority,ConclusionText,AssignedToId,AssignedTo")] Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var user = await GetCurrentUserAsync();
                    ticket.AssignedToId = user.Id;
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



        // GET: Tickets/Create
        public IActionResult Create()
        {
            //var user = this.User.FindFirstValue(ClaimTypes.NameIdentifier);

            //var model = _userManager.GetUserId(HttpContext.User);
            

            return View();
        }

       

        private Task<TicketManagementUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);//get the current logged in user's Id.

       

        // POST: Tickets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Statement,DateCreated,TicketStatus,DateModified,Priority,ConclusionText")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {               
                var user = await GetCurrentUserAsync();               
                ticket.CreatedBy = user;

                ActivityLog newLogForTicket = new ActivityLog();
                newLogForTicket.Ticket = ticket;
                newLogForTicket.Name = user.Name; // Activity log will keep the user which has modified,deleted or assigned the ticket.
                newLogForTicket.ActionType = ActionType.Created;//this will be logged as 0 to database which is "created" ActionType
                newLogForTicket.Date = DateTime.Now;
                

             
                _context.Add(ticket);
                await _context.SaveChangesAsync();

                _context.Add(newLogForTicket);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(ticket);
        }



        public async Task<IActionResult> Assign(int? id)
        {

            var employees = _userManager.Users.ToList();

            ViewBag.Employees = employees;

            if (id == null)
            {
                return NotFound();
            }

            _context.Tickets.Include("AssignedTo");

            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(int id, [Bind("Id,Title,Statement,DateCreated,TicketStatus,DateModified,Priority,ConclusionText,AssignedToId,AssignedTo")] Ticket ticket)
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

        // GET: Tickets/Edit/5
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


        // POST: Tickets/Edit/5
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

        // GET: Tickets/Delete/5
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

        // POST: Tickets/Delete/5
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
