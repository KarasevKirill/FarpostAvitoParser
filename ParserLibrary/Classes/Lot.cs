namespace ParserLibrary
{
    public class Lot : ILot
    {
        public string Title { get; set; }

        public string Price { get; set; }

        public string Link { get; set; }

        public string City { get; set; }

        public string[] Trashwords { get; set; }

        public bool UseTrashwords { get; set; }

        public string GetData()
        {
            return $"{Link};{Title};{Price};{City};";
        }

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
        /// <returns>Строка без знаков переноса</returns>
        private string RemoveNewline(string text)
        {
            text = text.Replace("\r", "");
            return text.Replace("\n", "");
        }
    }
}
