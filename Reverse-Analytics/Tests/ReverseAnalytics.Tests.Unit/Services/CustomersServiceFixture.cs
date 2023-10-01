using AutoMapper;
using Moq;
using ReverseAnalytics.Domain.DTOs.Customer;
using ReverseAnalytics.Domain.Entities;
using ReverseAnalytics.Domain.Exceptions;
using ReverseAnalytics.Domain.Interfaces.Repositories;
using ReverseAnalytics.Services;

namespace ReverseAnalytics.Tests.Unit.Services
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _customerService;
        private readonly Mock<ICommonRepository> _repositoryMock;
        private readonly Mock<IMapper> _mapperMock;

        public CustomerServiceTests()
        {
            _repositoryMock = new Mock<ICommonRepository>();
            _mapperMock = new Mock<IMapper>();
            _customerService = new CustomerService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task GetAllCustomerAsync_ReturnsExpectedCustomers()
        {
            // Arrange
            var customers = new List<Customer> { new Customer { Id = 1, FullName = "John Doe" } };
            var customerDtos = new List<CustomerDto> { new CustomerDto { Id = 1, FullName = "John Doe" } };
            _repositoryMock.Setup(r => r.Customer.FindAllCustomers(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(customers);
            _mapperMock.Setup(m => m.Map<IEnumerable<CustomerDto>>(customers)).Returns(customerDtos);

            // Act
            var result = await _customerService.GetAllCustomerAsync(null, 1, 10);

            // Assert
            Assert.Single(result);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ReturnsCustomer()
        {
            // Arrange
            int customerId = 1;
            var customer = new Customer { Id = customerId, FullName = "John Doe" };
            var customerDto = new CustomerDto { Id = customerId, FullName = "John Doe" };
            _repositoryMock.Setup(r => r.Customer.FindByIdAsync(customerId)).ReturnsAsync(customer);
            _mapperMock.Setup(m => m.Map<CustomerDto>(customer)).Returns(customerDto);

            // Act
            var result = await _customerService.GetCustomerByIdAsync(customerId);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(customerId, result.Id);
        }

        [Fact]
        public async Task GetCustomerByIdAsync_ThrowsNotFoundException()
        {
            // Arrange
            int customerId = 1;
            _repositoryMock.Setup(r => r.Customer.FindByIdAsync(customerId)).ReturnsAsync((Customer)null);

            // Act & Assert
            await Assert.ThrowsAsync<NotFoundException>(() => _customerService.GetCustomerByIdAsync(customerId));
        }

        [Fact]
        public async Task CreateCustomerAsync_CreatesValidCustomer()
        {
            // Arrange
            var customerToCreate = new CustomerForCreateDto { FullName = "John Doe" };
            var customer = new Customer { Id = 1, FullName = "John Doe" };
            var customerDto = new CustomerDto { Id = 1, FullName = "John Doe" };
            _mapperMock.Setup(m => m.Map<Customer>(customerToCreate)).Returns(customer);
            _repositoryMock.Setup(r => r.Customer.Create(customer)).Returns(customer);
            _repositoryMock.Setup(r => r.Customer.SaveChangesAsync()).Returns(Task.FromResult(true));
            _mapperMock.Setup(m => m.Map<CustomerDto>(customer)).Returns(customerDto);

            // Act
            var result = await _customerService.CreateCustomerAsync(customerToCreate);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Id);
            _repositoryMock.Verify(r => r.Customer.Create(customer), Times.Once);
        }

        [Fact]
        public async Task UpdateCustomerAsync_UpdatesValidCustomer()
        {
            // Arrange
            var customerToUpdate = new CustomerForUpdateDto { Id = 1, FullName = "Updated Name" };
            var customer = new Customer { Id = 1, FullName = "Updated Name" };
            _mapperMock.Setup(m => m.Map<Customer>(customerToUpdate)).Returns(customer);
            _repositoryMock.Setup(r => r.Customer.Update(customer));
            _repositoryMock.Setup(r => r.Customer.SaveChangesAsync()).Returns(Task.FromResult(true));

            // Act
            await _customerService.UpdateCustomerAsync(customerToUpdate);

            // Assert
            _repositoryMock.Verify(r => r.Customer.Update(customer), Times.Once);
        }

        [Fact]
        public async Task DeleteCustomerAsync_DeletesValidCustomer()
        {
            // Arrange
            int customerId = 1;
            _repositoryMock.Setup(r => r.Customer.Delete(customerId));
            _repositoryMock.Setup(r => r.Customer.SaveChangesAsync()).Returns(Task.FromResult(true));

            // Act
            await _customerService.DeleteCustomerAsync(customerId);

            // Assert
            _repositoryMock.Verify(r => r.Customer.Delete(customerId), Times.Once);
        }
    }
}