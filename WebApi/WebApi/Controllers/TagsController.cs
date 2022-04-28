﻿#nullable disable
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
    public class TagsController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public TagsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: api/Tags
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tag>>> GetTag()
        {
            return await _context.Tag.ToListAsync();
        }

        // GET: api/Tags/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tag>> GetTag(int id)
        {
            var tag = await _context.Tag.FindAsync(id);

            if (tag == null)
            {
                return NotFound();
            }

            return tag;
        }

        // PUT: api/Tags/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTag(int id, Tag tag)
        {
            if (id != tag.tagId)
            {
                return BadRequest();
            }

            _context.Entry(tag).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TagExists(id))
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

        // POST: api/Tags
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Tag>> PostTag(Tag tag)
        {
            _context.Tag.Add(tag);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTag", new { id = tag.tagId }, tag);
        }

        // DELETE: api/Tags/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTag(int id)
        {
            var tag = await _context.Tag.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }

            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TagExists(int id)
        {
            return _context.Tag.Any(e => e.tagId == id);
        }
    }
}
