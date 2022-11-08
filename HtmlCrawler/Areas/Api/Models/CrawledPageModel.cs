using Models;

namespace HtmlCrawler.Areas.Api.Models
{
    public class CrawledPageModel
    {
        public IEnumerable<string> ImageList { get; set; }

        public IEnumerable<WordCountModel> WordsWithCounts { get; set; }
    }
}
