using BLOGN.Models;
using BLOGN.MVC.Helper;
using BLOGN.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text;

namespace BLOGN.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        ApiHttpClient _api = new ApiHttpClient();
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Name = "-";

            List<Category> categories = new List<Category>();
            HttpClient client = _api.initial();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
            HttpResponseMessage response = await client.GetAsync("api/Category");

            if (response.IsSuccessStatusCode)
            {
                var result = response.Content.ReadAsStringAsync().Result;
                categories = JsonConvert.DeserializeObject<List<Category>>(result);
            }
                        

            return View(categories.ToList());
        }

        public async Task<ActionResult<Category>> Save(Category model)
        {
            HttpClient client = _api.initial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMSIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTY0NjE2OTYxNCwiZXhwIjoxNjQ2NjAxNjE0LCJpYXQiOjE2NDYxNjk2MTR9.6TZMMV4031G7UpdaOfaukDYHiXN1uXcInRH47dv6xzc");
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var action = "api/Category";
            if (model.Id == 0)
            {
                action = "api/Category";
            }
            HttpResponseMessage res = await client.PostAsync(action, content);//.ConfigureAwait(false);
            res.EnsureSuccessStatusCode();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
            }

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult<Category>> DeleteData(Category model)
        {
            HttpClient client = _api.initial();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1lIjoiMSIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTY0NjE2OTYxNCwiZXhwIjoxNjQ2NjAxNjE0LCJpYXQiOjE2NDYxNjk2MTR9.6TZMMV4031G7UpdaOfaukDYHiXN1uXcInRH47dv6xzc");
            var content = new StringContent(JsonConvert.SerializeObject(model), Encoding.UTF8, "application/json");
            var action = "api/Category/" + model.Id;
            HttpResponseMessage res = await client.DeleteAsync(action).ConfigureAwait(false);
            res.EnsureSuccessStatusCode();
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
            }

            return RedirectToAction("Index", "Home");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}