using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using super_ws.client;
using super_ws.client.Humble;
using super_ws.client.Quotes;
using super_ws.database.Repository;
using System.Net.WebSockets;

namespace super_ws.clientTest
{
    public class ClientAppTests
    {
        private readonly Mock<ISuperDbContextRepository> dbContextMock;
        private readonly Mock<IQuote> quote1Mock;
        private readonly Mock<IQuote> quote2Mock;
        private readonly Mock<IClientWebSocket> clientWebSocketMock;
        private readonly Mock<ILogger<ClientApp>> loggerMock;
        private readonly Mock<IConfigurationSection> sectionMock;
        private readonly Mock<IConfiguration> configurationMock;
        private readonly List<IQuote> quotes;

        public ClientAppTests()
        {
            dbContextMock = new Mock<ISuperDbContextRepository>();
            quote1Mock = new Mock<IQuote>();
            quote2Mock = new Mock<IQuote>();
            clientWebSocketMock = new Mock<IClientWebSocket>();
            loggerMock = new Mock<ILogger<ClientApp>>();
            sectionMock = new Mock<IConfigurationSection>();
            configurationMock = new Mock<IConfiguration>();
            quotes = new List<IQuote>();

        }


        [Fact]
        public async Task ClientAppShouldTryConnectToTheServer()
        {

            Setup(false);

            var app = new ClientApp(dbContextMock.Object, quotes, clientWebSocketMock.Object, configurationMock.Object, loggerMock.Object);

            await app.RunAsync();

            clientWebSocketMock.Verify(l => l.ConnectAsync(It.IsAny<Uri>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task ClientAppShouldSendSubscribtionMessageToTheServer()
        {
            Setup(false);

            var app = new ClientApp(dbContextMock.Object, quotes, clientWebSocketMock.Object, configurationMock.Object, loggerMock.Object);

            await app.RunAsync();

            clientWebSocketMock.Verify(l => l.SendAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<WebSocketMessageType>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task ClientAppShouldReceiveMessageFromTheServer()
        {
            Setup(false);

            var app = new ClientApp(dbContextMock.Object, quotes, clientWebSocketMock.Object, configurationMock.Object, loggerMock.Object);

            await app.RunAsync();

            clientWebSocketMock.Verify(l => l.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task ClientAppShouldTryToRecconectTheServerWhenConnectionFails()
        {

            Setup(true);

            var app = new ClientApp(dbContextMock.Object, quotes, clientWebSocketMock.Object, configurationMock.Object, loggerMock.Object);

            await Assert.ThrowsAsync<TaskCanceledException>(async () => await app.RunAsync());

            clientWebSocketMock.Verify(l => l.ConnectAsync(It.IsAny<Uri>(), It.IsAny<CancellationToken>()), Times.AtLeast(2));
        }

        private void Setup(bool throws)
        {
            if (throws)
            {
                clientWebSocketMock.Setup(c => c.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>())).Throws(new WebSocketException());
            }
            else
            {
                clientWebSocketMock.Setup(c => c.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new WebSocketReceiveResult(1, WebSocketMessageType.Close, true));
            }
            quotes.Add(quote1Mock.Object);
            quotes.Add(quote2Mock.Object);
            sectionMock.Setup(s => s.Value).Returns("https://www.fakeConnectionString.com");
            configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Returns(sectionMock.Object);
        }
    }
}