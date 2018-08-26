using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ParserLibrary
{
    public class Model
    {
        public static void TempSave(string seller, List<ILot> lots, IQueueSettings queueSettings)
        {
            try
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

                File.WriteAllLines(filePath, data, Encoding.UTF8);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при сохранении временных файлов.\r\nError: {e.Message}");
            }
        }

        public static void SaveAllData(string tempFolderPath, string saveFolderPath)
        {          
            try
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
                    Stream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);

                    using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                    {
                        string txt = "";

                        while (streamReader.Peek() != -1)
                        {
                            txt = streamReader.ReadLine();

                            if (!string.IsNullOrEmpty(txt))
                                data.Add(txt);
                        }
                    }

                    fileStream.Close();
                }

                // сохраняем данные
                File.WriteAllLines(filePath, data, Encoding.UTF8);

                // проверяем, чтобы случайно не форматнуть диск
                if (tempFolderPath.Equals("D:\\") || tempFolderPath.Equals("C:\\") || tempFolderPath.Equals("E:\\"))
                {
                    Console.WriteLine($"Выбран недопустимый путь для удаления! Путь: {tempFolderPath}");
                    return;
                }

                // удаляем папку data
                if (Directory.Exists(tempFolderPath))
                    Directory.Delete(tempFolderPath, true);
            }
            catch (Exception e)
            {
                Console.WriteLine($"При итоговом сохранении данных произошла ошибка.\r\nError: {e.Message}");
                Console.ReadKey();
            }
        }
    }
}
