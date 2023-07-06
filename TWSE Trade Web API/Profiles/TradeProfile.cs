using AutoMapper;
using System;
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
            CreateMap<TradeQueryViewModel, TradeQueryServiceModel>()
                .ForMember(
                    member => member.StartDate,
                    opt => opt.MapFrom(src => DateTime.ParseExact(src.StartDate, "yyyy-MM-dd", null).ToString("yyyyMMdd"))
                )
                .ForMember(
                    member => member.EndDate,
                    opt => opt.MapFrom(src => DateTime.ParseExact(src.EndDate, "yyyy-MM-dd", null).ToString("yyyyMMdd"))
                );
            CreateMap<TradeUpdateViewModel, TradeUpdateServiceModel>()
                .ForMember(
                    member => member.Status,
                    opt => opt.MapFrom(src => 1)
                );
            CreateMap<TradeUpdateServiceModel, Trade>();
            CreateMap<TradeQueryRespServiceModel, TradeQueryRespViewModel>();
            CreateMap<TradeQueryServiceModel, TradeQueryViewModel>()
                .ForMember(
                    member => member.StartDate,
                    opt => opt.MapFrom(src => DateTime.ParseExact(src.StartDate, "yyyyMMdd", null).ToString("yyyy-MM-dd"))
                )
                .ForMember(
                    member => member.EndDate,
                    opt => opt.MapFrom(src => DateTime.ParseExact(src.EndDate, "yyyyMMdd", null).ToString("yyyy-MM-dd"))
                );
            CreateMap<TradeRespServiceModel, TradeRespViewModel>()
                .ForMember(
                    member => member.StockIdAndName,
                    opt => opt.MapFrom(src => $"{src.StockId} {src.StockName}")
                )
                .ForMember(
                    member => member.Type,
                    opt => opt.MapFrom(src => TransformTypeTools.TransformTypeCode(src.Type))
                )
                .ForMember(
                    member => member.TradeDate,
                    opt => opt.MapFrom(src => src.TradeDate.ToString("yyyy-MM-dd"))
                )
                .ForMember(
                    member => member.ReturnDate,
                    opt => opt.MapFrom(src => src.TradeDate.AddDays(src.LendingPeriod).ToString("yyyy-MM-dd"))
                );
        }
    }
}
