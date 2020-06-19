using System.Collections.Generic;
using System.Threading.Tasks;
using PlayerActionValidationService.Models;

namespace PlayerActionValidationService.Services
{
    public interface ICosmosDbService
    {
        Task<IEnumerable<PerformanceValidation>> GetItemsAsync();
        Task<PerformanceValidation> GetItemAsync(int levelId);
        Task<PerformanceValidation> AddItemAsync(PerformanceValidation validation);
        Task<PerformanceValidation> DeleteItemAsync(int id);
        Task<bool> PerformanceValidationExists(int levelId);
    }
}
