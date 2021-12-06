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
            var xivobj = new XIVCollectDTO();
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(url)
            };

            client.DefaultRequestHeaders.Accept.Add(
            new MediaTypeWithQualityHeaderValue("application/json"));

            string response = await client.GetStringAsync(url);
            if (!(String.IsNullOrEmpty(response)))
            {
                //creating xivobject for webpage to consume
                var test = response.ToString();
                JObject json = JObject.Parse(response.ToString());
                xivobj.portraitLink = json.SelectToken("portrait").Value<string>();
                xivobj.inGameName = json.SelectToken("name").Value<string>();
                xivobj.totalMounts = json.SelectToken("mounts").SelectToken("total").Value<string>();
                xivobj.totalCards = json.SelectToken("triad").SelectToken("total").Value<string>();
                xivobj.allCards = json.SelectToken("triad").SelectToken("count").Value<string>();
                xivobj.allMounts = json.SelectToken("mounts").SelectToken("count").Value<string>();
                xivobj.triadPrecent = Math.Truncate((decimal)((double.Parse(xivobj.allCards) / double.Parse(xivobj.totalCards))*100)).ToString() + "%";
                xivobj.mountPrecent = Math.Truncate((decimal)((double.Parse(xivobj.allMounts) / double.Parse(xivobj.totalMounts)) * 100)).ToString() + "%";
                xivobj.lastUpdated = json.SelectToken("last_parsed").Value<DateTime>();


            }
            return View(xivobj);
        }
        public IActionResult RaeshawnBart()
        {
            return View();
        }
        public IActionResult SamuelDappen()
        {
            return View();
        }
        public IActionResult BillAppiagyei()
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
