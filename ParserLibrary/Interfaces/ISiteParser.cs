using AngleSharp.Dom;
using System.Text;

namespace ParserLibrary
{
    public interface ISiteParser
    {
        /// <summary>
        /// Возвращает количество страниц продавца
        /// </summary>
        /// <param name="link">Ссылка на первую страницу продавца</param>
        /// <param name="request">Селекторы для получения элемента, хранящего количество лотов продавца</param>
        /// <param name="lotsOnPage">Количество лотов на странице</param>
        /// <returns>Количество страниц</returns>
        int GetPageNumber(string link, string request, int lotsOnPage, Encoding encoding);

        /// <summary>
        /// Получение текстового содержимого элемента
        /// </summary>
        /// <param name="element">Элемент, в котором осуществляется поиск текста</param>
        /// <param name="request">Селектор для поиска вложенного элемента с нужным содержимым</param>
        /// <returns>Строка с текстовым содержимым искомого элемента</returns>
        string GetTextFromElement(IElement element, string request);

        /// <summary>
        /// Получение цены из элемента
        /// </summary>
        /// <param name="element">Элемент, в котором осуществляется поиск текста</param>
        /// <param name="request">Селектор для поиска вложенного элемента с нужным содержимым</param>
        /// <returns>Строка, содержащая цену искомого элемента</returns>
        string GetPriceFromElement(IElement element, string request);

        /// <summary>
        /// Получение строки со значением произвольного аттрибута html элемента страницы
        /// </summary>
        /// <param name="element">Элемент, в котором осуществляется поиск текста</param>
        /// <param name="request">Селектор для поиска вложенного элемента с нужным содержимым</param>
        /// <param name="attribute">Имя аттрибута, представленное в виде строки, например href</param>
        /// <param name="prefix">Префикс, добавляемый к значению аттрибута, например для превращения относительной ссылки в абсолютную</param>
        /// <returns>Содержимое искомого аттрибута</returns>
        string GetStringFromAttribute(IElement element, string request, string attribute, string prefix);

        /// <summary>
        /// Получение коллекции искомых элементов
        /// </summary>
        /// <param name="link">Ссылка на страницу, по которой ведется поиск</param>
        /// <param name="request">Селектор для поиска элементов</param>
        /// <param name="encoding">Кодировка. Используется в случае необходимости изменить кодировку страницы</param>
        /// <returns>Коллекция искомых элементов</returns>
        IHtmlCollection<IElement> GetAllElement(string link, string request, Encoding encoding);

        /// <summary>
        /// Извлекает и возвращает город продажи лота из его ссылки
        /// </summary>
        /// <param name="link">Ссылка на лот, содержащая город</param>
        /// <param name="deleteSymbolsCount">Количество лишних символов, стоящих перед именем города в ссылке, для удаления лишней части строки, находящейся перед именем города</param>
        /// <returns>Имя города в представлении сайта</returns>
        string GetCityFromLink(string link, int deleteSymbolsCount);

        /// <summary>
        /// Возвращает html содержимое нужной страницы
        /// </summary>
        /// <param name="link">Ссылка на страницу</param>
        /// <returns>Строка, хранящая содержимое исходной страницы</returns>
        string GetPage(string link, Encoding encoding);

        /// <summary>
        /// Удаляет из входной строки все символы, не являющиеся числами и возвращает результат
        /// </summary>
        /// <param name="text">Строка, из которое необходимо удалить все не числовые символы</param>
        /// <returns>Строка, содержащая только числа, либо пустая, если чисел во входной строке не было</returns>
        string GetNumbers(string text);
    }
}
