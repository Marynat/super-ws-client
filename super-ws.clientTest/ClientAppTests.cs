using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using super_ws.client;
using super_ws.client.Humble;
using super_ws.client.Quotes;
using super_ws.database;
using super_ws.database.Repository;
using System.Net.WebSockets;

namespace super_ws.clientTest
{
    public class ClientAppTests
    {
        [Fact]
        public async Task ClientAppShouldTryConnectToTheServer()
        {
            var dbContextMock = new Mock<ISuperDbContextRepository>();
            var quote1Mock = new Mock<IQuote>();
            var quote2Mock = new Mock<IQuote>();
            var qoutes = new List<IQuote>() { quote1Mock.Object, quote2Mock.Object };
            var clientWebSocketMock = new Mock<IClientWebSocket>();
            clientWebSocketMock.Setup(c => c.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new WebSocketReceiveResult(1, WebSocketMessageType.Close, true));
            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s.Value).Returns("https://www.fakeConnectionString.com");
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Returns(sectionMock.Object);
            var logger = new Mock<ILogger<ClientApp>>();

            var app = new ClientApp(dbContextMock.Object, qoutes, clientWebSocketMock.Object, configurationMock.Object, logger.Object);

            await app.RunAsync();

            clientWebSocketMock.Verify(l => l.ConnectAsync(It.IsAny<Uri>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task ClientAppShouldSendSubscribtionMessageToTheServer()
        {
            var dbContextMock = new Mock<ISuperDbContextRepository>();
            var quote1Mock = new Mock<IQuote>();
            var quote2Mock = new Mock<IQuote>();
            var qoutes = new List<IQuote>() { quote1Mock.Object, quote2Mock.Object };
            var clientWebSocketMock = new Mock<IClientWebSocket>();
            clientWebSocketMock.Setup(c => c.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new WebSocketReceiveResult(1, WebSocketMessageType.Close, true));
            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s.Value).Returns("https://www.fakeConnectionString.com");
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Returns(sectionMock.Object);
            var logger = new Mock<ILogger<ClientApp>>();

            var app = new ClientApp(dbContextMock.Object, qoutes, clientWebSocketMock.Object, configurationMock.Object, logger.Object);

            await app.RunAsync();

            clientWebSocketMock.Verify(l => l.SendAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<WebSocketMessageType>(), It.IsAny<bool>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task ClientAppShouldReceiveMessageFromTheServer()
        {
            var dbContextMock = new Mock<ISuperDbContextRepository>();
            var quote1Mock = new Mock<IQuote>();
            var quote2Mock = new Mock<IQuote>();
            var qoutes = new List<IQuote>() { quote1Mock.Object, quote2Mock.Object };
            var clientWebSocketMock = new Mock<IClientWebSocket>();
            clientWebSocketMock.Setup(c => c.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>())).ReturnsAsync(new WebSocketReceiveResult(1, WebSocketMessageType.Close, true));
            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s.Value).Returns("https://www.fakeConnectionString.com");
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Returns(sectionMock.Object);
            var logger = new Mock<ILogger<ClientApp>>();

            var app = new ClientApp(dbContextMock.Object, qoutes, clientWebSocketMock.Object, configurationMock.Object, logger.Object);

            await app.RunAsync();

            clientWebSocketMock.Verify(l => l.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>()), Times.AtLeastOnce());
        }

        [Fact]
        public async Task ClientAppShouldTryToRecconectTheServerWhenConnectionFails()
        {
            var dbContextMock = new Mock<ISuperDbContextRepository>();
            var quote1Mock = new Mock<IQuote>();
            var quote2Mock = new Mock<IQuote>();
            var qoutes = new List<IQuote>() { quote1Mock.Object, quote2Mock.Object };
            var clientWebSocketMock = new Mock<IClientWebSocket>();
            clientWebSocketMock.Setup(c => c.ReceiveAsync(It.IsAny<ArraySegment<byte>>(), It.IsAny<CancellationToken>())).Throws(new WebSocketException());
            var sectionMock = new Mock<IConfigurationSection>();
            sectionMock.Setup(s => s.Value).Returns("https://www.fakeConnectionString.com");
            var configurationMock = new Mock<IConfiguration>();
            configurationMock.Setup(c => c.GetSection(It.IsAny<string>())).Returns(sectionMock.Object);
            var logger = new Mock<ILogger<ClientApp>>();

            var app = new ClientApp(dbContextMock.Object, qoutes, clientWebSocketMock.Object, configurationMock.Object, logger.Object);

            await Assert.ThrowsAsync<TaskCanceledException>(async () => await app.RunAsync());

            clientWebSocketMock.Verify(l => l.ConnectAsync(It.IsAny<Uri>(), It.IsAny<CancellationToken>()), Times.AtLeast(2));
        }
    }
}