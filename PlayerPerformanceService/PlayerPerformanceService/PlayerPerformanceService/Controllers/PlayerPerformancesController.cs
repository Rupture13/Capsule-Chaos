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

        // GET: api/PlayerPerformances/Level/1/Player/2
        [HttpGet("Level/{levelId}/Player{playerId}")]
        public async Task<ActionResult<PlayerPerformance>> GetPlayerPerformance(int levelId, int playerId)
        {
            var playerPerformance = await _context.PlayerPerformances
                                                    .Include(p => p.Snapshots)
                                                    .Where(p => (p.LevelId == levelId && p.PlayerId == playerId))
                                                    .FirstOrDefaultAsync();

            if (playerPerformance == null)
            {
                return NotFound();
            }

            return playerPerformance;
        }

        // POST: api/PlayerPerformances
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PlayerPerformance>> PostPlayerPerformance(PlayerPerformance playerPerformance)
        {
            var oldPerformance = GetPlayerPerformanceFromLevel(playerPerformance.PlayerId, playerPerformance.LevelId);

            if (oldPerformance == null)
            {
                //There is no previous performance for this player in this level, creating one

                _context.PlayerPerformances.Add(playerPerformance);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetPlayerPerformance", new { levelId = playerPerformance.LevelId, playerId = playerPerformance.PlayerId }, playerPerformance);       //201
            }
            else
            {
                //There is a previous performance, new performance will overwrite old performance
                oldPerformance.Snapshots = playerPerformance.Snapshots;

                _context.Entry(oldPerformance).State = EntityState.Modified;

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlayerPerformanceExists(oldPerformance.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return Ok();            //200
            }
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

        private PlayerPerformance GetPlayerPerformanceFromLevel(int playerId, int levelId)
        {
            return _context.PlayerPerformances
                .Where(performance => performance.PlayerId == playerId && performance.LevelId == levelId)
                .FirstOrDefault();
        }
    }
}
