namespace ParserLibrary
{
    public class Lot : ILot
    {
        /// <summary>
        /// Название лота
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Цена лота
        /// </summary>
        public string Price { get; set; }

        /// <summary>
        /// Ссылка на лот
        /// </summary>
        public string Link { get; set; }

        /// <summary>
        /// Город нахождения
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Инициализация полей лота
        /// </summary>
        /// <param name="price">Цена</param>
        /// <param name="title">Заголовок</param>
        /// <param name="link">Ссылка на лот</param>
        /// <param name="city">Город продажи лота</param>
        /// <param name="trashwords">Лишние слова, которые необходимо удалить из заголовка</param>
        /// <param name="useTrashwords">Признак удаления лишних слов</param>
        public Lot(string price, string title, string link, string city, string[] trashwords, bool useTrashwords)
        {
            Price = RemoveNewline(price);
            Title = RemoveNewline(RemoveTrashwords(title, trashwords, useTrashwords));
            Link = RemoveNewline(link);
            City = RemoveNewline(city);
        }

        /// <summary>
        /// Возвращает данные лота в пригодном для записи в .csv файл виде
        /// </summary>
        /// <returns></returns>
        public string GetData()
        {
            return $"{Link};{Title};{Price};{City};";
        }

        /// <summary>
        /// Удаляет при необходимости лишние слова из заголовка
        /// </summary>
        /// <param name="text"></param>
        /// <param name="trashwords"></param>
        /// <param name="useTrashwords"></param>
        /// <returns></returns>
        private string RemoveTrashwords(string text, string[] trashwords, bool useTrashwords = false)
        {
            if (useTrashwords)
            {
                foreach (var word in trashwords)
                {
                    text = text.Replace(word, "");
                }
            }

            return text;
        }

        /// <summary>
        /// Удаляет символы переноса строки
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private string RemoveNewline(string text)
        {
            text = text.Replace("\r", "");
            return text.Replace("\n", "");
        }
    }
}
