using System.Text;

namespace ParserLibrary
{
    public class QueueSettings : IQueueSettings
    {
        public bool UseThisSite { get; set; }

        public bool UseKeywords { get; set; }

        public bool UseTrashwords { get; set; }

        public bool UseCitiesFilter { get; set; }

        public string SiteLink { get; set; }

        public string SearchLotsCount { get; set; }

        public string UrlPageNumber { get; set; }

        public string SearchAllElements { get; set; }

        public string SearchLotTitle { get; set; }

        public string SearchLotPrice { get; set; }

        public string SearchLotLink { get; set; }

        public string LinkPrefix { get; set; }

        public string TempFilePrefix { get; set; }

        public string SaveTempFolderPath { get; set; }

        public string[] Sellers { get; set; }

        public string[] Keywords { get; set; }

        public string[] Trashwords { get; set; }

        public string[] Cities { get; set; }

        public int CutSymbolsFromLink { get; set; }

        public int LotsNumberFromPage { get; set; }

        public Encoding Encoding { get; set; }

        public ISiteParser Parser { get; set; }

        public IDataManager DataManager { get; set; }

        public bool CorrectCity(string currentCity, Settings settings)
        {
            if (!settings.UseCitiesFilter || currentCity == null)
                return true;

            foreach (var city in settings.Cities)
            {
                if (currentCity.Equals(city))
                    return true;
            }

            return false;
        }

        public bool CorrectTitle(string title, Settings settings)
        {
            if (!settings.UseKeywords || title == null)
                return true;

            foreach (var keyword in settings.Keywords)
            {
                if (title.Contains(keyword))
                    return true;
            }

            return false;
        }
    }
}
