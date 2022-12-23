﻿using System;
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
    public class CardsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public CardsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Cards
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CardsModel>>> GetCards()
        {
            return await _context.Cards.ToListAsync();
        }

        // GET: api/Cards/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CardsModel>> GetCardsModel(int id)
        {
            var cardsModel = await _context.Cards.FindAsync(id);

            if (cardsModel == null)
            {
                return NotFound();
            }

            return cardsModel;
        }

        // PUT: api/Cards/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCardsModel(int id, CardsModel cardsModel)
        {
            if (id != cardsModel.Id)
            {
                return BadRequest();
            }

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
        public async Task<ActionResult<CardsModel>> PostCardsModel(CardsModel cardsModel)
        {
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