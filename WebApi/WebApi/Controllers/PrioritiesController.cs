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
    public class PrioritiesController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public PrioritiesController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Priorities
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Priority>>> GetPriority()
        {
            var priorities = _context.Priority
                .ToList();

            var getPriorities = _mapper.Map<IList<Priority>>(priorities);

            return Ok(getPriorities);
        }

        // GET: api/Priorities/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Priority>> GetPriority(int id)
        {
            var result = _context.Priority
                .Where(s => s.priorityId == id)
                .FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            var getPriority = _mapper.Map<Priority>(result);

            return Ok(getPriority);
        }

        // PUT: api/Priorities/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPriority(int id, Priority priority)
        {
            if (id != priority.priorityId)
            {
                return BadRequest();
            }

            _context.Entry(priority).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PriorityExists(id))
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

        // POST: api/Priorities
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Priority>> PostPriority(Priority priority)
        {
            _context.Priority.Add(priority);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPriority", new { id = priority.priorityId }, priority);
        }

        // DELETE: api/Priorities/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePriority(int id)
        {
            var priority = await _context.Priority.FindAsync(id);
            if (priority == null)
            {
                return NotFound();
            }

            _context.Priority.Remove(priority);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PriorityExists(int id)
        {
            return _context.Priority.Any(e => e.priorityId == id);
        }
    }
}
