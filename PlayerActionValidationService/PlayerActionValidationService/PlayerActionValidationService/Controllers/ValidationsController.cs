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
    [Route("api/[controller]")]
    [ApiController]
    public class ValidationsController : ControllerBase
    {
        private readonly PerformanceValidationContext _context;

        public ValidationsController(PerformanceValidationContext context)
        {
            _context = context;
        }

        // GET: api/Validations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PerformanceValidation>>> GetPerformanceValidations()
        {
            return await _context.PerformanceValidations.ToListAsync();
        }

        // GET: api/Validations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PerformanceValidation>> GetPerformanceValidation(int id)
        {
            var performanceValidation = await _context.PerformanceValidations.FindAsync(id);

            if (performanceValidation == null)
            {
                return NotFound();
            }

            return performanceValidation;
        }

        // PUT: api/Validations/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPerformanceValidation(int id, PerformanceValidation performanceValidation)
        {
            if (id != performanceValidation.Id)
            {
                return BadRequest();
            }

            _context.Entry(performanceValidation).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PerformanceValidationExists(id))
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

        // POST: api/Validations
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<PerformanceValidation>> PostPerformanceValidation(PerformanceValidation performanceValidation)
        {
            _context.PerformanceValidations.Add(performanceValidation);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPerformanceValidation", new { id = performanceValidation.Id }, performanceValidation);
        }

        // DELETE: api/Validations/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<PerformanceValidation>> DeletePerformanceValidation(int id)
        {
            var performanceValidation = await _context.PerformanceValidations.FindAsync(id);
            if (performanceValidation == null)
            {
                return NotFound();
            }

            _context.PerformanceValidations.Remove(performanceValidation);
            await _context.SaveChangesAsync();

            return performanceValidation;
        }

        private bool PerformanceValidationExists(int id)
        {
            return _context.PerformanceValidations.Any(e => e.Id == id);
        }
    }
}
