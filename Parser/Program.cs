using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;
using ParserLibrary;

namespace Parser
{
    class Program
    {
        static void Main(string[] args)
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool IsAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

#if DEBUG
            IsAdmin = true;  
#endif

            if (!IsAdmin)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.Verb = "runas";
                info.FileName = Application.ExecutablePath;
                try
                {
                    Process.Start(info);
                }
                catch (Win32Exception) { }

                Application.Exit();
            }
            else
            {
                GetStarted();
            }
        }

        private static void GetStarted()
        {
            Console.WriteLine("Запуск парсера...");

            var settingsPath = $"{Application.StartupPath}\\settings.xml";

            var parserSettings = Settings.GetSettings(settingsPath);

            parserSettings.CurrentAppPath = Application.StartupPath;

            var queueSettings = GetQueueSettings(parserSettings);
           
            var dispatcher = new Dispatcher(parserSettings, Queue.Factory(queueSettings));

            dispatcher.Start();
        }

        private static IQueueSettings[] GetQueueSettings(Settings parserSettings)
        {
            return new IQueueSettings[]
            {
                new QueueSettings()
                {
                    UseThisSite         = parserSettings.UseFarpost,
                    UseKeywords         = parserSettings.UseKeywords,
                    UseCitiesFilter     = parserSettings.UseCitiesFilter,
                    UseTrashwords       = parserSettings.UseTrashwords,
                    SiteLink            = "https://www.farpost.ru/user/",
                    SearchLotsCount     = "[id=\"itemsCount_placeholder\"]",
                    UrlPageNumber       = "/?page=",
                    SearchAllElements   = "[class=\"descriptionCell\"]",
                    SearchLotTitle      = "[class=\"bulletinLink\"]",
                    SearchLotPrice      = "[class=\"finalPrice\"]",
                    SearchLotLink       = "[class=\"bulletinLink\"]",
                    LinkPrefix          = "",
                    TempFilePrefix      = "farpost",
                    SaveTempFolderPath  = $"{parserSettings.CurrentAppPath}\\data",
                    Sellers             = parserSettings.FarpostSellerNames,
                    Keywords            = parserSettings.Keywords,
                    Trashwords          = parserSettings.Trashwords,
                    Cities              = parserSettings.Cities,
                    CutSymbolsFromLink  = 23,
                    LotsNumberFromPage  = 50,
                    Encoding            = Encoding.GetEncoding(1251),
                    Parser              = new SiteParser()
                },
                new QueueSettings()
                {
                    UseThisSite         = parserSettings.UseAvito,
                    UseKeywords         = parserSettings.UseKeywords,
                    UseCitiesFilter     = parserSettings.UseCitiesFilter,
                    UseTrashwords       = parserSettings.UseTrashwords,
                    SiteLink            = "https://www.avito.ru/",
                    SearchLotsCount     = "[class=\"breadcrumbs-link-count js-breadcrumbs-link-count\"]",
                    UrlPageNumber       = "/rossiya?p=",
                    SearchAllElements   = "[class=\"item_table-header\"]",
                    SearchLotTitle      = "[class=\"item-description-title-link\"]",
                    SearchLotPrice      = "[class=\"about \"]",
                    SearchLotLink       = "[class=\"item-description-title-link\"]",
                    LinkPrefix          = "https://www.avito.ru",
                    TempFilePrefix      = "avito",
                    SaveTempFolderPath  = $"{parserSettings.CurrentAppPath}\\data",
                    Sellers             = parserSettings.AvitoSellerNames,
                    Keywords            = parserSettings.Keywords,
                    Trashwords          = parserSettings.Trashwords,
                    Cities              = parserSettings.Cities,
                    CutSymbolsFromLink  = 21,
                    LotsNumberFromPage  = 50,
                    Encoding            = null,
                    Parser              = new SiteParser()
                }
            };          
        }
    }
}
