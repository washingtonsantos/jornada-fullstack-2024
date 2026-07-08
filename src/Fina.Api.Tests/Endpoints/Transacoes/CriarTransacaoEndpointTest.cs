using Fina.Core.Handlers;
using Fina.Core.Requests.Transacoes;

namespace Fina.Api.Tests.Endpoints.Transacoes
{
    public class CriarTransacaoEndpointTest
    {
        private readonly Mock<ITransacaoHandler> handlerMock = new Mock<ITransacaoHandler>();

        [Fact]
        public async Task HandleAsync()
        {
            var request = new CriarTransacaoRequest
            {

            };

            var result = await handlerMock.Object.CreateAsync(request);

            // Assert
            result.Should().Be(true);
        }
    }
}
