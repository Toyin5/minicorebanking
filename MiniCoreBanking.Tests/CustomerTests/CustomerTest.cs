using MiniCoreBanking.Api;
using MiniCoreBanking.Application.Dtos;
using MiniCoreBanking.Domain.Entities;
using MiniCoreBanking.Infrastructure.Repositories;
using Moq;

public class CustomerTest
{

    [Fact]
    public async Task GetCustomer_Returns_CustomerDto()
    {
        var repositoryMock = new Mock<CustomerRepository>();
        // CustomerDto customer = new ("Customer", Address:"dd@example.com", UserName:"jdj");
        repositoryMock.Setup(r => r.GetAsync(new Guid("00bcca61-fb73-4b53-b6a9-c468256d950f"))).Returns(Task.FromResult());

        var controller = new CustomerControllers(repositoryMock.Object);

        var result = await controller.GetTitle("978-0-10074-5");
        Assert.Equal(title, result.Value);
    }
}