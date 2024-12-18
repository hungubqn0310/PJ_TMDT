using _123.Models;
using Microsoft.AspNetCore.Mvc;
using _123.Helpers;
using _123.Services;
using System.Diagnostics;

namespace _123.Controllers
{
    [Route("admin/review")]
    public class ReviewController : Controller
    {
        private readonly ILogger<ReviewController> _logger;

        public ReviewController(ILogger<ReviewController> logger)
        {
            _logger = logger;
        }

        // Index: List all reviews
        public IActionResult Index()
        {
            var reviewViewModel = new ReviewsViewModel();
            reviewViewModel.Reviews = ReviewService.GetAllReviews(); // Call service to get all reviews
            ViewBag.Users = UserService.GetUsers() ?? new List<User>();
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();


            return View("Views/Admin/review.cshtml", reviewViewModel);
        }

        // GET Add: Show the form to add a new review
        [HttpGet("add")]
        public IActionResult Add()
        {
            Review review = new Review();
            ViewBag.Users = UserService.GetUsers() ?? new List<User>();
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();
            return PartialView("/Views/Admin/reviewadd.cshtml", review);
        }

        // POST Add: Add a new review
        [HttpPost("add")]
        public IActionResult Add(Review review)
        {
            ReviewService.CreateReview(review); // Call service to create a new review
            return new RedirectResult("/admin/review");
        }

        // GET Edit: Show the form to edit an existing review
        [HttpGet("edit")]
        public IActionResult Edit(int id)
        {
            Review review = ReviewService.GetReviewById(id); // Fetch review by ID
            ViewBag.Users = UserService.GetUsers() ?? new List<User>();
            ViewBag.Products = ProductService.GetProducts() ?? new List<Product>();
            return PartialView("/Views/Admin/reviewedit.cshtml", review);
        }

        // POST Edit: Update an existing review
        [HttpPost("edit")]
        public IActionResult Edit(Review review)
        {
            ReviewService.UpdateReview(review); // Call service to update review
            return new RedirectResult("/admin/review");
        }

        // GET Delete: Show the confirmation to delete a review
        [HttpGet("delete")]
        public IActionResult Delete(int id)
        {
            Review review = ReviewService.GetReviewById(id); // Fetch review by ID
            return PartialView("/Views/Admin/reviewdelete.cshtml", review);
        }

        // POST Delete: Soft delete the review (set is_deleted = 1)
        [HttpPost("delete")]
        public IActionResult Delete(Review review)
        {
            ReviewService.DeleteReview(review.ReviewId); // Call service to delete review
            return new RedirectResult("/admin/review");
        }

        // Error Handling Action
        [HttpGet("error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
