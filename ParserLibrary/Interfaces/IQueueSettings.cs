using System.Text;

namespace ParserLibrary
{
    public interface IQueueSettings
    {
        bool UseThisSite { get; set; }

        bool UseKeywords { get; set; }

        bool UseTrashwords { get; set; }
      
        bool UseCitiesFilter { get; set; }

        string SiteLink { get; set; }

        string SearchLotsCount { get; set; }

        string UrlPageNumber { get; set; }

        string SearchAllElements { get; set; }

        string SearchLotTitle { get; set; }

        string SearchLotPrice { get; set; }

        string SearchLotLink { get; set; }

        string LinkPrefix { get; set; }

        string TempFilePrefix { get; set; }

        string SaveTempFolderPath { get; set; }

        string[] Sellers { get; set; }

        string[] Keywords { get; set; }

        string[] Trashwords { get; set; }

        string[] Cities { get; set; }

        int CutSymbolsFromLink { get; set; }

        int LotsNumberFromPage { get; set; }

        Encoding Encoding { get; set; }

        ISiteParser Parser { get; set; }

        IDataManager DataManager { get; set; }

        bool CorrectTitle(string title, Settings settings);

        bool CorrectCity(string currentCity, Settings settings);
    }
}
