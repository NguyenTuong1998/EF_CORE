using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Areas.Admin.Pages
{
    public class EditCategoryModel : PageModel
    {
        private DatabaseContext db;
        [BindProperty]
        public Category category { get; set; }

        [BindProperty(Name = "id", SupportsGet = true)]
        public int Id { get; set; }
        public EditCategoryModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            category = db.Category.Find(Id);
        }
        public IActionResult OnPost(Category category)
        {
            db.Entry(category).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            db.SaveChanges();
            return RedirectToPage("Category");
        }
    }
}
