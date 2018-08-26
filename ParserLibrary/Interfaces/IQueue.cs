using System.Collections.Generic;

namespace ParserLibrary
{
    public interface IQueue
    {
        void SaveTempData(string seller, List<ILot> lots);

        void DataCollection();

        IQueueSettings QueueSettings { get; set; }
    }
}
