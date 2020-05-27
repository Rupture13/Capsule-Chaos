using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerPerformanceService.Models;

namespace PlayerPerformanceService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlayerPerformancesController : ControllerBase
    {
        private readonly PlayerPerformanceContext _context;

        public PlayerPerformancesController(PlayerPerformanceContext context)
        {
            _context = context;
        }

        // GET: api/PlayerPerformances
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PlayerPerformance>>> GetPlayerPerformances()
        {
            return await _context.PlayerPerformances
                .Include(p => p.Snapshots)
                .ToListAsync();
        }

        // GET: api/PlayerPerformances/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PlayerPerformance>> GetPlayerPerformance(int id)
        {
            var playerPerformance = await _context.PlayerPerformances.FindAsync(id);

            if (playerPerformance == null)
            {
                return NotFound();
            }

            return playerPerformance;
        }

        // PUT: api/PlayerPerformances/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPlayerPerformance(int id, PlayerPerformance playerPerformance)
        {
            if (id != playerPerformance.Id)
            {
                return BadRequest();
            }

            _context.Entry(playerPerformance).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerPerformanceExists(id))
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

        // POST: api/PlayerPerformances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlayerPerformance>> PostPlayerPerformance(PlayerPerformance playerPerformance)
        {
            _context.PlayerPerformances.Add(playerPerformance);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayerPerformance", new { id = playerPerformance.Id }, playerPerformance);
        }

        // DELETE: api/PlayerPerformances/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PlayerPerformance>> DeletePlayerPerformance(int id)
        {
            var playerPerformance = await _context.PlayerPerformances.FindAsync(id);
            if (playerPerformance == null)
            {
                return NotFound();
            }

            _context.PlayerPerformances.Remove(playerPerformance);
            await _context.SaveChangesAsync();

            return playerPerformance;
        }

        private bool PlayerPerformanceExists(int id)
        {
            return _context.PlayerPerformances.Any(e => e.Id == id);
        }
    }
}
