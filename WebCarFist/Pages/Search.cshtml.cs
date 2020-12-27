using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Pages
{
    public class SearchModel : PageModel
    {
        private DatabaseContext db;
        [BindProperty(Name = "id", SupportsGet = true)]
        public int categoryId { get; set; }

        [BindProperty(Name = "keyword", SupportsGet = true)]
        public string keyword { get; set; }
        public List<Product> products { get; set; }
        public SearchModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            if(categoryId == -1)
            {
                products = db.Product.Where(p => p.Name.Contains(keyword)).ToList();
            }
            else
            {
                products = db.Product.Where(p => p.CategoryId == categoryId && p.Name.Contains(keyword)).ToList();
            }
        }
    }
}
