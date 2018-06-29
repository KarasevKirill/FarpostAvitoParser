using AngleSharp.Dom;
using System.Text;

namespace Parser.Interfaces
{
    interface ISiteParser
    {
        int GetPageNumber(string link, string request, int lotsOnPage, Encoding encoding);

        string GetTextFromElement(IElement element, string request);

        string GetPriceFromElement(IElement element, string request);

        string GetStringFromAttribute(IElement element, string request, string attribute, string prefix);

        IHtmlCollection<IElement> GetAllElement(string link, string request, Encoding encoding);

        string GetCityFromLink(string link, int position);

        string GetPage(string link, Encoding encoding);

        string GetNumbers(string text);
    }
}
