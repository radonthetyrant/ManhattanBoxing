using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManhattanBoxingProblem
{
    class DrawCanvas : PictureBox
    {

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);

            Console.WriteLine("Hello OnPaint!");
        }


    }
}
