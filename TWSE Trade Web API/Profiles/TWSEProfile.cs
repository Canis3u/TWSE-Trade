using AutoMapper;
using System;
using System.Collections.Generic;
using System.Globalization;
using TWSE_Trade_Web_API.Models;
using TWSE_Trade_Web_API.Utils;

namespace TWSE_Trade_Web_API.Profiles
{
    public class TWSEProfile : Profile
    {
        public TWSEProfile()
        {
            // ReqViewModel -> DB
            // 0  ["112年06月01日",
            // 1  "0050   元大台灣50      ",
            // 2  "議借",
            // 3  "25,000",
            // 4  "0.25",
            // 5  "125.15",
            // 6  "112年12月01日",
            // 7  183,
            // 8  ""]
            CreateMap<List<Object>, Stock>()
                .ForMember(
                    member => member.StockId,
                    opt => opt.MapFrom(src => GetStockId(src[1].ToString()))
                )
                .ForMember(
                    member => member.Name,
                    opt => opt.MapFrom(src => GetStockName(src[1].ToString()))
                );

            CreateMap<List<Object>, ClosingPrice>()
                .ForMember(
                    member => member.StockId,
                    opt => opt.MapFrom(src => GetStockId(src[1].ToString()))
                )
                .ForMember(
                    member => member.TradeDate,
                    opt => opt.MapFrom(src => TransformDateTime(src[0].ToString()))
                )
                .ForMember(
                    member => member.Price,
                    opt => opt.MapFrom(src => Convert.ToDouble(src[5].ToString()))
                );

            CreateMap<List<Object>, Trade>()
                .ForMember(
                    member => member.StockId,
                    opt => opt.MapFrom(src => GetStockId(src[1].ToString()))
                )
                .ForMember(
                    member => member.TradeDate,
                    opt => opt.MapFrom(src => TransformDateTime(src[0].ToString()))
                )
                .ForMember(
                    member => member.Type,
                    opt => opt.MapFrom(src => TransformTypeTools.TransformTypeName(src[2].ToString()))
                )
                .ForMember(
                    member => member.Volume,
                    opt => opt.MapFrom(src => Convert.ToInt64(src[3].ToString().Replace(",", "")))
                )
                .ForMember(
                    member => member.Fee,
                    opt => opt.MapFrom(src => Convert.ToDouble(src[4].ToString()))
                )
                .ForMember(
                    member => member.LendingPeriod,
                    opt => opt.MapFrom(src => Convert.ToInt64(src[7].ToString()))
                )
                .ForMember(
                    member => member.Status,
                    opt => opt.MapFrom(src => 0)
                );
        }
        private static string GetStockId(string stockString)
        {
            var splitList = stockString.Trim().Split(' ');
            return splitList[0];
        }
        private static string GetStockName(string stockString)
        {
            var splitList = stockString.Trim().Split(' ');
            return splitList[^1];
        }
        private static DateTime TransformDateTime(string twDateString)
        {
            // 時間日期 民國112年1月1號 -> 20230101
            CultureInfo culture = new CultureInfo("zh-TW");
            culture.DateTimeFormat.Calendar = new TaiwanCalendar();
            return DateTime.Parse(twDateString, culture);
        }
    }
}
