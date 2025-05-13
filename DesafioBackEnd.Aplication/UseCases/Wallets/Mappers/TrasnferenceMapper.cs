using AutoMapper;
using StockManager.Aplication.UseCases.Wallets.Responses;
using StockManager.Domain.Entities;

namespace StockManager.Aplication.UseCases.Wallets.Mappers;

public class TrasnferenceMapper : Profile
{
    public TrasnferenceMapper()
    {
        CreateMap<Transference, TransferenceResponse>()
            .ForMember(x => x.UserReciever,
                y => y.MapFrom(_ => _.Reciever.Name));
    }
}