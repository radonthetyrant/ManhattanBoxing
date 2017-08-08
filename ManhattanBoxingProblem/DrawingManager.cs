using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace ManhattanBoxingProblem
{

    /// <summary>
    /// Class with logic for the user interface. Handles the field and solvers
    /// </summary>
    class DrawingManager
    {

        Form1 form;

        PictureBox canvas;

        Bitmap bmp;

        Bitmap grid;

        Graphics picture;

        Point? Cursor;

        const int NUM_CELLS = 16;

        const int CELL_SIZE = 32;

        uint delayMs = 1000; // Currenly unused

        List<Rectangle> boxes = new List<Rectangle>();

        bool isInPlaceMode = false;

        bool cellPlaceMode;

        bool[,] field = new bool[NUM_CELLS, NUM_CELLS];

        Color[] colors =
        {
            Color.Red, Color.Green, Color.Blue, Color.Yellow, Color.Magenta, Color.Cyan, Color.Turquoise, Color.DarkGray, Color.Olive, Color.Violet
        };

        Dictionary<string, IBoxingSolver> solvers = new Dictionary<string, IBoxingSolver>
        {
            { "ExpandingBoxSolver", new ExpandingBoxSolver() }
        };

        string SelectedSolver;

        public DrawingManager(Form1 form)
        {
            this.form = form;
            this.canvas = form.canvas;

            form.Load += Form_Load;
            form.iDelayMs.ValueChanged += IDelayMs_ValueChanged;
            form.btnClear.Click += BtnClear_Click;
            form.btnBox.Click += BtnBox_Click;
            // Combobox
            form.solverDropdown.Items.Clear();
            foreach (KeyValuePair<string, IBoxingSolver> entry in solvers)
            {
                form.solverDropdown.Items.Add(entry.Key);
            }
            form.solverDropdown.SelectedIndex = 0;
            form.solverDropdown.SelectedValueChanged += SolverDropdown_SelectedValueChanged;
            SelectedSolver = (string)form.solverDropdown.SelectedItem;
        }

        private void SolverDropdown_SelectedValueChanged(object sender, EventArgs e)
        {
            ComboBox cb = (sender as ComboBox);
            SelectedSolver = (string)cb.SelectedItem;
        }

        private void BtnBox_Click(object sender, EventArgs e)
        {
            if (SelectedSolver == null || !solvers.ContainsKey(SelectedSolver))
            {
                Console.WriteLine("No Solver selected or Solver not found!");
                return;
            }

            form.btnBox.Enabled = false; // Disable btn

            Console.WriteLine("Starting Boxing with delay " + delayMs + " ms ...");
            boxes.Clear();

            bool[,] tmpField = new bool[field.GetLength(0), field.GetLength(1)];
            for (int y = 0; y < field.GetLength(0); y++)
                for (int x = 0; x < field.GetLength(1); x++)
                    tmpField[y, x] = field[y, x];

            //boxes.Add(new Rectangle(2, 4, 4, 8));
            //boxes.Add(new Rectangle(1, 1, 1, 9));

            IBoxingSolver solver = solvers[SelectedSolver];

            Action finalize = () => {
                form.btnBox.Enabled = true;
                Redraw();
            };

            Thread t = new Thread(() =>
            {
                Stopwatch sw = new Stopwatch();
                sw.Start();
                Rectangle[] result = solver.Solve(tmpField);
                sw.Stop();
                Console.WriteLine("Solver finished in " + sw.ElapsedMilliseconds + "ms with "+result.Length+" boxes.");
                foreach (Rectangle r in result)
                {
                    boxes.Add(r);
                }
                form.Invoke(finalize);
            });
            t.Start();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            for (int y = 0; y < NUM_CELLS; y++)
                for (int x = 0; x < NUM_CELLS; x++)
                    field[y, x] = false;
            boxes.Clear();
            Redraw();
        }

        private void IDelayMs_ValueChanged(object sender, EventArgs e)
        {
            delayMs = (uint)(sender as NumericUpDown).Value;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            canvas.MouseClick += Canvas_MouseClick;
            canvas.MouseMove += Canvas_MouseMove;
            canvas.MouseEnter += Canvas_MouseEnter;
            canvas.MouseLeave += Canvas_MouseLeave;
            canvas.MouseDown += Canvas_MouseDown;
            canvas.MouseUp += Canvas_MouseUp;

            bmp = new Bitmap(512, 512);
            for (int y = 0; y < 512; y++)
                for (int x = 0; x < 512; x++)
                    bmp.SetPixel(x, y, Color.White);
            grid = new Bitmap(512, 512);
            for (int y = 0; y < 512; y++)
                for (int x = 0; x < 512; x++)
                {
                    if (y % 32 == 0 || x % 32 == 0)
                        grid.SetPixel(x, y, Color.Gray);
                    else if ((y - 1) % 32 == 0 || (x - 1) % 32 == 0)
                        grid.SetPixel(x, y, Color.LightGray);
                    else
                        grid.SetPixel(x, y, Color.White);
                }

            canvas.SizeMode = PictureBoxSizeMode.Normal;
            canvas.ClientSize = new Size(512, 512);
            canvas.Image = bmp;
            canvas.BorderStyle = BorderStyle.Fixed3D;

            picture = canvas.CreateGraphics();

            Redraw();
        }

        // =============== Drawing methods

        public void Redraw()
        {
            //picture.Clear(Color.White);
            DrawGrid();
            DrawField();
            DrawBoxes();
            DrawCursor();
        }

        void DrawGrid()
        {
            picture.DrawImage(grid, new Point(0,0));
        }

        void DrawField()
        {
            for (int y = 0; y < NUM_CELLS; y++)
                for (int x = 0; x < NUM_CELLS; x++)
                    if (field[x,y])
                        picture.FillRectangle(Brushes.LightGreen, new Rectangle(y * CELL_SIZE + 1, x * CELL_SIZE + 1, CELL_SIZE - 2, CELL_SIZE - 2));
        }

        void DrawBoxes()
        {
            int i = 0;
            int n = colors.Length;
            foreach (Rectangle rect in boxes)
            {
                Pen p = new Pen(colors[i++ % n]);
                picture.DrawRectangle(p, rect.X * CELL_SIZE + 4, rect.Y * CELL_SIZE + 4, rect.Width * CELL_SIZE - 8, rect.Height * CELL_SIZE - 8);
            }
        }

        void DrawCursor()
        {
            if (!Cursor.HasValue) return;

            picture.DrawRectangle(Pens.Blue, new Rectangle(Cursor.Value.X * CELL_SIZE, Cursor.Value.Y * CELL_SIZE, CELL_SIZE, CELL_SIZE));
            picture.DrawRectangle(Pens.LightBlue, new Rectangle(Cursor.Value.X * CELL_SIZE + 1, Cursor.Value.Y * CELL_SIZE + 1, CELL_SIZE - 2, CELL_SIZE - 2));
        }

        // =========== events

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            Point cell = PixelsToCell(e.X, e.Y);
            if (!IsInCellBounds(cell))
                return; // Out-of-bounds

            if (!Cursor.HasValue || cell != Cursor.Value)
            {
                //Console.WriteLine("Mouse cell move to " + cell.X + ", " + cell.Y);

                if (isInPlaceMode)
                {
                    field[cell.Y, cell.X] = cellPlaceMode;
                }

                Cursor = cell;

                Redraw();
            }
        }

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            isInPlaceMode = false;
            //Console.WriteLine("Placement Drag End");
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            isInPlaceMode = true;
            cellPlaceMode = !field[Cursor.Value.Y, Cursor.Value.X];
            //Console.WriteLine("Starting Placement Drag with mode " + (cellPlaceMode ? "PLACE" : "DEL"));
        }

        private void Canvas_MouseLeave(object sender, EventArgs e)
        {
            isInPlaceMode = false;
            //Console.WriteLine("Placement Drag End");
        }

        private void Canvas_MouseEnter(object sender, EventArgs e)
        {

        }

        private void Canvas_MouseClick(object sender, MouseEventArgs e)
        {
            if (!Cursor.HasValue) return;

            //Console.WriteLine("Mouse click at " + Cursor.Value.X + ", " + Cursor.Value.Y);

            field[Cursor.Value.Y, Cursor.Value.X] = !field[Cursor.Value.Y, Cursor.Value.X];

            Redraw();
        }

        // ===================== Helper functions

        Point PixelsToCell(int x, int y)
        {
            return new Point(x / CELL_SIZE, y / CELL_SIZE);
        }

        bool IsInCellBounds(Point p)
        {
            return p.X >= 0 && p.X < NUM_CELLS && p.Y >= 0 && p.Y < NUM_CELLS;
        }

    }
}
