using System.Collections.Generic;

namespace ParserLibrary
{
    public interface IDataManager
    {
        /// <summary>
        /// Сохранение данных лотов продавца во временный файл
        /// </summary>
        /// <param name="seller">Ник продавца</param>
        /// <param name="lots">Список лотов</param>
        /// <param name="queueSettings">Настройки</param>
        void TempSave(string seller, List<ILot> lots, IQueueSettings queueSettings);

        /// <summary>
        /// Итоговое сохранение всех данных
        /// </summary>
        /// <param name="tempFolderPath">Путь к папке с временнными файлами</param>
        /// <param name="saveFolderPath">Путь сохранения итогового файла</param>
        void SaveAllData(string tempFolderPath, string saveFolderPath);
    }
}
