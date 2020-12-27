using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;
using BCrypt;

namespace WebCarFist.Pages
{
    public class RegisterModel : PageModel
    {
        private DatabaseContext db;

        [BindProperty]
        public Account account { get; set; }

        public RegisterModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            account = new Account();
        }
        public IActionResult OnPost()
        {
            //account.Password = BCrypt.Net.BCrypt.HashPassword(account.Password);         
            db.Account.Add(account);
            db.SaveChanges();
            //RoleAccount roleAccount = new RoleAccount
            //{
              //  AccountId = account.Id,
                //RoleId = 2
           // };
            //db.RoleAccount.Add(roleAccount);
            //db.SaveChanges();
            return RedirectToPage("login");
        }
    }
}
