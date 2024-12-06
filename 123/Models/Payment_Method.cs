using System;

namespace _123.Models
{
    public class PaymentMethod
    {
        public int PaymentMethodId { get; set; }
        public string PaymentMethodName { get; set; }
        public bool IsDeleted { get; set; }
    }
}
