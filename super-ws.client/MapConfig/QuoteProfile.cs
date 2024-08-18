using AutoMapper;
using super_ws.client.Messages;
using super_ws.database.Entity;

namespace super_ws.client.MapConfig
{
    public class QuoteProfile : Profile
    {
        public QuoteProfile()
        {
            CreateMap<QuotePrice, QuoteEntity>()
                .ForMember(ent => ent.Name, p => p.MapFrom(x => x.S))
                .ForMember(ent => ent.AskPrice, p => p.MapFrom(x => x.A))
                .ForMember(ent => ent.BidPrice, p => p.MapFrom(x => x.B))
                .ForMember(ent => ent.Time, p => p.MapFrom(x => x.T));
        }
    }
}
