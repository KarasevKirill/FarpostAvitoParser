namespace ParserLibrary
{
    public interface ILot
    {
        /// <summary>
        /// Название (заголовок) лота
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Цена лота
        /// </summary>
        string Price { get; set; }

        /// <summary>
        /// Ссылка на лот
        /// </summary>
        string Link { get; set; }

        /// <summary>
        /// Город нахождения лота
        /// </summary>
        string City { get; set; }

        /// <summary>
        /// Список слов, которые необходимо удалить из заголовка лота
        /// </summary>
        string[] Trashwords { get; set; }

        /// <summary>
        /// Флаг, определяющий, нужно ли удалять лишние слова из заголовка лота
        /// </summary>
        bool UseTrashwords { get; set; }

        /// <summary>
        /// Возвращает данные лота в пригодном для сохранения виде
        /// </summary>
        /// <returns>Строка произвольного формата</returns>
        string GetData();

        /// <summary>
        /// Удаляет при необходимости лишние слова из заголовка
        /// </summary>
        void RemoveTrashwords();
    }
}
