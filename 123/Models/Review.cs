using System;

namespace _123.Models
{
    public class Review
    {
        public int ReviewId { get; set; }
        public string ProductId { get; set; }
        public int UserId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime ReviewDate { get; set; }
        public bool IsDeleted { get; set; }

        public virtual User User { get; set; }
        public virtual Product Product { get; set; }


    }
}
