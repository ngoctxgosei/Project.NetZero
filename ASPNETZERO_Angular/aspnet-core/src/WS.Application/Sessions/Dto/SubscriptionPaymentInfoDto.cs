using Abp.AutoMapper;
using WS.MultiTenancy.Payments;

namespace WS.Sessions.Dto
{
    [AutoMapFrom(typeof(SubscriptionPayment))]
    public class SubscriptionPaymentInfoDto
    {
        public decimal Amount { get; set; }
    }
}