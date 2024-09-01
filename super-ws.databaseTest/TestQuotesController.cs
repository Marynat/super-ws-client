using Moq;
using super_ws.api.Controller;
using super_ws.api.Model;
using super_ws.api.Services;

namespace super_ws.databaseTest
{
    public class TestQuotesController
    {
        private readonly QuotesController _controller;

        private readonly Mock<IQuoteModelService> _modelService = new();

        public TestQuotesController()
        {
            _controller = new(_modelService.Object);
        }


        [Fact]
        public async Task GetQoutesShoudReturnQoutesWhitinTheRangeWhenDataExists()
        {
            _modelService.Setup(x => x.GetMinuteModelForQuoteInRangeAsync(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(new List<QuoteMinuteModel>() { new QuoteMinuteModel() { Close = (decimal)2.2, High = (decimal)3, Low = (decimal)1.4, Open = (decimal)1.7, Name = "EXTRA", Time = DateTimeOffset.Now, Volume = 22 } });
            var result = await _controller.GetQuotes("EXTRA", 100000, 200000);
            _modelService.Verify(x => x.GetMinuteModelForQuoteInRangeAsync(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()), Times.Once);
            Assert.NotEmpty(result);
        }
        [Fact]
        public async Task GetQoutesShoudReturnEmptyArrayWhenDataDoesNotExists()
        {
            _modelService.Setup(x => x.GetMinuteModelForQuoteInRangeAsync(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()))
                .ReturnsAsync(new List<QuoteMinuteModel>() { });
            var result = await _controller.GetQuotes("EXTRA", 100000, 200000);
            _modelService.Verify(x => x.GetMinuteModelForQuoteInRangeAsync(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()), Times.Once);
            Assert.Empty(result);
        }
    }
}