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
        /// Список слов, которые необходимо удалить из заголовка лота
        /// </summary>
        public string[] Trashwords { get; set; }

        /// <summary>
        /// Флаг, определяющий, нужно ли удалять лишние слова из заголовка лота
        /// </summary>
        public bool UseTrashwords { get; set; }

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
        public void RemoveTrashwords()
        {
            if (UseTrashwords)
            {
                foreach (var word in Trashwords)
                {
                    Title = Title.Replace(word, "");
                }
            }
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
