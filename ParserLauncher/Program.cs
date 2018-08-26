using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Security.Principal;
using System.Windows.Forms;

namespace ParserLauncher
{
    class Program
    {
        private const string parserFileName = "Parser.exe";

        static void Main(string[] args)
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool IsAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

#if DEBUG
            IsAdmin = true;  
#endif

            if (!IsAdmin)
            {
                ProcessStartInfo info = new ProcessStartInfo();
                info.Verb = "runas";
                info.FileName = Application.ExecutablePath;
                try
                {
                    Process.Start(info);
                }
                catch (Win32Exception) { }

                Application.Exit();
            }
            else
            {
                GetStarted();
            }
        }

        private static void GetStarted()
        {
            Console.WriteLine("Запуск лаунчера...");

            Launcher launcher = new Launcher();

            Console.WriteLine("Текущая версия приложения: {0}", launcher.CurrentVersion);

            if (launcher.NeedUpdate())
            {
                Console.WriteLine("Получение обновлений...");
                var error = launcher.GetNewFiles();

                if (!error)
                {
                    Console.WriteLine("Обновления получены");
                    ParserStart();
                }
                else
                {
                    Console.WriteLine("При получении обновлений произошла ошибка!");
                    Console.ReadKey();
                }                
            }
            else
            {
                Console.WriteLine("Ипользуется актуальная версия приложения");
                ParserStart();
            }
        }

        static void ParserStart()
        {
            Console.WriteLine("Запуск парсера...");

            var parserFilePath = String.Format("{0}\\{1}", Application.StartupPath, parserFileName);

            if (File.Exists(parserFilePath))
            {
                Process.Start(parserFilePath);
                Application.Exit();
            }
            else
            {
                Console.WriteLine("Не удалось обнаружить файл Parser.exe! Он был удален, переименован или перемещен!");
                Console.ReadKey();
            }
        }       
    }
}
