using _123.Models;

namespace _123 {
  public class ReviewsViewModel
  {
    public List<Review> Reviews {get; set;}

    public Review Review {get; set;}

        public IEnumerable<User> Users { get; set; }  = new List<User>();

        public IEnumerable<Product> Products { get; set; }  = new List<Product>();



  }
}
