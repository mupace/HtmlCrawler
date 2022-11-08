using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net;
using HtmlCrawler.Models;
using Microsoft.AspNetCore.Http.Extensions;
using Newtonsoft.Json;
using HtmlCrawler.Areas.Api.Models;

namespace HtmlCrawler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpClientFactory _httpClientFactory;

        public HomeController(ILogger<HomeController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult Index()
        {
            return View(new HomepageModel());
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Index(HomepageModel model)
        {
            var httpRequestMsg = new HttpRequestMessage(HttpMethod.Get, $"{Request.Scheme}://{Request.Host}/CrawlerApi/?url={WebUtility.UrlEncode(model.Url)}");

            var httpClient = _httpClientFactory.CreateClient();
            
            var responseMsg = await httpClient.SendAsync(httpRequestMsg);

            if (responseMsg.IsSuccessStatusCode)
            {
                var crawledPage = await responseMsg.Content.ReadFromJsonAsync<CrawledPageModel>();

                var topTenWords = crawledPage.WordsWithCounts.OrderByDescending(x => x.Occurance).Take(10);

                crawledPage.WordsWithCounts = topTenWords;

                model.CrawledPage = crawledPage;

                return View(model);
            }

            model.ErrorMessages = model.ErrorMessages.Append(await responseMsg.Content.ReadAsStringAsync());

            return View(model);
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