using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Areas.Admin.Pages
{
    public class CategoryModel : PageModel
    {
        private DatabaseContext db;
        public List<Category> categories { get; set; }
        public CategoryModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            categories = db.Category.ToList();
        }
        public IActionResult OnGetDelete(int id)
        {
            db.Category.Remove(db.Category.Find(id));
            db.SaveChanges();
            return RedirectToPage("Category");
        }
    }
}
