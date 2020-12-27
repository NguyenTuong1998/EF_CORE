using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Pages
{
    public class InvoiceDetailsModel : PageModel
    {
        private DatabaseContext db;

        [BindProperty(Name ="id", SupportsGet =true)]
        public int Id { get; set; }
        public Invoice invoice { get; set; }
        public decimal Total { get; set; }

        public InvoiceDetailsModel (DatabaseContext _db)
        {
            db = _db;
        }
        public IActionResult OnGet()
        {
            string username = HttpContext.Session.GetString("username");
            if (username == null)
            {
                return RedirectToPage("Login");
            }
            else
            {
                invoice = db.Invoice.Find(Id);
                Total = invoice.InvoiceDetails.Sum(i => i.Price * i.Quantity);
                return Page();
            }
             
        }
    }
}
