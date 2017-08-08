using System;
using System.Windows.Forms;

namespace ManhattanBoxingProblem
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form1 form = new Form1();
            DrawingManager dman = new DrawingManager(form);
            Application.Run(form);
        }
    }
}
