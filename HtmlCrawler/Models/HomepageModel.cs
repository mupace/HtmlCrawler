using System.ComponentModel.DataAnnotations;
using HtmlCrawler.Areas.Api.Models;

namespace HtmlCrawler.Models
{
    public class HomepageModel
    {
        [Required]
        public string Url { get; set; }

        public CrawledPageModel? CrawledPage { get; set; }

        public IEnumerable<string> ErrorMessages{ get; set; }

        public HomepageModel()
        {
            ErrorMessages = new List<string>();
        }
    }
}
