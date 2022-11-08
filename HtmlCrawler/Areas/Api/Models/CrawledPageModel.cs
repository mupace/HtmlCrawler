using Models;

namespace HtmlCrawler.Areas.Api.Models
{
    public class CrawledPageModel
    {
        public List<string> ImageList { get; set; }

        public WordCountModel WordCount { get; set; }
    }
}
