using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Pages
{
    public class IndexModel : PageModel
    {
        private DatabaseContext db;
        public List<Product> NewProducts { get; set; }
        

        public IndexModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            NewProducts = db.Product.ToList();
        }
    }
}
