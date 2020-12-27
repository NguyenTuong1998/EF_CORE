using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebCarFist.Models;

namespace WebCarFist.Pages
{
    public class InvoiceManagementModel : PageModel
    {
        private DatabaseContext db;
        public List<Invoice> invoices { get; set; }
        public InvoiceManagementModel (DatabaseContext _db)
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
                var account = db.Account.AsNoTracking().SingleOrDefault(a => a.UserName.Equals
                (username));
                invoices = db.Invoice.Where(i => i.AccountId == account.Id).OrderByDescending(i => i.Id).ToList();
                return Page();
            }
        }
    }
}
