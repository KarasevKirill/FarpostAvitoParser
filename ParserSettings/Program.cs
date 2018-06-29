using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;
using System.Windows.Forms;

namespace Parser
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            WindowsPrincipal principal = new WindowsPrincipal(WindowsIdentity.GetCurrent());
            bool IsAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);

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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            }
        }
    }
}
