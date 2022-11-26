using ReverseAnalytics.Domain.DTOs.Phone;

namespace ReverseAnalytics.Domain.Interfaces.Services
{
    public interface IPhoneService
    {
        public Task<IEnumerable<PhoneDto>> GetAllPhonesAsync();
        public Task<PhoneDto> GetByPersonAndPhoneIdAsync(int personId, int phoneId);
        public Task<IEnumerable<PhoneDto>> GetAllByPersonIdAsync(int personId);
        public Task<PhoneDto> GetByIdAsync(int id);
        public Task<PhoneDto> CreatePhoneAsync(PhoneForCreateDto phoneToCreate);
        public Task UpdatePhoneAsync(PhoneForUpdateDto phoneToUpdate);
        public Task DeletePhoneAsync(int id);
    }
}
