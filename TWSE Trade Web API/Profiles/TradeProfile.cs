using AutoMapper;
using TWSE_Trade_Web_API.ServiceModel;
using TWSE_Trade_Web_API.ViewModel;

namespace TWSE_Trade_Web_API.Profiles
{
    public class TradeProfile:Profile
    {
        public TradeProfile()
        {
            // service -> conroller
            CreateMap<TradeRespServiceModel, TradeRespViewModel>()
                .ForMember(
                    member => member.ReturnDate,
                    opt => opt.MapFrom(src => src.TradeDate.AddDays(src.LendingPeriod))
                )
                .ForMember(
                    member => member.Type,
                    opt => opt.MapFrom(src => TransformTypeName(src.Type))
                );
        }

        private static string TransformTypeName(char type)
        {
            switch (type)
            {
                case 'F':
                    return "定價";
                case 'C':
                    return "競價";
                case 'N':
                    return "議借";
                default:
                    return null;
            }
        }
    }
}
