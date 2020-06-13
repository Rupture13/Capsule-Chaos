using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerActionValidationService.Models;

namespace PlayerActionValidationService.Controllers
{
    [Route("api/validate")]
    [ApiController]
    public class ValidationsController : ControllerBase
    {
        private readonly PerformanceValidationContext _context;

        public ValidationsController(PerformanceValidationContext context)
        {
            _context = context;
        }

        // POST: api/validate
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostPerformanceValidation(PerformanceValidation performance)
        {
            if (!PerformanceValidationExists(performance.LevelId))
            {
                return NotFound();
            }

            var validation = await _context.PerformanceValidations.FirstOrDefaultAsync(p => p.LevelId == performance.LevelId);

            if (PerformanceMatchesValidation(performance, validation))
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        private bool PerformanceValidationExists(int levelId)
        {
            return _context.PerformanceValidations.Any(e => e.LevelId == levelId);
        }

        private bool PerformanceMatchesValidation(PerformanceValidation performance, PerformanceValidation validation)
        {
            return (performance.LevelId == validation.LevelId 
                && performance.MaximumScore == validation.MaximumScore 
                && performance.MinimumTime >= validation.MinimumTime);
        }
    }
}
