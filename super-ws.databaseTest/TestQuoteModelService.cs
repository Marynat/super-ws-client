using AutoMapper;
using Moq;
using super_ws.api.Model;
using super_ws.api.Services;
using super_ws.database.Entity;
using super_ws.database.Repository;

namespace super_ws.databaseTest;

public class TestQuoteModelService
{
    [Fact]
    public async Task GetMinuteModelForQuoteInRangeAsyncShouldCallrepositoryToFetchData()
    {
        var mockRepository = new Mock<ISuperDbContextRepository>();
        var mockMapper = new Mock<IMapper>();
        var service = new QuoteModelService(mockRepository.Object, mockMapper.Object);

        await service.GetMinuteModelForQuoteInRangeAsync("ASDF", 1000000, 2000000);

        mockRepository.Verify(x => x.GetQuoteMinutesForRange(It.IsAny<string>(), It.IsAny<DateTimeOffset>(), It.IsAny<DateTimeOffset>()), Times.Once);
        mockMapper.Verify(x => x.Map<List<QuoteMinuteModel>>(It.IsAny<IEnumerable<QuoteMinuteEntity>>()), Times.Once);
    }
    [Fact]
    public async Task GetModelForQuoteAsyncShouldCallRepositoryAndGetDataForLast30Minutes()
    {
        var mockRepository = new Mock<ISuperDbContextRepository>();
        var mockMapper = new Mock<IMapper>();
        var service = new QuoteModelService(mockRepository.Object, mockMapper.Object);

        await service.GetModelForQuoteAsync("ASDF");

        mockRepository.Verify(x => x.GetQuotesForMinute(It.IsAny<string>(), It.IsAny<long>(), It.IsAny<long>()), Times.Exactly(30));
    }
}
