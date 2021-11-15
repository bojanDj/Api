using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Http.Headers;
using Project.Models;

namespace Project.Controllers
{
    public class HomeController : Controller
    {
        public static HttpClient httpClient = new HttpClient();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Logout()
        {
            Session["TokenNumber"] = null;

            return RedirectToAction("Index");
        }

        public async Task<ActionResult> Login(User user)
        {
            var token = string.Empty;
            var response = await Global.httpClient.GetAsync("ValidLogin?username="+user.username+"&password="+user.password);
            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                token = JsonConvert.DeserializeObject<string>(result);
                Session["TokenNumber"] = token;
                return RedirectToAction("Index", "Story", new { area = "" });
            }
            
            return RedirectToAction("Login");
            //return Content(token);
        }
    }
}