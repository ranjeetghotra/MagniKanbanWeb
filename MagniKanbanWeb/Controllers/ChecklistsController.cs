using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;
using Microsoft.CodeAnalysis;

namespace MagniKanbanWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChecklistsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Checklists/1
        [HttpGet("{cardId}")]
        public IQueryable<Object> GetChecklists(int cardId)
        {
            var checklists = _context.Checklists
            .Where(a => a.CardId == cardId)
            .Include(a => a.ChecklistItems);

            if (checklists == null)
            {
                return (IQueryable<object>)NotFound();
            }

            return checklists;
        }

        // GET: api/Checklists/1/5
        [HttpGet("{cardId}/{id}")]
        public async Task<ActionResult<Checklist>> GetChecklist(int cardId ,int id)
        {
            var checklist = await _context.Checklists.FindAsync(id);

            if (checklist == null || checklist.CardId != cardId)
            {
                return NotFound();
            }

            return checklist;
        }

        // PUT: api/Checklists/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChecklist(int id, Checklist checklist)
        {
            if (id != checklist.Id)
            {
                return BadRequest();
            }

            _context.Entry(checklist).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChecklistExists(id))
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

        // POST: api/Checklists
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Checklist>> PostChecklist(Checklist checklist)
        {
            _context.Checklists.Add(checklist);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChecklist", new { id = checklist.Id, cardId = checklist.CardId }, checklist);
        }

        // DELETE: api/Checklists/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChecklist(int id)
        {
            var checklist = await _context.Checklists.FindAsync(id);
            if (checklist == null)
            {
                return NotFound();
            }

            _context.Checklists.Remove(checklist);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChecklistExists(int id)
        {
            return _context.Checklists.Any(e => e.Id == id);
        }
    }
}
