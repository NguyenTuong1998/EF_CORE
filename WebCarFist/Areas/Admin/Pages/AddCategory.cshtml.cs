using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Areas.Admin.Pages
{
    public class AddCategoryModel : PageModel
    {
        private DatabaseContext db;
        [BindProperty]
        public Category category { get; set; }
        public AddCategoryModel(DatabaseContext _db)
        {
            db = _db;                                                                   
        }
        public void OnGet()
        {
            category = new Category();
        }
        public IActionResult OnPost(Category category)
        {
            db.Category.Add(category);           
            db.SaveChanges();
            return RedirectToPage("Category");
        }
    }
}
