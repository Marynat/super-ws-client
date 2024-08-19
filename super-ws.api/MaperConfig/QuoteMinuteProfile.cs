using AutoMapper;
using super_ws.api.Model;
using super_ws.database.Entity;

namespace super_ws.api.MaperConfig;

public class QuoteMinuteProfile : Profile
{
    public QuoteMinuteProfile()
    {
        CreateMap<QuoteMinuteModel, QuoteMinuteEntity>();
        CreateMap<QuoteMinuteEntity, QuoteMinuteModel>();
    }
}
