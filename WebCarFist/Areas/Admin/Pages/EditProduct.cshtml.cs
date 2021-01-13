using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Areas.Admin.Pages
{
    public class EditProductModel : PageModel
    {
        private DatabaseContext db;
        [BindProperty]
        public Product product { get; set; }
        [BindProperty(Name ="id", SupportsGet = true)]
        public int Id { get; set; }
        public List<Category> categories { get; set; }

        public EditProductModel(DatabaseContext _db)
        {
            db = _db;
          
           
        }
          
        public void OnGet()
        {
            product = db.Product.Find(Id);
            categories = db.Category.ToList();

        }     
      
        public IActionResult OnPost(Product product)
        {         
          
            db.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToPage("Product");

        }
    }
}
