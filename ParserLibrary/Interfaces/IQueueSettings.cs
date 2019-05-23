using System.Text;

namespace ParserLibrary
{
    public interface IQueueSettings
    {
        /// <summary>
        /// Определяет, нужно ли запускать парсинг этого сайта
        /// </summary>
        bool UseThisSite { get; set; }

        /// <summary>
        /// Флаг "Исп. ключевые слова"
        /// </summary>
        bool UseKeywords { get; set; }

        /// <summary>
        /// Флаг "Удалять лишние слова"
        /// </summary>
        bool UseTrashwords { get; set; }

        /// <summary>
        /// Флаг "Включить фильтр по городам"
        /// </summary>
        bool UseCitiesFilter { get; set; }

        /// <summary>
        /// Ссылка на сайт
        /// </summary>
        string SiteLink { get; set; }

        /// <summary>
        /// Селектор для поиска количества страниц в разметке страницы
        /// </summary>
        string SearchLotsCount { get; set; }

        /// <summary>
        /// Строка, которая подставляется в url, чтобы перемещаться по страницам
        /// </summary>
        string UrlPageNumber { get; set; }

        /// <summary>
        /// Селектор для получения всех лотов на странице
        /// </summary>
        string SearchAllElements { get; set; }

        /// <summary>
        /// Селектор для получения заголовка лота
        /// </summary>
        string SearchLotTitle { get; set; }

        /// <summary>
        /// Селектор для получения цены лота
        /// </summary>
        string SearchLotPrice { get; set; }

        /// <summary>
        /// Селектор для получения ссылки на лот
        /// </summary>
        string SearchLotLink { get; set; }

        /// <summary>
        /// Префикс для ссылок на лоты. 
        /// </summary>
        string LinkPrefix { get; set; }

        /// <summary>
        /// Префикс для имен временных файлов. Необходим, чтобы не затереть файлы если ник продавца одинаков на разных сайтах
        /// </summary>
        string TempFilePrefix { get; set; }

        /// <summary>
        /// Папка для сохранения временных файлов
        /// </summary>
        string SaveTempFolderPath { get; set; }

        /// <summary>
        /// Список ников продавцов
        /// </summary>
        string[] Sellers { get; set; }

        /// <summary>
        /// Ключевые слова для отбора
        /// </summary>
        string[] Keywords { get; set; }

        /// <summary>
        /// Лишние слова для удаления
        /// </summary>
        string[] Trashwords { get; set; }

        /// <summary>
        /// Города для отбора
        /// </summary>
        string[] Cities { get; set; }

        /// <summary>
        /// Количество символов, которое необходимо отрезать из ссылки лота, чтобы получить город
        /// </summary>
        int CutSymbolsFromLink { get; set; }

        /// <summary>
        /// Количество лотов на странице
        /// </summary>
        int LotsNumberFromPage { get; set; }

        /// <summary>
        /// Кодировка, используется если для получения данных с сайта в корректном виде необходимо изменить кодировку текста
        /// </summary>
        Encoding Encoding { get; set; }

        /// <summary>
        /// Парсер
        /// </summary>
        ISiteParser Parser { get; set; }

        /// <summary>
        /// Модель, отвечает за сохранение результатов работы
        /// </summary>
        IDataManager DataManager { get; set; }

        /// <summary>
        /// Если включен отбор по городам, то проверяет, находится ли лот в нужном городе
        /// </summary>
        /// <returns></returns>
        bool CorrectTitle(string title, Settings settings);

        /// <summary>
        /// Если включен отбор по ключевым словам, то проверяет, содержит ли заголовок эти слова
        /// </summary>
        /// <returns></returns>
        bool CorrectCity(string currentCity, Settings settings);
    }
}
