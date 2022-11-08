using System.Net;
using HtmlAgilityPack;
using HtmlCrawler.Areas.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Operations.Helpers;

namespace HtmlCrawler.Areas.Api.Controllers;

[Area("CrawlerApi")]
[Route("/[controller]")]
[ApiController]
public class CrawlerApiController : ControllerBase
{
    private readonly IHtmlDocumentHelper _htmlDocumentHelper;

    private readonly ILogger<CrawlerApiController> _logger;

    public CrawlerApiController(IHtmlDocumentHelper htmlDocumentHelper, ILogger<CrawlerApiController> logger)
    {
        _htmlDocumentHelper = htmlDocumentHelper;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> CrawlPage(string url)
    {
        if (!url.StartsWith("http") && !url.StartsWith("https")) 
            return BadRequest("Protocol is missing");
        Uri uri;
        try
        {
            uri = new Uri(url);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest("Cannot parse url");
        }

        var web = new HtmlWeb();
        HtmlDocument htmlDoc;

        try
        {
            htmlDoc = await web.LoadFromWebAsync(url);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);
            return BadRequest("Cannot load the page");
        }

        var domain = $"{uri.Scheme}://{uri.Host}";

        var imagesRec = _htmlDocumentHelper.FindImagesRecursive(htmlDoc.DocumentNode, domain);

        var wordCounts = _htmlDocumentHelper.CountWords(htmlDoc.DocumentNode.InnerText);

        return Ok(JsonConvert.SerializeObject(new CrawledPageModel
        {
            ImageList = imagesRec,
            WordsWithCounts = wordCounts,
            TotalWords = wordCounts.Any() ? wordCounts.Count() : 0
        }));
    }
    
}