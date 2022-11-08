using HtmlAgilityPack;
using Models;

namespace Operations.Helpers;

public interface IHtmlDocumentHelper
{
    IEnumerable<string> FindImagesRecursive(HtmlNode htmlNode, string domain);

    IEnumerable<WordCountModel> CountWords(string innerText);
}