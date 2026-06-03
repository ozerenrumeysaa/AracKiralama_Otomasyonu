using System;
using System.Windows.Forms;

namespace AracKiralama_Otomasyonu
{
    static class Program
    {
        /// <summary>
        /// Uygulamanın ana girdi noktası.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Programın Form1 ile başlamasını sağlayan komut
            Application.Run(new Form1());
        }
    }
}