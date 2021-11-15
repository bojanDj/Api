using Newtonsoft.Json;
using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Project.Controllers
{
    public class StoryController : Controller
    {

        // GET: Story
        public async Task<ActionResult> Index()
        {
            var response = await Global.httpClient.GetAsync("Stories");
            if (response.IsSuccessStatusCode)
            {
                IEnumerable<StoryModel> stories = response.Content.ReadAsAsync<IEnumerable<StoryModel>>().Result;
                ViewBag.Stories = stories;
            }
            return View(TempData["model"]);
        }

        [HttpPost]
        public ActionResult Create(StoryModel story)
        {
            if (story.storyID != 0) 
            {
                TempData["story"] = story;
                return RedirectToAction("EditPut"); 
            }
            if (Session["TokenNumber"] != null)
                Global.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["TokenNumber"].ToString());
            var response = Global.httpClient.PostAsJsonAsync("Stories", story).Result;
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return RedirectToAction("Login");
            ViewBag.Message = response.Content.ToString();
            return RedirectToAction("Index");
        }

        public ActionResult Delete(int id)
        {
            if (Session["TokenNumber"] != null)
                Global.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["TokenNumber"].ToString());
            var response = Global.httpClient.DeleteAsync("Stories/"+id).Result;
            
            if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized) return RedirectToAction("Login");
            ViewBag.Message = "Deleted.";
            return RedirectToAction("Index");
        }
        public async Task<ActionResult> Edit(int id)
        {
            var response = Global.httpClient.GetAsync("Stories/"+id.ToString()).Result;
         
            TempData["model"] = response.Content.ReadAsAsync<StoryModel>().Result;
            return RedirectToAction("Index");
        }
        
        public ActionResult EditPut()
        {
            StoryModel story = (StoryModel)TempData["story"];
            if (Session["TokenNumber"] != null)
                Global.httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Session["TokenNumber"].ToString());
            var response = Global.httpClient.PutAsJsonAsync("Stories/" + story.storyID, story).Result;

            return RedirectToAction("Index");
        }
        public ActionResult Login(StoryModel story)
        {
            return View();
        }
    }
}