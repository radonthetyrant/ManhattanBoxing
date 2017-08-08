using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManhattanBoxingProblem
{
    public interface IBoxingSolver
    {

        /// <summary>
        /// A name for this solver.
        /// </summary>
        string Name { get; }

        /// <summary>
        ///     Gets a 2-dimensional field of booleans. Fields that have been placed by the user are true.
        ///     The solver has to partition the marked space (all fields with "true") into the least amount of boxes in the shortest time.
        ///     The first dimension is the y-axis, the second dimension is the x-axis. The origin (0,0) is in the top left.
        /// </summary>
        /// <param name="field">
        ///     a clone of the 2-dimensional boolean field. Contents can be modified by the solver.
        /// </param>
        /// <returns>
        ///     A list of rectangles. Rectangles have a Location which is the position in the coordinate system.
        ///     And it has a height and width that is greater than zero.
        ///     A single-cell box located at the origin (0,0) has the location x:0, y:0 and the size height:1, width:1
        /// </returns>
        Rectangle[] Solve(bool[,] field);

    }
}
