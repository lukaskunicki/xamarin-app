#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.DAL;
using WebApi.Model;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeptsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public DeptsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Depts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Depts>>> GetDept()
        {
            return await _context.Dept.ToListAsync();
        }

        // GET: api/Depts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Depts>> GetDepts(int id)
        {
            var depts = await _context.Dept.FindAsync(id);

            if (depts == null)
            {
                return NotFound();
            }

            return depts;
        }

        // PUT: api/Depts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDepts(int id, Depts depts)
        {
            if (id != depts.deptid)
            {
                return BadRequest();
            }

            _context.Entry(depts).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeptsExists(id))
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

        // POST: api/Depts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Depts>> PostDepts(Depts depts)
        {
            _context.Dept.Add(depts);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDepts", new { id = depts.deptid }, depts);
        }

        // DELETE: api/Depts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepts(int id)
        {
            var depts = await _context.Dept.FindAsync(id);
            if (depts == null)
            {
                return NotFound();
            }

            _context.Dept.Remove(depts);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeptsExists(int id)
        {
            return _context.Dept.Any(e => e.deptid == id);
        }
    }
}
