using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParserLibrary
{
    public class Dispatcher
    {
        /// <summary>
        /// Эксземпляр настроек
        /// </summary>
        private Settings Settings { get; set; }  
        
        /// <summary>
        /// Список очередей для парсинга
        /// </summary>
        private List<Queue> Queues { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="parserSettings"></param>
        /// <param name="sites"></param>
        public Dispatcher(Settings parserSettings, List<Queue> queues)
        {
            Settings = parserSettings;
            Queues = queues;
        }

        /// <summary>
        /// Метод в котором у очередей запускается парсинг в отдельных потоках
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Формирование и запуск очередей...");

            var taskList = new List<Task>();

            foreach (var queue in Queues)
            {
                if (queue.QueueSettings.UseThisSite)
                {
                    taskList.Add(Task.Factory.StartNew(queue.DataCollection));
                }
            }

            Task.WaitAll(taskList.ToArray());

            Console.WriteLine("Сбор данных окончен. Сохранение...");

            SaveData();
        }

        /// <summary>
        /// Сохраняет все данные в итоговый файл
        /// </summary>
        private void SaveData()
        {
            Model.SaveAllData($"{Settings.CurrentAppPath}\\data", Settings.SaveDataPath);

            Console.WriteLine($"Сохранение данных по адресу: {Settings.SaveDataPath} завершено!");
        }
    }
}
