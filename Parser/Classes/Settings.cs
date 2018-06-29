using System.IO;
using System.Xml.Serialization;

namespace Parser
{
    public class Settings
    {
        /// <summary>
        /// Путь для сохранения итогового файла
        /// </summary>
        public string SaveDataPath { get; set; }

        /// <summary>
        /// Путь до текущего экземпляра приложения
        /// </summary>
        public string CurrentAppPath { get; set; }

        /// <summary>
        /// Массив ников продавцов с фарпоста
        /// </summary>
        public string[] FarpostSellerNames { get; set; }

        /// <summary>
        /// Массив ников продавцов с авито
        /// </summary>
        public string[] AvitoSellerNames { get; set; }

        /// <summary>
        /// Массив ключевых слов для поиска
        /// </summary>
        public string[] Keywords { get; set; }

        /// <summary>
        /// Массив имен городов
        /// </summary>
        public string[] Cities { get; set; }

        /// <summary>
        /// Массив слов для удаления
        /// </summary>
        public string[] Trashwords { get; set; }

        /// <summary>
        /// Флаг "Выключить по завершении"
        /// </summary>
        public bool Switch { get; set; }


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
        /// Флаг "Собирать цены с Фарпост"
        /// </summary>
        public bool UseFarpost { get; set; }

        /// <summary>
        /// Флаг "Собирать цены с Авито"
        /// </summary>
        public bool UseAvito { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        private Settings()
        { }

        private static Settings settings;

        public static Settings GetSettings(string path)
        {
            if (settings == null)
                settings = new Settings();

            XmlSerializer xml = new XmlSerializer(typeof(Settings));

            using (FileStream fileStream = new FileStream(path, FileMode.OpenOrCreate))
            {
                try
                {
                    settings = (Settings)xml.Deserialize(fileStream);
                }
                catch { }
            }

            return settings;
        }

        public static void SaveSettings(string path, Settings settings)
        {
            XmlSerializer xml = new XmlSerializer(typeof(Settings));

            using (FileStream fileStream = new FileStream(path, FileMode.Create))
            {
                try
                {
                    xml.Serialize(fileStream, settings);
                }
                catch { }
            }
        }
    }
}
