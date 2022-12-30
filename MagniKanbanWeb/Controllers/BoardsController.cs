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
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;

namespace MagniKanbanWeb.Controllers
{
    [Authorize]
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
            .OrderBy(a => a.Order)
            .Include(a => a.Cards.OrderBy(a => a.Order));

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

            // var board = await _context.Boards.FindAsync(id);

            _context.Entry(boardModel).State = EntityState.Modified;

            try
            {
                // if(boardModel.Order != board?.Order)
                // {
                // 
                // }
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
        public async Task<ActionResult<Board>> PostBoard(BoardRequest boardRequest)
        {
            int order = _context.Boards.Where(a => a.ProjectId == boardRequest.ProjectId).Count();
            Board board = new Board { ProjectId = boardRequest.ProjectId, Title = boardRequest.Title, Order = order };
            _context.Boards.Add(board);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBoardModel", new { id = board.Id, projectId = board.ProjectId }, board);
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
