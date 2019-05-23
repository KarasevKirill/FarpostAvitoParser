using System.Collections.Generic;

namespace ParserLibrary
{
    public interface IQueue
    {
        /// <summary>
        /// Временное сохранение данных
        /// </summary>
        void SaveTempData(string seller, List<ILot> lots);

        /// <summary>
        /// Сбор данных
        /// </summary>
        void DataCollection();

        /// <summary>
        /// Настройки
        /// </summary>
        IQueueSettings QueueSettings { get; set; }
    }
}
