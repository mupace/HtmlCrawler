using System.Net;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
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

        var htmlDoc = await web.LoadFromWebAsync(url);

        var txt = htmlDoc.DocumentNode.InnerText;

        var domain = $"{uri.Scheme}://{uri.Host}";

        var imagesRec = _htmlDocumentHelper.FindImagesRecursive(htmlDoc.DocumentNode, domain);

        var cleanedText = _htmlDocumentHelper.CleanInnerText(txt);

        var decoded = WebUtility.HtmlDecode(txt);

        return Ok("ok");
    }
    
}