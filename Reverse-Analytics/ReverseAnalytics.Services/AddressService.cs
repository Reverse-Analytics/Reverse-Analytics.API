using AutoMapper;
using ReverseAnalytics.Domain.DTOs.Address;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Domain.Interfaces.Services;

namespace ReverseAnalytics.Services
{
    public class AddressService : IAddressService
    {
        private readonly ICommonRepository _repository;
        private readonly IMapper _mapper;

        public AddressService(ICommonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<AddressDto>> GetAllAddressesAsync()
        {
            try
            {
                var addresses = await _repository.Address.FindAllAsync();

                var addressDtos = _mapper.Map<IEnumerable<AddressDto>>(addresses);

                return addressDtos;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AddressDto> GetAddressByIdAsync(int id)
        {
            try
            {
                var address = await _repository.Address.FindByIdAsync(id);

                var addressDto = _mapper.Map<AddressDto>(address);

                return addressDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AddressDto> GetAddressByPersonIdAsync(int personId)
        {
            try
            {
                var address = await _repository.Address.FindAllByPersonId(personId);

                var addressDto = _mapper.Map<AddressDto>(address);

                return addressDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<AddressDto> CreateAddressAsync(AddressForCreateDto addressToCreate)
        {
            try
            {
                if(addressToCreate is null)
                {
                    throw new ArgumentNullException(nameof(addressToCreate));
                }

                var addressEntity = _mapper.Map<Address>(addressToCreate);

                var createdEntity = _repository.Address.Create(addressEntity);
                await _repository.SaveChangesAsync();

                var addressDto = _mapper.Map<AddressDto>(createdEntity);

                return addressDto;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAddresAsync(AddressForUpdateDto addressToUpdate)
        {
            try
            {
                var addressEntity = _mapper.Map<Address>(addressToUpdate);

                _repository.Address.Update(addressEntity);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAddressAsync(int id)
        {
            try
            {
                _repository.Address.Delete(id);
                await _repository.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
