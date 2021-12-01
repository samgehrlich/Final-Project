using Final_Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using static Final_Project.Models.XIVCollectDTO;

namespace Final_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult SamGehrlich()
        {
            return View();
        }
        public async Task<IActionResult> ColinVitolsAsync()
        {
            //making a api call to xivcollect and creating object to pass to model. 
            var url = "https://ffxivcollect.com/api/characters/10323955";
            var bruh = new XIVCollectDTO();
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(url);

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string response = await client.GetStringAsync(url);
            if (!(String.IsNullOrEmpty(response)))
            {
                //creating xivobject for webpage to consume
                var test = response.ToString();
                JObject json = JObject.Parse(response.ToString());
                bruh.portraitLink = json.SelectToken("portrait").Value<string>();
            }
            return View(bruh);
        }
        public IActionResult Page3()
        {
            return View();
        }
        public IActionResult Page4()
        {
            return View();
        }
        public IActionResult Page5()
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
