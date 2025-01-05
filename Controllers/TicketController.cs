using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UsersApp.Data;
using UsersApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;


namespace UsersApp.Controllers
{
    public class TicketController : Controller
    {
        private readonly AppDbContext _context;

        public TicketController(AppDbContext context)
        {
            _context = context;
        }

        // Index Action: List all tickets
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var tickets = await _context.Tickets
                .Include(t => t.Flight)
                .Include(t => t.Passenger)
                .ToListAsync();

            return View(tickets);
        }

        // Create Action: GET
        [Authorize]
        public IActionResult Create()

        {
            ViewData["Flights"] = new SelectList(_context.Flights, "Id", "FlightNumber");
            ViewData["Passengers"] = new SelectList(_context.Passengers, "Id", "FullName");
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Ticket ticket)
        {
            var flight = await _context.Flights.FindAsync(ticket.FlightId);

            ticket.TotalPrice = flight?.Price ?? 0;
            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }





        // Details Action
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Flight)
                .Include(t => t.Passenger)
                .FirstOrDefaultAsync(t => t.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Ticket/Delete/5
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ticket = await _context.Tickets
                .Include(t => t.Flight)
                .Include(t => t.Passenger)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return View(ticket);
        }

        // GET: Ticket/Edit/5
        [Authorize]
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

            ViewData["Flights"] = new SelectList(_context.Flights, "Id", "FlightNumber", ticket.FlightId);
            ViewData["Passengers"] = new SelectList(_context.Passengers, "Id", "FullName", ticket.PassengerId);

            return View(ticket);
        }

        // POST: Ticket/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ticket ticket)
        {
            if (id != ticket.Id)
            {
                return NotFound();
            }

           
                    _context.Update(ticket);
                    await _context.SaveChangesAsync();
               
               
                return RedirectToAction(nameof(Index));
            

            ViewData["Flights"] = new SelectList(_context.Flights, "Id", "FlightNumber", ticket.FlightId);
            ViewData["Passengers"] = new SelectList(_context.Passengers, "Id", "FullName", ticket.PassengerId);

            return View(ticket);
        }


        // POST: Ticket/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ticket = await _context.Tickets.FindAsync(id);
            if (ticket != null)
            {
                _context.Tickets.Remove(ticket);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }



    }
}