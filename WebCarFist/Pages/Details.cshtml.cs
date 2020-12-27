using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Pages
{
    public class DetailsModel : PageModel
    {
        private DatabaseContext db;

        [BindProperty(Name ="id", SupportsGet =true)]
        public int Id { get; set; }

        public Product product { get; set; }
        public DetailsModel(DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            product = db.Product.Find(Id);
        }
    }
}
