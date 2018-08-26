using Newtonsoft.Json;
using System;
using System.Collections.Specialized;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace ParserLauncher
{
    class Launcher
    {
        private const string urlSettings = "http://karasev-kirill.somee.com/update/GetSettings";
        private const string urlGetFile = "http://karasev-kirill.somee.com/update/GetFile";
        private const string versionFileName = "version.txt";      

        private WebClient Client { get; set; }

        private ServerSettings Settings { get; set; }

        /// <summary>
        /// Путь к файлу с текущей версией приложения
        /// </summary>
        private string VersionFilePath { get; set; }

        /// <summary>
        /// Текущая версия приложения (из файла)
        /// </summary>
        public string CurrentVersion{ get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public Launcher()
        {
            Client = new WebClient();

            VersionFilePath = $"{Application.StartupPath}\\{versionFileName}";

            GetCurrentVersion();

            GetServerSettings();
        }

        /// <summary>
        /// Получает текущую версию приложения из файла
        /// </summary>
        private void GetCurrentVersion()
        {
            if (!File.Exists(VersionFilePath))
            {
                using (FileStream fileStream = File.Create(VersionFilePath))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes("0.1");

                    fileStream.Write(info, 0, info.Length);
                }
            }

            CurrentVersion = File.ReadAllText(VersionFilePath);
        }

        /// <summary>
        /// Получает данные с сервера
        /// </summary>
        private void GetServerSettings()
        {
            var values = new NameValueCollection
            {
                { "psw", "1" }
            };

            var response = Encoding.UTF8.GetString(Client.UploadValues(urlSettings, values));

            Settings = JsonConvert.DeserializeObject<ServerSettings>(response);
        }

        /// <summary>
        /// Получает файлы с сервера
        /// </summary>
        public bool GetNewFiles()
        {
            var error = false;

            foreach (var file in Settings.Files)
            {
                try
                {
                    string url = $"{urlGetFile}/?fileName={file}";

                    Client.DownloadFile(url, file);       
                }
                catch
                {
                    Console.WriteLine("Не удалось получить файл {0}", file);

                    error = true;
                }
            }

            if (!error)
                UpdateCurrentVersion();

            return error;
        }

        /// <summary>
        /// Перезаписывает файл с версией
        /// </summary>
        /// <returns></returns>
        public void UpdateCurrentVersion()
        {
            using (FileStream fileStream = File.Create(VersionFilePath))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(Settings.AppVersion);

                fileStream.Write(info, 0, info.Length);
            }
        }

        /// <summary>
        /// Если версия на сервере старше текущей, значит требуется обновление
        /// </summary>
        /// <returns></returns>
        public bool NeedUpdate()
        {
            var serverVersion = Convert.ToDecimal(Settings.AppVersion, System.Globalization.CultureInfo.InvariantCulture);
            var clientVersion = Convert.ToDecimal(CurrentVersion, System.Globalization.CultureInfo.InvariantCulture);

            return serverVersion > clientVersion;
        }
    }
}
