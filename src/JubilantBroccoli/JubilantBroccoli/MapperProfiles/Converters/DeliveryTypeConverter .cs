using AutoMapper;
using JubilantBroccoli.Domain.Core.Enums;

namespace JubilantBroccoli.MapperProfiles.Converters
{

    public class DeliveryTypeConverter : ITypeConverter<DeliveryType, string>
    {
        public string Convert(DeliveryType source, string destination, ResolutionContext context)
        {
            return source.ToString(); // Возвращает название перечисления в виде строки
        }
    }
    //public class DeliveryTypeConverter : IValueConverter<DeliveryType, string>
    //{
    //    public string Convert(DeliveryType sourceMember, ResolutionContext context)
    //    {
    //        switch (sourceMember)
    //        {
    //            case DeliveryType.PickUp:
    //                return "Самовывоз";
    //            case DeliveryType.Courier:
    //                return "Курьерская доставка";
    //            default:
    //                return string.Empty;
    //        }
    //    }
    //}

}
