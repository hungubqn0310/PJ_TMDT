using _123.Models;


//transac
namespace _123 {
  public class TransactionHistoryViewModel
  {
    public List<TransactionHistory> TransactionHistorys {get; set;}

    public TransactionHistory TransactionHistory {get; set;}

        public IEnumerable<User> Users { get; set; }  = new List<User>();

        public IEnumerable<Order> Orders { get; set; }  = new List<Order>();

        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }  = new List<PaymentMethod>();


  }
}
