﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using HtmlAgilityPack;
using Models;

namespace Operations.Helpers
{
    public class HtmlDocumentHelper : IHtmlDocumentHelper
    {
        private const string imgTag = "img";
        private const string srcAttr = "src";
        private const string whitespace = " ";

        public IEnumerable<string> FindImagesRecursive(HtmlNode htmlNode, string domain)
        {
            var imagesList = new List<string>();

            if (htmlNode.Name == imgTag)
            {
                var attrValue = htmlNode.GetAttributeValue(srcAttr, null);
                if (attrValue != null)
                {
                    if (!HasProtocol(attrValue))
                        attrValue = domain + attrValue;

                    imagesList.Add(attrValue);
                }
            }

            if (htmlNode.HasChildNodes)
            {
                imagesList.AddRange(htmlNode.ChildNodes.Select(x => FindImagesRecursive(x, domain)).SelectMany(x => x).ToList());
            }

            return imagesList;
        }

        public IEnumerable<WordCountModel> CountWords(string innerText)
        {
            throw new NotImplementedException();
        }

        private bool HasProtocol(string uri)
        {
            return uri.StartsWith("http") || uri.StartsWith("https");
        }

        private string CleanInnerText(string innerText)
        {
            innerText = Regex.Replace(innerText, "\\n", whitespace);
            innerText = Regex.Replace(innerText, "\\s+", whitespace);

            return innerText;
        }
    }
}
