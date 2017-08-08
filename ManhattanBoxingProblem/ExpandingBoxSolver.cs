using System;
using System.Collections.Generic;
using System.Drawing;

namespace ManhattanBoxingProblem
{
    public class ExpandingBoxSolver : IBoxingSolver
    {
        /// <summary>
        /// A reference to the field
        /// </summary>
        bool[,] field;

        /// <summary>
        /// Field dimensions
        /// </summary>
        int height, width;

        /// <summary>
        /// List of cells that haven't been covered by a box yet
        /// </summary>
        List<Point> openCells;

        /// <summary>
        /// Interface implementation.
        /// </summary>
        public string Name => "ExpandingBoxSolver";

        /// <summary>
        /// Solve method
        /// </summary>
        /// <param name="field"></param>
        /// <returns></returns>
        public Rectangle[] Solve(bool[,] field)
        {
            List<Rectangle> boxes = new List<Rectangle>();

            height = field.GetLength(0);
            width = field.GetLength(1);
            this.field = field;

            openCells = new List<Point>();

            for (int y = 0; y < height; y++)
                for (int x = 0; x < width; x++)
                    if (field[y, x])
                        openCells.Add(new Point(x, y));

            Random rand = new Random();

            while (openCells.Count > 0)
            {
                // Pick one at random
                int i = rand.Next() % openCells.Count;
                Point origin = openCells[i];
                Rectangle box = new Rectangle(origin.X, origin.Y, 1, 1); // Start with 1x1 box at this cell

                int numExpansions = 0;
                do
                {
                    //Console.WriteLine("Current box: " + boxes.Count + ", X,Y: " + box.X + "," + box.Y + ", h,w: " + box.Height + "," + box.Width);

                    numExpansions = 0;

                    // Try to expand north
                    if (CanExpandNorth(box))
                    {
                        ExpandNorth(ref box);
                        numExpansions++;
                    }

                    // Try to expand east
                    if (CanExpandEast(box))
                    {
                        ExpandEast(ref box);
                        numExpansions++;
                    }

                    // Try to expand south
                    if (CanExpandSouth(box))
                    {
                        ExpandSouth(ref box);
                        numExpansions++;
                    }

                    // Try to expand west
                    if (CanExpandWest(box))
                    {
                        ExpandWest(ref box);
                        numExpansions++;
                    }

                } while (numExpansions > 0);

                MarkCellsClosed(box);
                boxes.Add(box);
            }

            return boxes.ToArray();
        }

        /// <summary>
        /// Remove all contained cells from both the openCells list and mark them as covered in the field array
        /// </summary>
        /// <param name="box"></param>
        private void MarkCellsClosed(Rectangle box)
        {
            List<Point> removeCells = new List<Point>();

            // Remove all intercecting cells from the openlist
            foreach (Point cell in openCells)
            {
                if (cell.X >= box.Left && cell.X < box.Right
                    && cell.Y >= box.Top && cell.Y < box.Bottom)
                {
                    // It intersects
                    removeCells.Add(cell);
                }
            }

            foreach (Point p in removeCells)
                openCells.Remove(p);

            // Mark the field as occupied
            for (int y = box.Top; y < box.Bottom; y++)
                for (int x = box.Left; x < box.Right; x++)
                    field[y, x] = false;
        }

        /// <summary>
        /// Expand the box towards the direction
        /// </summary>
        /// <param name="box"></param>
        private void ExpandNorth(ref Rectangle box)
        {
            box.Location = new Point(box.Location.X, box.Location.Y - 1);
            box.Height += 1;
        }

        /// <summary>
        /// Expand the box towards the direction
        /// </summary>
        /// <param name="box"></param>
        private void ExpandEast(ref Rectangle box)
        {
            box.Width += 1;
        }

        /// <summary>
        /// Expand the box towards the direction
        /// </summary>
        /// <param name="box"></param>
        private void ExpandSouth(ref Rectangle box)
        {
            box.Height += 1;
        }

        /// <summary>
        /// Expand the box towards the direction
        /// </summary>
        /// <param name="box"></param>
        private void ExpandWest(ref Rectangle box)
        {
            box.Location = new Point(box.Location.X - 1, box.Location.Y);
            box.Width += 1;
        }

        /// <summary>
        /// Check if the box can be expanded into that direction
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private bool CanExpandNorth(Rectangle box)
        {
            int y = box.Top - 1;
            if (y < 0) return false;
            int x_start = box.Left;
            int x_end = box.Right;

            for (int x = x_start; x < x_end; x++)
            {
                if (!field[y, x]) // if there is a field which is not marked (marked=true) then this collides.
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Check if the box can be expanded into that direction
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private bool CanExpandEast(Rectangle box)
        {
            int x = box.Right;
            if (x >= width) return false;
            int y_start = box.Top;
            int y_end = box.Bottom;

            for (int y = y_start; y < y_end; y++)
            {
                if (!field[y, x]) // if there is a field which is not marked (true) then this collides.
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Check if the box can be expanded into that direction
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private bool CanExpandSouth(Rectangle box)
        {
            int y = box.Bottom;
            if (y >= height) return false;
            int x_start = box.Left;
            int x_end = box.Right;

            for (int x = x_start; x < x_end; x++)
            {
                if (!field[y, x]) // if there is a field which is not marked (true) then this collides.
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Check if the box can be expanded into that direction
        /// </summary>
        /// <param name="box"></param>
        /// <returns></returns>
        private bool CanExpandWest(Rectangle box)
        {
            int x = box.Left - 1;
            if (x < 0) return false;
            int y_start = box.Top;
            int y_end = box.Bottom;

            for (int y = y_start; y < y_end; y++)
            {
                if (!field[y, x]) // if there is a field which is not marked (true) then this collides.
                    return false;
            }

            return true;
        }

    }
}
