using ParserLibrary;
using ParserLibrary.Classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.Text;
using System.Windows.Forms;

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

            var appPath = Application.StartupPath;

            var settingsPath = $"{appPath}\\settings.xml";

            var parserSettings = Settings.GetSettings(settingsPath);

            parserSettings.CurrentAppPath = appPath;

            var queueSettings = GetQueueSettings(parserSettings);

            var queues = new List<IQueue>();

            queues.Add(new Queue(queueSettings[0]));
            queues.Add(new Queue(queueSettings[1]));

            var dispatcher = new Dispatcher(parserSettings, queues, new Model());

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
                    SearchLotTitle      = "[class=\"bulletinLink auto-shy\"]",
                    SearchLotPrice      = "[class=\"price-block__final-price finalPrice\"]",
                    SearchLotLink       = "[class=\"bulletinLink auto-shy\"]",
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
                    Parser              = new SiteParser(),
                    DataManager         = new Model(),
                    LotFactory          = new LotFactory<Lot>()
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
                    SearchAllElements   = "[class=\"b-shop-list clearfix catalog-list catalog-list_table\"]",
                    SearchLotTitle      = "[class=\"item-description-title-link\"]",
                    SearchLotPrice      = "[class=\"about\"]",
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
                    Parser              = new SiteParser(),
                    DataManager         = new Model(),
                    LotFactory          = new LotFactory<Lot>()
                }
            };          
        }
    }
}
