using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebCarFist.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;


namespace WebCarFist.Pages
{
    public class CartModel : PageModel
    {
        private DatabaseContext db;
        public List<Item> cart { get; set; }

        public decimal Total { get; set; }
        public CartModel (DatabaseContext _db)
        {
            db = _db;
        }
        public void OnGet()
        {
            string sessionCart = HttpContext.Session.GetString("cart");
            Total = 0;
            if (sessionCart != null)
            {
                cart = JsonConvert.DeserializeObject<List<Item>>(sessionCart);
                Total = cart.Sum(it => it.product.Price * it.Quantity);
            }
        }

        public IActionResult OnPost(int[] quantity)
        {
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>
                (HttpContext.Session.GetString("cart"));
            for (int i= 0; i < quantity.Length; i++)
            {
                items[i].Quantity = quantity[i];  
            }
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(items));
            return RedirectToPage("Cart");
        }

        public IActionResult OnGetRemove(int id)
        {
            List<Item> items = JsonConvert.DeserializeObject<List<Item>>
                (HttpContext.Session.GetString("cart"));
            int index = exits(id, items);
            items.RemoveAt(index);
            HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(items));
            return RedirectToPage("Cart");
        }

        public IActionResult OnGetCheckout()
        {
            string username = HttpContext.Session.GetString("username");
            if(username == null)
            {
                return RedirectToPage("Login");
            }
            else
            {
                var account = db.Account.AsNoTracking().SingleOrDefault(a => a.UserName.Equals
                (username));
                //add new invoice
                var invoice = new Invoice
                {
                    Created = DateTime.Now,
                    Name="Invoice",
                    AccountId = account.Id
                };
                db.Invoice.Add(invoice);
                db.SaveChanges();

                //Add invoice details
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>
                (HttpContext.Session.GetString("cart"));

                if(items != null)
                {
                    foreach(var item in items)
                    {
                        var invoiceDatails = new InvoiceDetails
                        {
                            InvoiceId = invoice.Id,
                            ProductId = item.product.Id,
                            Price = item.product.Price,
                            Quantity = item.Quantity,
                        };
                        db.InvoiceDetails.Add(invoiceDatails);
                        db.SaveChanges();
                    }
                }
                return RedirectToPage("Thanks");
            }
        }

        public IActionResult OnGetBuyNow(int id)
        {
            string sessionCart = HttpContext.Session.GetString("cart");
            Product product = db.Product.SingleOrDefault(p => p.Id == id);
            if (sessionCart == null)
            {
                List<Item> items = new List<Item>();
                items.Add(new Item
                {
                    product = new ProductCart { 

                        Id = product.Id,
                        Name = product.Name,
                        Price = product.Price,
                        Photo = product.Photo,
                    },
                    Quantity = 1
                });
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(items));
            }
            else
            {
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>
                (HttpContext.Session.GetString("cart"));
                int index = exits(id, items);
                if(index == -1)
                {
                    items.Add(new Item
                    {
                        product = new ProductCart
                        {

                            Id = product.Id,
                            Name = product.Name,
                            Price = product.Price,
                            Photo = product.Photo,
                        },
                        Quantity = 1
                    });
                }
                else
                {
                    items[index].Quantity++;
                }
                HttpContext.Session.SetString("cart", JsonConvert.SerializeObject(items));
            }
            return RedirectToPage("Cart");
        }

        private int exits(int id, List<Item> items)
        {
            for(var i=0; i < items.Count; i++)
            {
                if(items[i].product.Id == id)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
