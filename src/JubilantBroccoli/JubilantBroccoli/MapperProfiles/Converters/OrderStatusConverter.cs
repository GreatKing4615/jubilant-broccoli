using AutoMapper;
using JubilantBroccoli.Domain.Core.Enums;

namespace JubilantBroccoli.MapperProfiles.Converters
{


    public class OrderStatusConverter : ITypeConverter<OrderStatus, string>
    {
        public string Convert(OrderStatus source, string destination, ResolutionContext context)
        {
            return source.ToString();
        }
    }
    //public class OrderStatusConverter : IValueConverter<OrderStatus, string>
    //{
    //    public string Convert(OrderStatus sourceMember, ResolutionContext context)
    //    {
    //        switch (sourceMember)
    //        {
    //            case OrderStatus.Cooking:
    //                return "Готовится";
    //            case OrderStatus.WaitingPay:
    //                return "Ожидает оплаты";
    //            case OrderStatus.Canceled:
    //                return "Отменен";
    //            case OrderStatus.Delivering:
    //                return "В процессе доставки";
    //            case OrderStatus.Finished:
    //                return "Завершен";
    //            case OrderStatus.InTheCart:
    //                return "В корзине";
    //            case OrderStatus.WaitingPickup:
    //                return "Готов для самовывоза";
    //            default:
    //                return string.Empty;
    //        }
    //    }
    //}

}
