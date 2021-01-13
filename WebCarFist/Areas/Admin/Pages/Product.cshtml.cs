using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Areas.Admin.Pages
{
    public class ProductModel : PageModel
    {

        private DatabaseContext db;
        public List<Product> products { get; set; }
        public ProductModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            products = db.Product.ToList();
        }
        public IActionResult OnGetDelete(int id)
        {
            db.Product.Remove(db.Product.Find(id));
            db.SaveChanges();
            return RedirectToPage("Product");
        }
    }
}
