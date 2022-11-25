using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Phone;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class PhoneService : IPhoneService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public PhoneService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<PhoneDto>> GetAllPhonesAsync()
        {
            try
            {
                var phones = await _repository.Phone.FindAllAsync();

                var phoneDtos = _mapper.Map<IEnumerable<PhoneDto>>(phones);

                return phoneDtos;
            }
            catch
            {
                throw;
            }
        }
        
        public async Task<IEnumerable<PhoneDto>> GetAllByPersonIdAsync(int personId)
        {
            try
            {
                var phones = await _repository.Phone.FindAllByPersonIdAsync(personId);
                var phoneDtos = _mapper.Map<IEnumerable<PhoneDto>>(phones);

                return phoneDtos;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PhoneDto> GetByPersonAndPhoneIdAsync(int personId, int phoneId)
        {
            try
            {
                var phone = await _repository.Phone.FindByPersonAndPhoneId(personId, phoneId);
                var phoneDto = _mapper.Map<PhoneDto>(phone);

                return phoneDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PhoneDto> GetByIdAsync(int id)
        {
            try
            {
                var phone = await _repository.Phone.FindByIdAsync(id);

                var phoneDto = _mapper.Map<PhoneDto>(phone);

                return phoneDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task<PhoneDto> CreatePhoneAsync(PhoneForCreateDto phoneToCreate)
        {
            try
            {
                var phoneEntity = _mapper.Map<Phone>(phoneToCreate);

                var createdEntity = _repository.Phone.Create(phoneEntity);
                await _repository.SaveChangesAsync();

                var phoneDto = _mapper.Map<PhoneDto>(phoneEntity);

                return phoneDto;
            }
            catch
            {
                throw;
            }
        }

        public async Task UpdatePhoneAsync(PhoneForUpdateDto phoneToUpdate)
        {
            try
            {
                var phoneEntity = _mapper.Map<Phone>(phoneToUpdate);

                _repository.Phone.Update(phoneEntity);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task DeletePhoneAsync(int id)
        {
            try
            {
                _repository.Phone.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
        }
    }
}
