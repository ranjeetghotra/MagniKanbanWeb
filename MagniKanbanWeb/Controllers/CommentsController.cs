using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;

namespace MagniKanbanWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CommentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Comments
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommentsModel>>> GetComments()
        {
            return await _context.Comments.ToListAsync();
        }

        // GET: api/Comments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentsModel>> GetCommentsModel(int id)
        {
            var commentsModel = await _context.Comments.FindAsync(id);

            if (commentsModel == null)
            {
                return NotFound();
            }

            return commentsModel;
        }

        // PUT: api/Comments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCommentsModel(int id, CommentsModel commentsModel)
        {
            if (id != commentsModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(commentsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CommentsModelExists(id))
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

        // POST: api/Comments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<CommentsModel>> PostCommentsModel(CommentsModel commentsModel)
        {
            _context.Comments.Add(commentsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCommentsModel", new { id = commentsModel.Id }, commentsModel);
        }

        // DELETE: api/Comments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommentsModel(int id)
        {
            var commentsModel = await _context.Comments.FindAsync(id);
            if (commentsModel == null)
            {
                return NotFound();
            }

            _context.Comments.Remove(commentsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CommentsModelExists(int id)
        {
            return _context.Comments.Any(e => e.Id == id);
        }
    }
}
