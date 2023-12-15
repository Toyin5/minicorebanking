using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MiniCoreBanking.Application;
using MiniCoreBanking.Application.Dtos;
using MiniCoreBanking.Application.Interfaces;
using MiniCoreBanking.Application.Models;
using MiniCoreBanking.Domain.Entities;
using MiniCoreBanking.Infrastructure;
using Moq;

namespace MiniCoreBanking.Tests.AuthTests;
public class AuthServiceTests
{
    private readonly Mock<IJwtService> _jwtServiceMock = new Mock<IJwtService>();
    private readonly Mock<ILogger<AuthService>> _loggerMock = new Mock<ILogger<AuthService>>();
    private readonly Mock<ICustomerRepository> _repositoryMock = new Mock<ICustomerRepository>();
    private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

    // ... your test methods will go here

    [Fact]
    public async Task RegisterAsync_NewCustomer_Success()
    {
        // Arrange
        var authService = new AuthService(
            _jwtServiceMock.Object,
            _loggerMock.Object,
            _repositoryMock.Object,
            _mapperMock.Object
        );

        var request = new CustomerRequest
        {
            Email = "Customer",
            Password = "password",
            FirstName = "firstName",
            LastName = "lastName",
            PhoneNumber = "phoneNumber",
            IdNumber = 0,
            IdType = 1,
            UserName = "userName",
            Address = "address",
            // Set up request data here
        };

        _repositoryMock.Setup(m => m.GetQuerable().FirstOrDefaultAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
            .ReturnsAsync((Customer)null); // Simulate no existing customer with the same email and username

        _repositoryMock.Setup(m => m.AddAsync(It.IsAny<Customer>()));
        _repositoryMock.Setup(m => m.SaveChangesAsync()).ReturnsAsync(1); // Simulate successful save

        _mapperMock.Setup(m => m.Map<CustomerDto>(It.IsAny<Customer>())).Returns(new CustomerDto { Email = "email", Address = "address", });

        // Act
        var result = await authService.RegisterAsync(request);

        // Assert
        Assert.NotNull(result);
        // Add more assertions as needed
    }

}
