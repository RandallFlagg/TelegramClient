using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyWinFormClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            var loginForm = new TelegramLoginForm();
            Application.Run(loginForm);
            Application.Run(new TelegramClientForm(loginForm.Helper));
        }
    }
}
