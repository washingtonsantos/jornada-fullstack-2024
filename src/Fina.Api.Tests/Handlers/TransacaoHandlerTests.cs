using Fina.Api.Data;
using Fina.Api.Handlers;
using Fina.Core.Models;
using Fina.Core.Requests.Transacoes;
using Fina.Core.Responses;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Fina.Api.Tests.Handlers;

public class TransacaoHandlerTests
{
    [Fact]
    public async Task CreateAsync_ShouldReturnCreatedResponse_WhenRequestIsValid()
    {
        // Arrange
        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        using var context = new AppDbContext(options);
        var loggerMock = new Mock<ILogger<TransacaoHandler>>();

        var handler = new TransacaoHandler(context, loggerMock.Object);
        var sut = await handler.CreateAsync(new CriarTransacaoRequest
        {
            // Set up the request properties as needed
        });

        sut.Should().NotBeNull();
        sut.Should().BeOfType<Response<Transacao>>();
        sut.Should().Match<Response<Transacao>>(r => r.Sucesso == true);
    }
}
