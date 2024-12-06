using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using _123.Services;
using _123.Models;

namespace _123.Controllers
{
    [Route("admin/transaction")]
    public class TransactionController : Controller
    {
        private readonly ILogger<TransactionController> _logger;

        public TransactionController(ILogger<TransactionController> logger)
        {
            _logger = logger;
        }

        // Index: List all transactions
        [HttpGet("")]
        public IActionResult Index()
        {
            var transactionViewModel = new TransactionHistoryViewModel();
            transactionViewModel.TransactionHistorys = TransactionHistoryService.GetAllTransactions(); // Call service to get all transactions
            return View("Views/Admin/TransactionHistory.cshtml", transactionViewModel);
        }

        // GET Add: Show the form to add a new transaction
        [HttpGet("add")]
        public IActionResult Add()
        {
            var transaction = new TransactionHistory();
            return PartialView("/Views/Admin/transactionadd.cshtml", transaction); // Show the form to add a new transaction
        }

        // POST Add: Create a new transaction
        [HttpPost("add")]
        public IActionResult Add(TransactionHistory transaction)
        {
            if (ModelState.IsValid)
            {
                TransactionHistoryService.CreateTransaction(transaction); // Call service to create a new transaction
                return RedirectToAction("Index"); // Redirect to the transaction list
            }
            return View("/Views/Admin/transactionadd.cshtml", transaction); // Return the form with validation errors
        }

        // GET Edit: Show the form to edit an existing transaction
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var transaction = TransactionHistoryService.GetTransactionById(id); // Fetch transaction by ID
            if (transaction == null)
            {
                return NotFound(); // Return 404 if the transaction does not exist
            }
            return PartialView("/Views/Admin/transactionedit.cshtml", transaction); // Show the form to edit the transaction
        }

        // POST Edit: Update an existing transaction
        [HttpPost("edit")]
        public IActionResult Edit(TransactionHistory transaction)
        {
            if (ModelState.IsValid)
            {
                TransactionHistoryService.UpdateTransaction(transaction); // Call service to update the transaction
                return RedirectToAction("Index"); // Redirect to the transaction list
            }
            return View("/Views/Admin/transactionedit.cshtml", transaction); // Return the form with validation errors
        }

        // GET Delete: Show the confirmation to delete a transaction
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var transaction = TransactionHistoryService.GetTransactionById(id); // Fetch transaction by ID
            if (transaction == null)
            {
                return NotFound(); // Return 404 if the transaction does not exist
            }
            return PartialView("/Views/Admin/transactiondelete.cshtml", transaction); // Show the confirmation to delete the transaction
        }

        // POST Delete: Delete a transaction (soft delete by setting is_deleted = 1)
        [HttpPost("delete")]
        public IActionResult Delete(TransactionHistory transaction)
        {
            if (transaction == null)
            {
                return NotFound();
            }

            TransactionHistoryService.DeleteTransaction(transaction.TransactionId); // Call service to delete the transaction
            return RedirectToAction("Index"); // Redirect to the transaction list
        }
        
        // Optional: GET Transaction details by ID (for viewing details)
        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var transaction = TransactionHistoryService.GetTransactionById(id); // Fetch transaction by ID
            if (transaction == null)
            {
                return NotFound(); // Return 404 if the transaction does not exist
            }
            return View("/Views/Admin/transactiondetails.cshtml", transaction); // Show transaction details
        }
    }
}
