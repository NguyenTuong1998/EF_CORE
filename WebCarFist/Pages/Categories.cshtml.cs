using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Pages
{
    public class CategoriesModel : PageModel
    {
        private DatabaseContext db;
        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
        public Category category { get; set; }
        public List<Product> products { get; set; }
        public CategoriesModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            category = db.Category.Find(Id);
            products = db.Product.Where(p => p.CategoryId == Id).ToList();
        }
    }
}
