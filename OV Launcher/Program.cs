using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Windows.Forms;

namespace OV_Launcher
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            if (!IsRunAsAdmin())
            {
                // If not, try to restart the application with administrative privileges
                RestartAsAdmin();
                return; // Exit the Main method after restarting
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        private static bool IsRunAsAdmin()
        {
            using (WindowsIdentity identity = WindowsIdentity.GetCurrent())
            {
                WindowsPrincipal principal = new WindowsPrincipal(identity);
                return principal.IsInRole(WindowsBuiltInRole.Administrator);
            }
        }

        // Method to restart the application with administrative privileges
        private static void RestartAsAdmin()
        {
            ProcessStartInfo proc = new ProcessStartInfo
            {
                UseShellExecute = true,
                WorkingDirectory = Environment.CurrentDirectory,
                FileName = Application.ExecutablePath,
                Verb = "runas" // This will prompt for elevation
            };

            try
            {
                Process.Start(proc);
            }
            catch
            {
                // The user refused the elevation.
                MessageBox.Show("Bu uygulamanın çalışması için yönetici ayrıcalıkları gerekir", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            Application.Exit(); // Close the application
        }
    }
}
