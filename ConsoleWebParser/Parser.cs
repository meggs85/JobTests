using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebParser
{
    public class Parser
    {
        public Parser() { }

        public void ParseNode(HtmlNode node, ref DataModel model)
        {
            string xpathNodeImg = ".//img";
            string xpathNodeName = ".//h3/a";
            string xpathNodeVin = ".//@data-vin";
            string xpathNodePrice = ".//div[1]/div/div[2]/ul/li[1]/span/span[2]";
            string regExpNumberPattern = "\\D*(\\d+([[,|.]\\d+)?)";

            try
            {
                HtmlNode nodeImg = node.SelectNodes(xpathNodeImg).Where(x => x.GetAttributeValue("class", "") == "photo thumb").Single();
                string nodeImgSrc = nodeImg.GetAttributeValue("src", "N/A").Split('?')[0];
                model.URL = nodeImgSrc.Trim();
            }
            catch { }

            try
            {
                HtmlNode nodeName = node.SelectNodes(xpathNodeName).Single();
                string nodeNameText = nodeName.InnerText;
                model.name = nodeNameText.Trim();
            }
            catch { }

            try
            {
                var nodeVin = node.SelectNodes(xpathNodeVin).Single();
                string nodeVinText = nodeVin.GetAttributeValue("data-vin", "N/A");
                model.VIN = nodeVinText.Trim();
            }
            catch { }

            try
            {
                HtmlNode nodePrice = node.SelectNodes(xpathNodePrice).Single();
                string nodePriceText = nodePrice.InnerText;
                Regex regExPrice = new Regex(regExpNumberPattern);
                string numberVal = regExPrice.Match(nodePriceText).Groups[1].Value;
                //Char separator = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0];
                //double price = Convert.ToDouble(numberVal.Replace(',', separator));
                double price = Convert.ToDouble(numberVal.Replace(",", "")); // , - not decimal point! 
                model.price = price;
            }
            catch { }
        }

        public DataModel ParseDocument(string url)
        {
            string xpathNode = "//*[@id=\"compareForm\"]/div/div[2]/ul[1]/li";
            DataModel model = new DataModel();
            var web = new HtmlWeb();
            var doc = web.Load(url);
            HtmlNodeCollection htmlNodes = doc.DocumentNode.SelectNodes(xpathNode);
            ParseNode(htmlNodes[1], ref model);
            model.Validate();
            return model;
        }
    }
}
