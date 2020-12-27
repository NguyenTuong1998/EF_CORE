using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCarFist.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace WebCarFist.ViewComponents
{
    [ViewComponent(Name = "Cart")]
    public class CartViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessionCart = HttpContext.Session.GetString("cart");
            if (sessionCart != null)
            {
                List<Item> cart = JsonConvert.DeserializeObject<List<Item>>(sessionCart);
                ViewBag.CoutItems = cart.Count;
            }
            return View("Index");
        }
    }
}
