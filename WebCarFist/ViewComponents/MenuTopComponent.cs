using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCarFist.Models;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace WebCarFist.ViewComponents
{
    [ViewComponent(Name = "MenuTop")]
    public class MenuTopViewComponent : ViewComponent
    {
        public string usernameSession { get; set; }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            ViewBag.usernameSession = HttpContext.Session.GetString("username");
            return View("Index");
        }
    }
}
