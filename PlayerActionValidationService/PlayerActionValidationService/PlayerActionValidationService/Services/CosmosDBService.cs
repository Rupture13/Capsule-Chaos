using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PlayerActionValidationService.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Fluent;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;

namespace PlayerActionValidationService.Services
{
    public class CosmosDbService : ICosmosDbService
    {
        private readonly PerformanceValidationContext context;

        public CosmosDbService(PerformanceValidationContext _context)
        {
            this.context = _context;
            // this will make sure that the database is created
            _context.Database.EnsureCreated();
        }

        //Get Validations
        public async Task<IEnumerable<PerformanceValidation>> GetItemsAsync()
        {
            var results = await context.PerformanceValidations.ToListAsync();
            return results;
        }

        //Get Validation by LevelID
        public async Task<PerformanceValidation> GetItemAsync(int levelId)
        {
            var result = await context.PerformanceValidations.FirstOrDefaultAsync(validation => validation.LevelId == levelId);
            return result;
        }

        //Add Validation
        public async Task<PerformanceValidation> AddItemAsync(PerformanceValidation validation)
        {
            validation.Id = new System.Guid();
            var response = await context.PerformanceValidations.AddAsync(validation);
            await context.SaveChangesAsync();
            return response.Entity;
        }

        //Delete Validation
        public async Task<PerformanceValidation> DeleteItemAsync(int levelId)
        {
            var validation = await context.PerformanceValidations.FirstOrDefaultAsync(v => v.LevelId == levelId);
            if (validation == null)
            {
                return null;
            }

            context.PerformanceValidations.Remove(validation);
            await context.SaveChangesAsync();

            return validation;
        }

        //Check if exists
        public async Task<bool> PerformanceValidationExists(int levelId)
        {
            return await context.PerformanceValidations.AnyAsync(validation => validation.LevelId == levelId);
        }
    }
}
