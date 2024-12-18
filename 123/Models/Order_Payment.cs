using System;

namespace _123.Models
{
    public class OrderPayment
    {
        public int OrderPaymentId { get; set; }
        public int OrderId { get; set; }
        public int PaymentMethodId { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime PaymentDate { get; set; }
        public bool IsDeleted { get; set; }
        public virtual Order Order { get; set; }
        public virtual PaymentMethod PaymentMethod { get; set; }


    }
}
