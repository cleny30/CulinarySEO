using BusinessObject.Models.Enum;

namespace BusinessObject.Models.Dto
{
    public class CheckoutRequestDto
    {
        public Guid CustomerId { get; set; }
        public string ShippingAddress { get; set; } = string.Empty;
        public string ShippingCity { get; set; } = string.Empty;
        public List<string>? VoucherCodes { get; set; }
        public PaymentMethodEnum PaymentMethod { get; set; } = PaymentMethodEnum.COD;
    }

}
