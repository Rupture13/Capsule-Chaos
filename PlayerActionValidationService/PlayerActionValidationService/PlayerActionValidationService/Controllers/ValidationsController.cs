using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PlayerActionValidationService.Models;
using PlayerActionValidationService.Services;

namespace PlayerActionValidationService.Controllers
{
    [Route("api/validate")]
    [ApiController]
    public class ValidationsController : ControllerBase
    {
        private readonly ICosmosDbService service;

        public ValidationsController(ICosmosDbService service)
        {
            this.service = service;
        }

        [HttpGet("Query")]
        public async Task<ActionResult<IEnumerable<PerformanceValidation>>> GetValidations()
        {
            return Ok(await service.GetItemsAsync());
        }

        // POST: api/validate
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult> PostPerformanceValidation(PerformanceValidation performance)
        {
            bool validationForLevelExists = await service.PerformanceValidationExists(performance.LevelId);
            if (!validationForLevelExists)
            {
                return NotFound();
            }

            var validation = await service.GetItemAsync(performance.LevelId);

            if (PerformanceMatchesValidation(performance, validation))
            {
                return Ok();
            }
            else
            {
                return Conflict();
            }
        }

        // DELETE: api/validate/add
        [HttpPost("Add")]
        public async Task<ActionResult<PerformanceValidation>> AddPerformanceValidation(PerformanceValidation performance)
        {
            await service.AddItemAsync(performance);

            return CreatedAtAction("GetAdvice", new { id = performance.Id }, performance);
        }

        // DELETE: api/validate/delete/5
        [HttpDelete("Delete/{id}")]
        public async Task<ActionResult<PerformanceValidation>> DeleteValidation(int id)
        {
            var advice = await service.GetItemAsync(id);
            if (advice == null)
            {
                return NotFound();
            }

            await service.DeleteItemAsync(id);

            return advice;
        }

        private bool PerformanceMatchesValidation(PerformanceValidation performance, PerformanceValidation validation)
        {
            return (performance.LevelId == validation.LevelId 
                && performance.MaximumScore == validation.MaximumScore 
                && performance.MinimumTime >= validation.MinimumTime);
        }
    }
}
