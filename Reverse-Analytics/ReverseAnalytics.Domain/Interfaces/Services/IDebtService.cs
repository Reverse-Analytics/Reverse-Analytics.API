using ReverseAnalytics.Domain.DTOs.Debt;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IDebtService
    {
        public Task<IEnumerable<DebtDto>> GetAllDebtsAsync();
        public Task<IEnumerable<DebtDto>> GetAllDebtsByPersonIdAsync(int personId);
        public Task<DebtDto> GetByPersonAndDebtId(int personId, int debtId);
        public Task<DebtDto> GetDebtByIdAsync(int id);
        public Task<DebtDto> CreateDebtAsync(DebtForCreateDto debtToCreate);
        public Task UpdateDebtAsync(DebtForUpdateDto debtToUpdate);
        public Task DeleteDebtAsync(int id);
    }
}
