#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public TicketsController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Tickets
        [HttpGet]
        public ActionResult<IEnumerable<Ticket>> GetTicket()
        {
            var tickets = _context.Ticket
                .Include(i => i.priority)
                .Include(i => i.assignedEmployee)
                .Include(i => i.assignedEmployee.team)
                .Include(i => i.reporter)
                .Include(i => i.reporter.team)
                .ToList();

            var getTickets = _mapper.Map<IList<Ticket>>(tickets);

            return Ok(getTickets);
        }

        // GET: api/Tickets/5
        [HttpGet("{id}")]
        public ActionResult<Ticket> GetTicket(int id)
        {
            var result = _context.Ticket
                .Where(s => s.ticketId == id)
                .Include(i => i.assignedEmployee)
                .Include(i => i.assignedEmployee.team)
                .Include(i => i.reporter)
                .Include(i => i.reporter.team)
                .FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            var getTicket = _mapper.Map<Ticket>(result);

            return Ok(getTicket);
        }

        // PUT: api/Tickets/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket(int id, Ticket ticket)
        {
            if (id != ticket.ticketId)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Tickets
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ticket>> PostTicket(Ticket ticket)
        {
            _context.Ticket.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.ticketId }, ticket);
        }

        // DELETE: api/Tickets/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket(int id)
        {
            var ticket = await _context.Ticket.FindAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Ticket.Remove(ticket);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TicketExists(int id)
        {
            return _context.Ticket.Any(e => e.ticketId == id);
        }
    }
}
