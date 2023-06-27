using AutoMapper;
using TWSE_Trade_Web_API.Models;
using TWSE_Trade_Web_API.ServiceModel;
using TWSE_Trade_Web_API.Utils;
using TWSE_Trade_Web_API.ViewModel;

namespace TWSE_Trade_Web_API.Profiles
{
    public class TradeProfile:Profile
    {

        public TradeProfile()
        {
            // controller -> service
            CreateMap<TradeViewModel, TradeServiceModel>()
                .ForMember(
                    member => member.Status,
                    opt => opt.MapFrom(src => 1)
                ) ;
            // service -> DB
            CreateMap<TradeServiceModel, Trade>()
                .ForMember(
                    member => member.Type,
                    opt => opt.MapFrom(src => TransformTypeTools.TransformTypeName(src.Type))
                );

            // service -> conroller
            CreateMap<TradeRespServiceModel, TradeRespViewModel>()
                .ForMember(
                    member => member.ReturnDate,
                    opt => opt.MapFrom(src => src.TradeDate.AddDays(src.LendingPeriod))
                )
                .ForMember(
                    member => member.Type,
                    opt => opt.MapFrom(src => TransformTypeTools.TransformTypeChar(src.Type))
                );
        }
    }
}
