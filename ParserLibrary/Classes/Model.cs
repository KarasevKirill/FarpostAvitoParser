using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

namespace ParserLibrary
{
    public class Model : IDataManager
    {
        public void TempSave(string seller, List<ILot> lots, IQueueSettings queueSettings)
        {           
            if (!Directory.Exists($"{queueSettings.SaveTempFolderPath}"))
            {
                DirectoryInfo folder = Directory.CreateDirectory($"{queueSettings.SaveTempFolderPath}");
                folder.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
                
            string filePath = $"{queueSettings.SaveTempFolderPath}\\{queueSettings.TempFilePrefix} {seller}.csv";

            string[] data = new string[lots.Count];

            for (int i = 0; i < data.Length; i++)
            {
                data[i] = lots[i].GetData();
            }

            try
            {
                File.WriteAllLines(filePath, data, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при сохранении временного файла {filePath}.\r\nError: {e.Message}");
            }
        }

        public void SaveAllData(string tempFolderPath, string saveFolderPath)
        {  
            // удаляем замыкающий слеш, если есть
            if (saveFolderPath.EndsWith("\\"))
                saveFolderPath.Remove(saveFolderPath.Length - 1);

            // формируем имя итогового файла
            string filePath = $"{saveFolderPath}\\Выгрузка цен {DateTime.Now.ToString("dd.MM.yy в H-mm-ss")}.csv";

            // собираем имена всех файлов
            string[] files = Directory.GetFiles(tempFolderPath, "*.csv");

            // список для хранения всех записей
            List<string> data = new List<string>();

            foreach (var file in files)
            {
                using (Stream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string txt;

                        while (streamReader.Peek() != -1)
                        {
                            txt = streamReader.ReadLine();

                            if (!string.IsNullOrEmpty(txt))
                                data.Add(txt);
                        }
                    }

                    fileStream.Close();
                }                                   
            }

            try
            {
                // сохраняем данные
                File.WriteAllLines(filePath, data, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine($"При итоговом сохранении данных произошла ошибка.\r\nError: {e.Message}");
                Console.ReadKey();
            }

            try
            {
                // удаляем папку data
                if (Directory.Exists(tempFolderPath))
                {
                    // проверочка, чтобы случайно не форматнуть диск, один раз было :D 
                    if (Regex.IsMatch(tempFolderPath, "^[A-Z]{1}:\\\\$", RegexOptions.IgnoreCase))
                        Console.WriteLine($"Выбран недопустимый путь для удаления! Путь: {tempFolderPath}");
                    else
                        Directory.Delete(tempFolderPath, true);
                }                    
            }
            catch (Exception e)
            {
                Console.WriteLine($"При удалении папки с временными файлами произошла ошибка.\r\nError: {e.Message}");
                Console.ReadKey();
            }
        }
    }
}
