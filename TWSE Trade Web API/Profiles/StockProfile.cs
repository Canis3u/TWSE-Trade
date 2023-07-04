using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TWSE_Trade_Web_API.Models;
using TWSE_Trade_Web_API.ServiceModel;
using TWSE_Trade_Web_API.ViewModel;

namespace TWSE_Trade_Web_API.Profiles
{
    public class StockProfile:Profile
    {
        public StockProfile()
        {
            CreateMap<ClosingPrice, StockRespServiceModel>()
                .ForMember(
                    member => member.Name,
                    opt => opt.MapFrom(src => src.Stock.Name)
                )
                .ForMember(
                    member => member.LatestTradeDate,
                    opt => opt.MapFrom(src => src.TradeDate.ToString("yyyyMMdd"))
                )
                .ForMember(
                    member => member.LatestClosingPrice,
                    opt => opt.MapFrom(src => src.Price)
                );
            CreateMap<StockRespServiceModel, StockRespViewModel>()
                .ForMember(
                    member => member.LatestTradeDate,
                    opt => opt.MapFrom(src => DateTime.ParseExact(src.LatestTradeDate, "yyyyMMdd", null).ToString("yyyy-MM-dd"))
                );
        }
    }
}
