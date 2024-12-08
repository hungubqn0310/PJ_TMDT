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
            transactionViewModel.Transaction_Historys = Transaction_HistoryService.GetAllTransactions(); // Call service to get all transactions
            return View("Views/Admin/TransactionHistory.cshtml", transactionViewModel);
        }

        // GET Add: Show the form to add a new transaction
        [HttpGet("add")]
        public IActionResult Add()
        {
            var transaction = new Transaction_History();
            return PartialView("/Views/Admin/transactionadd.cshtml", transaction); // Show the form to add a new transaction
        }

        // POST Add: Create a new transaction
        [HttpPost("add")]
        public IActionResult Add(Transaction_History transaction)
        {
            if (ModelState.IsValid)
            {
                Transaction_HistoryService.CreateTransaction(transaction); // Call service to create a new transaction
                return RedirectToAction("Index"); // Redirect to the transaction list
            }
            return View("/Views/Admin/transactionadd.cshtml", transaction); // Return the form with validation errors
        }

        // GET Edit: Show the form to edit an existing transaction
        [HttpGet("edit/{id}")]
        public IActionResult Edit(int id)
        {
            var transaction = Transaction_HistoryService.GetTransactionById(id); // Fetch transaction by ID
            if (transaction == null)
            {
                return NotFound(); // Return 404 if the transaction does not exist
            }
            return PartialView("/Views/Admin/transactionedit.cshtml", transaction); // Show the form to edit the transaction
        }

        // POST Edit: Update an existing transaction
        [HttpPost("edit")]
        public IActionResult Edit(Transaction_History transaction)
        {
            if (ModelState.IsValid)
            {
                Transaction_HistoryService.UpdateTransaction(transaction); // Call service to update the transaction
                return RedirectToAction("Index"); // Redirect to the transaction list
            }
            return View("/Views/Admin/transactionedit.cshtml", transaction); // Return the form with validation errors
        }

        // GET Delete: Show the confirmation to delete a transaction
        [HttpGet("delete/{id}")]
        public IActionResult Delete(int id)
        {
            var transaction = Transaction_HistoryService.GetTransactionById(id); // Fetch transaction by ID
            if (transaction == null)
            {
                return NotFound(); // Return 404 if the transaction does not exist
            }
            return PartialView("/Views/Admin/transactiondelete.cshtml", transaction); // Show the confirmation to delete the transaction
        }

        // POST Delete: Delete a transaction (soft delete by setting is_deleted = 1)
        [HttpPost("delete")]
        public IActionResult Delete(Transaction_History transaction)
        {
            if (transaction == null)
            {
                return NotFound();
            }

            Transaction_HistoryService.DeleteTransaction(transaction.transaction_id); // Call service to delete the transaction
            return RedirectToAction("Index"); // Redirect to the transaction list
        }
        
        // Optional: GET Transaction details by ID (for viewing details)
        [HttpGet("details/{id}")]
        public IActionResult Details(int id)
        {
            var transaction = Transaction_HistoryService.GetTransactionById(id); // Fetch transaction by ID
            if (transaction == null)
            {
                return NotFound(); // Return 404 if the transaction does not exist
            }
            return View("/Views/Admin/transactiondetails.cshtml", transaction); // Show transaction details
        }
    }
}
