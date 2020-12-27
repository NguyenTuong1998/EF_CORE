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
    public class LoginModel : PageModel
    {
        private DatabaseContext db;
        public string Msg { get; set; }
        public LoginModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return Page();
        }
        public IActionResult OnPost(string username, string password)
        {
            if(Check(username, password) != null)
            {
                HttpContext.Session.SetString("username", username);
                return RedirectToPage("Welcome");
            }
            else
            {
                Msg = "Invalid";
                return Page();
            }
        }
        private Account Check(string username, string password)
        {
            Account account = db.Account.SingleOrDefault(a => a.UserName.Equals(username));
            if(account != null)
            {
                if (password.Equals (account.Password) == true)
                {
                    return account;
                }
            }
            return null;
        }
    }
}
