using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MagniKanbanWeb.Models;
using MagniKanbanWeb.Models.Requests;
using Microsoft.CodeAnalysis;
using Microsoft.AspNetCore.Authorization;
using NuGet.Protocol;

namespace MagniKanbanWeb.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Card>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Object>> GetCardsModel(int id)
        {
            var cardsModel = _context.Cards
                .Include(a => a.Comments)
                .Include(a => a.Files)
                .Include(a => a.Timeline.OrderByDescending(a => a.CreatedAt))
                .Include(a => a.Checklists)
                   .ThenInclude(a => a.ChecklistItems)
                .Where(a => a.Id == id)
                .ToList();

            if (cardsModel == null || cardsModel.Count == 0)
            {
                return NotFound();
            }

            foreach (var file in cardsModel[0].Files)
            {
                file.FileData = null;
            }

            return (object) cardsModel[0];
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardsModel(int id, Card cardsModel)
        {
            if (id != cardsModel.Id)
            {
                return BadRequest();
            }

            var card = _context.Cards.Find(id);

            foreach (string tag in card.Tags)
            {
                if (!cardsModel.Tags.Contains(tag))
                {
                    Timeline timeline = new Timeline { Title = tag + " tag removed", Type = "tag", CardId = id };
                    _context.Timelines.Add(timeline);
                }
            }

            foreach (string tag in cardsModel.Tags)
            {
                if (!card.Tags.Contains(tag))
                {
                    Timeline timeline = new Timeline { Title = tag + " tag added", Type = "tag", CardId = id };
                    _context.Timelines.Add(timeline);
                }
            }
            await _context.SaveChangesAsync();
            _context.Entry(card).State = EntityState.Detached;
            _context.Entry(cardsModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardsModelExists(id))
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

        // POST: api/Cards
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Card>> PostCardsModel(CardRequest cardRequest)
        {
            int order = _context.Cards.Where(a => a.BoardId == cardRequest.BoardId).Count();
            Card cardsModel = new Card { Title = cardRequest.Title, BoardId = cardRequest.BoardId, Order = order };
            _context.Cards.Add(cardsModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCardsModel", new { id = cardsModel.Id }, cardsModel);
        }

        // DELETE: api/Cards/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCardsModel(int id)
        {
            var cardsModel = await _context.Cards.FindAsync(id);
            if (cardsModel == null)
            {
                return NotFound();
            }

            _context.Cards.Remove(cardsModel);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CardsModelExists(int id)
        {
            return _context.Cards.Any(e => e.Id == id);
        }
    }
}
