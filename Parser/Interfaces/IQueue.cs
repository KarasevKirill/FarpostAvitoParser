using System.Collections.Generic;

namespace Parser.Interfaces
{
    interface IQueue
    {
        void SaveTempData(string seller, List<ILot> lots);

        void DataCollection();

        IQueueSettings QueueSettings { get; set; }
    }
}
