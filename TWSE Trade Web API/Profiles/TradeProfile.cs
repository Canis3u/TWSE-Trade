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
            CreateMap<TradeQueryViewModel, TradeQueryServiceModel>();
            CreateMap<TradeUpdateViewModel, TradeUpdateServiceModel>()
                .ForMember(
                    member => member.Status,
                    opt => opt.MapFrom(src => 1)
                );
            CreateMap<TradeUpdateServiceModel, Trade>();


            CreateMap<TradeQueryRespServiceModel, TradeQueryRespViewModel>();
            CreateMap<TradeQueryServiceModel, TradeQueryViewModel>();
            CreateMap<TradeRespServiceModel, TradeRespViewModel>()
                .ForMember(
                    member => member.ReturnDate,
                    opt => opt.MapFrom(src => src.TradeDate.AddDays(src.LendingPeriod))
                )
                .ForMember(
                    member => member.Type,
                    opt => opt.MapFrom(src => TransformTypeTools.TransformTypeCode(src.Type))
                );
        }
    }
}
