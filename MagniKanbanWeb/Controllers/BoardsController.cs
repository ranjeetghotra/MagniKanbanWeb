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
    public class BoardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BoardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Boards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BoardModel>>> GetBoardModel()
        {
            return await _context.Boards.ToListAsync();
        }

        // GET: api/Boards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BoardModel>> GetBoardModel(int id)
        {
            var boardModel = await _context.Boards.FindAsync(id);

            if (boardModel == null)
            {
                return NotFound();
            }

            return boardModel;
        }

        // PUT: api/Boards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoardModel(int id, BoardModel boardModel)
        {
            if (id != boardModel.Id)
            {
                return BadRequest();
            }

            _context.Entry(boardModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BoardModelExists(id))
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

        // POST: api/Boards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BoardModel>> PostBoardModel(BoardModel boardModel)
        {
            _context.Boards.Add(boardModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoardModel", new { id = boardModel.Id }, boardModel);
        }

        // DELETE: api/Boards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBoardModel(int id)
        {
            var boardModel = await _context.Boards.FindAsync(id);
            if (boardModel == null)
            {
                return NotFound();
            }

            _context.Boards.Remove(boardModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BoardModelExists(int id)
        {
            return _context.Boards.Any(e => e.Id == id);
        }
    }
}
