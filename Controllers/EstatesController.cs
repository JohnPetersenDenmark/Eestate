using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Eestate.Models;
using Eestate.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Eestate.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EstatesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EstatesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Estates
        [HttpGet]
        [Produces("application/json")]
        public async Task<ActionResult<List<EstateViewModel>>> GetEstates()
        {
            List<EstateViewModel> estateModels = new List<EstateViewModel>();

            List<Estate> estates =  await _context.Estates.ToListAsync();

            foreach( var estate in estates)
            {
                EstateViewModel model = new EstateViewModel();
                model.Id = estate.Id.ToString();
                model.RegistrationNumber = estate.RegistrationNumber;
                model.OwnerIdentityUserIds = estate.OwnerIdentityUserIds.ToString();
                model.BuyerIdentityUserIds = estate.BuyerIdentityUserIds.ToString();
                model.CreatedDate = estate.CreatedDate.ToShortDateString();
                model.ModifiedDate = estate.ModifiedDate.ToShortDateString();

                estateModels.Add(model);
            }

            return estateModels;
        }

        // GET: api/Estates/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estate>> GetEstate(int id)
        {
            var estate = await _context.Estates.FindAsync(id);

            if (estate == null)
            {
                return NotFound();
            }

            return estate;
        }

        // PUT: api/Estates/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstate(int id, Estate estate)
        {
            if (id != estate.Id)
            {
                return BadRequest();
            }

            _context.Entry(estate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstateExists(id))
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

        // POST: api/Estates
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Estate>> PostEstate(Estate estate)
        {
            _context.Estates.Add(estate);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetEstate", new { id = estate.Id }, estate);
        }

        // DELETE: api/Estates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEstate(int id)
        {
            var estate = await _context.Estates.FindAsync(id);
            if (estate == null)
            {
                return NotFound();
            }

            _context.Estates.Remove(estate);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EstateExists(int id)
        {
            return _context.Estates.Any(e => e.Id == id);
        }
    }
}
