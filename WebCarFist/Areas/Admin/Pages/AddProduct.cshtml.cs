using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;

namespace WebCarFist.Areas.Admin.Pages
{
    public class AddProductModel : PageModel
    {

        private DatabaseContext db;
        public Product product { get; set; }
        public List<Category> categories { get; set; }
        private IWebHostEnvironment webHost;
        public AddProductModel(DatabaseContext _db, IWebHostEnvironment _webHost )
        {
            db = _db;
            webHost = _webHost;
        }
        public void OnGet()
        {
            product = new Product();
            categories = db.Category.ToList();
        }
    
        public IActionResult OnPost(Product product, IFormFile fileupload)
        {
            var fileName = DateTime.Now.ToString("yyyyyMMddHHmmss") + fileupload.FileName;
            var path = Path.Combine(webHost.WebRootPath, "product", fileName);
            var stream = new FileStream(path, FileMode.Create);
            fileupload.CopyToAsync(stream);

            product.Photo = fileName;
            product.Status = null;
            db.Product.Add(product);
            db.SaveChanges();
            return RedirectToPage("Product");

        }
    }
}
