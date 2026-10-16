using System;
using System.Windows.Forms;

namespace KolmRakendust
{
    internal static class Program
    {
        /// <summary>
        /// Rakenduse käivituspunkt - avab Avavormi.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // Klassikaline käivitus - töötab nii .NET Framework kui ka .NET 6/7/8 projektis
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Avavorm());
        }
    }
}
