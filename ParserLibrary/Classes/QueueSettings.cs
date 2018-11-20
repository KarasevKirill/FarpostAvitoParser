using System.Text;

namespace ParserLibrary
{
    public class QueueSettings : IQueueSettings
    {
        /// <summary>
        /// Определяет, нужно ли запускать парсинг этого сайта
        /// </summary>
        public bool UseThisSite { get; set; }

        /// <summary>
        /// Флаг "Исп. ключевые слова"
        /// </summary>
        public bool UseKeywords { get; set; }

        /// <summary>
        /// Флаг "Удалять лишние слова"
        /// </summary>
        public bool UseTrashwords { get; set; }

        /// <summary>
        /// Флаг "Включить фильтр по городам"
        /// </summary>
        public bool UseCitiesFilter { get; set; }

        /// <summary>
        /// Ссылка на сайт
        /// </summary>
        public string SiteLink { get; set; }

        /// <summary>
        /// Строка для поиска количества страниц в разметке страницы
        /// </summary>
        public string SearchLotsCount { get; set; }

        /// <summary>
        /// Строка, которая подставляется в url, чтобы перемещаться по страницам
        /// </summary>
        public string UrlPageNumber { get; set; }

        /// <summary>
        /// Строка для получения всех лотов на странице
        /// </summary>
        public string SearchAllElements { get; set; }

        /// <summary>
        /// Строка для получения заголовка лота
        /// </summary>
        public string SearchLotTitle { get; set; }

        /// <summary>
        /// Строка для получения цены лота
        /// </summary>
        public string SearchLotPrice { get; set; }

        /// <summary>
        /// Строка для получения ссылки на лот
        /// </summary>
        public string SearchLotLink { get; set; }

        /// <summary>
        /// Префикс для ссылок на лоты. 
        /// </summary>
        public string LinkPrefix { get; set; }

        /// <summary>
        /// Префикс для имен временных файлов. Необходим, чтобы не затереть файлы если ник продавца одинаков на разных сайтах
        /// </summary>
        public string TempFilePrefix { get; set; }

        /// <summary>
        /// Папка для сохранения временных файлов
        /// </summary>
        public string SaveTempFolderPath { get; set; }

        /// <summary>
        /// Список продавцов
        /// </summary>
        public string[] Sellers { get; set; }

        /// <summary>
        /// Ключевые слова для отбора
        /// </summary>
        public string[] Keywords { get; set; }

        /// <summary>
        /// Лишние слова для удаления
        /// </summary>
        public string[] Trashwords { get; set; }

        /// <summary>
        /// Города для отбора
        /// </summary>
        public string[] Cities { get; set; }

        /// <summary>
        /// Количество символов, которое необходимо отрезать из ссылки лота, чтобы получить город
        /// </summary>
        public int CutSymbolsFromLink { get; set; }

        /// <summary>
        /// Количество лотов на странице
        /// </summary>
        public int LotsNumberFromPage { get; set; }

        /// <summary>
        /// Кодировка, используется если для получения данных с сайта в корректном виде необходимо дополнительно декодировать текст
        /// </summary>
        public Encoding Encoding { get; set; }

        /// <summary>
        /// Парсер
        /// </summary>
        public ISiteParser Parser { get; set; }

        /// <summary>
        /// Модель, отвечает за сохранение результатов работы
        /// </summary>
        public IDataManager DataManager { get; set; }

        /// <summary>
        /// Если включен отбор по городам, то проверяет, находится ли лот в нужном городе
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Если включен отбор по ключевым словам, то проверяет, содержит ли заголовок эти слова
        /// </summary>
        /// <returns></returns>
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
