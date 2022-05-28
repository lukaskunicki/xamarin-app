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
    public class SprintsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;

        public SprintsController(ApplicationDBContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Sprints
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sprint>>> GetSprint()
        {
            var sprints = _context.Sprint
                .ToList();

            var getSprints = _mapper.Map<IList<Sprint>>(sprints);

            return Ok(getSprints);
        }

        // GET: api/Sprints/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Sprint>> GetSprint(int id)
        {
            var result = _context.Sprint
                .Where(s => s.sprintId == id)
                .FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            var getSprint = _mapper.Map<Sprint>(result);

            return Ok(getSprint);
        }

        // PUT: api/Sprints/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSprint(int id, Sprint sprint)
        {
            if (id != sprint.sprintId)
            {
                return BadRequest();
            }

            _context.Entry(sprint).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SprintExists(id))
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

        // POST: api/Sprints
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Sprint>> PostSprint(Sprint sprint)
        {
            _context.Sprint.Add(sprint);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSprint", new { id = sprint.sprintId }, sprint);
        }

        // DELETE: api/Sprints/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSprint(int id)
        {
            var sprint = await _context.Sprint.FindAsync(id);
            if (sprint == null)
            {
                return NotFound();
            }

            _context.Sprint.Remove(sprint);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SprintExists(int id)
        {
            return _context.Sprint.Any(e => e.sprintId == id);
        }
    }
}
