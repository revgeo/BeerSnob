using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BeerSnob.Models;

namespace BeerSnob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeersController : ControllerBase
    {
        private readonly BeerContext _context;

        public BeersController(BeerContext context)
        {
            _context = context;
        }

        // GET: api/Beers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BeerDTO>>> GetBeers()
        {
            return await _context.Beers
                .Select(x => BeerToSnobOn(x))
                .ToListAsync();
        }

        // GET: api/Beers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDTO>> GetBeer(long id)
        {
            var beer = await _context.Beers.FindAsync(id);

            if (beer == null)
            {
                return NotFound();
            }

            return BeerToSnobOn(beer);
        }

        // PUT: api/Beers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBeer(long id, BeerDTO beerDTO)
        {
            if (id != beerDTO.ID)
            {
                return BadRequest();
            }

            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            beer.Name = beerDTO.Name;
            beer.Brewery = beerDTO.Brewery;
            beer.ABV = beerDTO.ABV;
            beer.Style = beerDTO.Style;
            beer.IsGood = beerDTO.IsGood;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!BeerExists(id))
            {
                    return NotFound();
            }

            return NoContent();
        }

        // POST: api/Beers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<BeerDTO>> CreateBeer(BeerDTO beerDTO)
        {
            var beer = new Beer
            {
                Name = beerDTO.Name,
                Brewery = beerDTO.Brewery,
                ABV = beerDTO.ABV,
                Style = beerDTO.Style,
                IsGood = beerDTO.IsGood,
            };

            _context.Beers.Add(beer);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetBeer), 
                new { id = beer.ID }, 
                BeerToSnobOn(beer));
        }

        // DELETE: api/Beers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBeer(long id)
        {
            var beer = await _context.Beers.FindAsync(id);
            if (beer == null)
            {
                return NotFound();
            }

            _context.Beers.Remove(beer);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BeerExists(long id)
        {
            return _context.Beers.Any(e => e.ID == id);
        }

        private static BeerDTO BeerToSnobOn(Beer beer) =>
            new BeerDTO
            {
                ID = beer.ID,
                Name = beer.Name,
                Brewery = beer.Brewery,
                ABV = beer.ABV,
                Style = beer.Style,
                IsGood = beer.IsGood,
            };
    }
}
