﻿using System;
using System.Collections.Generic;
using System.Threading;

namespace ParserLibrary
{
    public class Queue : IQueue
    {

        /// <summary>
        /// Сайт
        /// </summary>
        public IQueueSettings QueueSettings { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="queueSettings"></param>
        public Queue(IQueueSettings queueSettings)
        {
            QueueSettings = queueSettings;
        }

        /// <summary>
        /// Временное сохранение данных
        /// </summary>
        public void SaveTempData(string seller, List<ILot> lots)
        {
            QueueSettings.DataManager.TempSave(seller, lots, QueueSettings);
        }

        /// <summary>
        /// Сбор данных
        /// </summary>
        public void DataCollection()
        {
            Console.WriteLine($"Запуск сбора данных с сайта {QueueSettings.SiteLink}");

            foreach (var seller in QueueSettings.Sellers)
            {
                Console.WriteLine($"Сбор лотов продавца: {seller} с сайта {QueueSettings.SiteLink}...");

                SellerDataCollection(seller);

                Console.WriteLine($"Сбор лотов продавца: {seller} с сайта {QueueSettings.SiteLink} успешно завершен");
            }

            Console.WriteLine($"Сбор данных с сайта {QueueSettings.SiteLink} завершен.");
        }

        /// <summary>
        /// Сбор лотов продавца и сохранение данных во временный файл
        /// </summary>
        /// <param name="seller"></param>
        private void SellerDataCollection(string seller)
        {
            SaveTempData(seller, GetAllLots(seller));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="seller"></param>
        /// <returns></returns>
        private List<ILot> GetAllLots(string seller)
        {
            List<ILot> lots = new List<ILot>();

            var url = $"{QueueSettings.SiteLink}{seller}";

            var pageCount = QueueSettings.Parser.GetPageNumber(url, QueueSettings.SearchLotsCount,
                                                                QueueSettings.LotsNumberFromPage,
                                                                QueueSettings.Encoding);

            for (int i = 1; i <= pageCount; i++)
            {
                var currentUrl = i == 1 ? url : $"{url}{QueueSettings.UrlPageNumber}{i}";

                var pageElements = QueueSettings.Parser.GetAllElement(currentUrl, QueueSettings.SearchAllElements, QueueSettings.Encoding);

                foreach (var element in pageElements)
                {
                    // просто глушим эксепшен, т.к. если лот кинул эксепшен, у него отсутствуют нужные поля и он нам не нужен
                    try
                    {
                        var price = QueueSettings.Parser.GetPriceFromElement(element, QueueSettings.SearchLotPrice);
                        var title = QueueSettings.Parser.GetTextFromElement(element, QueueSettings.SearchLotTitle);
                        var link = QueueSettings.Parser.GetStringFromAttribute(element, QueueSettings.SearchLotLink, "href", QueueSettings.LinkPrefix);
                        var city = QueueSettings.Parser.GetCityFromLink(link, QueueSettings.CutSymbolsFromLink);

                        if (CorrectCity(city) && CorrectTitle(title))
                            lots.Add(new Lot(price, title, link, city, QueueSettings.Trashwords, QueueSettings.UseTrashwords));                        
                    }
                    catch { }
                }

                Thread.Sleep(1500);
            }

            return lots;
        }

        /// <summary>
        /// Проверка заголовка лота на соответствие ключевым словам
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        private bool CorrectTitle(string title)
        {
            if (!QueueSettings.UseKeywords || title == null)
                return true;

            foreach (var keyword in QueueSettings.Keywords)
            {
                if (title.Contains(keyword))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Проверка города на соответствие отбору по городам
        /// </summary>
        /// <param name="cityName"></param>
        /// <returns></returns>
        private bool CorrectCity(string cityName)
        {
            if (!QueueSettings.UseCitiesFilter || cityName == null)
                return true;

            foreach (var city in QueueSettings.Cities)
            {
                if (cityName.Equals(city))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Фабрика для создания списка очередей из массива сайтов
        /// </summary>
        /// <param name="queueSettings"></param>
        /// <returns></returns>
        public static List<Queue> Factory(IQueueSettings[] queueSettings)
        {
            List <Queue> list = new List<Queue>();

            foreach (var settings in queueSettings)
            {
                list.Add(new Queue(settings));
            }

            return list;
        }
    }
}
