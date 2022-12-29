using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;
using Microsoft.CodeAnalysis;
using MagniKanbanWeb.Migrations;
using MagniKanbanWeb.Models.Requests;
using Microsoft.AspNetCore.Authorization;

namespace MagniKanbanWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistItemsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ChecklistItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ChecklistItems/
        [HttpGet("{checklistId}")]
        public IQueryable<Object> GetChecklistItems(int checklistId)
        {
            var checklistItems = _context.ChecklistItems
           .Where(a => a.ChecklistId == checklistId);
            if (checklistItems == null)
            {
                return (IQueryable<object>)NotFound();
            }

            return checklistItems;
        }

        // GET: api/ChecklistItems/5/
        [HttpGet("{checklistId}/{id}")]
        public async Task<ActionResult<ChecklistItem>> GetChecklistItem(int checklistId, int id)
        {
            var checklistItem = await _context.ChecklistItems.FindAsync(id);

            if (checklistItem == null || checklistItem.ChecklistId != checklistId)
            {
                return NotFound();
            }

            return checklistItem;
        }

        // PUT: api/ChecklistItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutChecklistItem(int id, ChecklistItem checklistItem)
        {
            if (id != checklistItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(checklistItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChecklistItemExists(id))
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

        // POST: api/ChecklistItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ChecklistItem>> PostChecklistItem(ChecklistItemRequest request)
        {
            ChecklistItem checklistItem = new ChecklistItem { Title = request.Title, ChecklistId = request.ChecklistId };
            _context.ChecklistItems.Add(checklistItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChecklistItem", new { id = checklistItem.Id, checklistId = checklistItem.ChecklistId }, checklistItem);
        }

        // DELETE: api/ChecklistItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteChecklistItem(int id)
        {
            var checklistItem = await _context.ChecklistItems.FindAsync(id);
            if (checklistItem == null)
            {
                return NotFound();
            }

            _context.ChecklistItems.Remove(checklistItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChecklistItemExists(int id)
        {
            return _context.ChecklistItems.Any(e => e.Id == id);
        }
    }
}
