using System.Collections.Generic;

namespace ParserLibrary
{
    public interface IDataManager
    {
        void TempSave(string seller, List<ILot> lots, IQueueSettings queueSettings);

        void SaveAllData(string tempFolderPath, string saveFolderPath);
    }
}
