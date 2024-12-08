using _123.Models;

namespace _123 {
  public class OrderPaymentViewModel
  {
    public List<OrderPayment> OrderPayments {get; set;}

    public OrderPayment OrderPayment {get; set;}

    public IEnumerable<Order> Orders { get; set; }   = new List<Order>();

    public IEnumerable<PaymentMethod> PaymentMethods { get; set; }   = new List<PaymentMethod>();

  }
}
