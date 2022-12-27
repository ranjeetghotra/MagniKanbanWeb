using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;
using MagniKanbanWeb.Models.Requests;
using MagniKanbanWeb.Models.Responses;

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

        // GET: api/Boards/1
        [HttpGet("{projectId}")]
        public IQueryable<Object> GetBoardModel(int projectId)
        {

            var boardModel = _context.Boards
            .Where(a => a.ProjectId == projectId)
            .Include(a => a.Cards)
            .Select(a =>
                new
                {
                    Id = a.Id,
                    Title = a.Title.ToString(),
                    Cards = a.Cards.Where((b) => b.BoardId == a.Id).ToList()
                }
                );

            if (boardModel == null)
            {
                return (IQueryable<object>)NotFound();
            }

            return boardModel;
        }

        // GET: api/Boards/1/5
        [HttpGet("{projectId}/{id}")]
        public async Task<ActionResult<Object>> GetBoardModel(int projectId, int id)
        {
            var boardModel = _context.Boards
            .Include(a => a.Cards)
            .Select(a =>
                new
                {
                    Id = a.Id,
                    Title = a.Title.ToString(),
                    Cards = a.Cards.Where((b) => b.BoardId == a.Id).ToList()
                }
                ).Where(a => a.Id == id).ToList();

            if (boardModel == null || boardModel.Count == 0)
            {
                return NotFound();
            }

            return boardModel[0];
        }

        // PUT: api/Boards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBoardModel(int id, Board boardModel)
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
        public async Task<ActionResult<Board>> PostBoardModel(BoardRequest boardRequest)
        {
            Board board = new Board { ProjectId = boardRequest.ProjectId, Title = boardRequest.Title };
            _context.Boards.Add(board);
           await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoardModel", new { id = board.Id }, board);
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
