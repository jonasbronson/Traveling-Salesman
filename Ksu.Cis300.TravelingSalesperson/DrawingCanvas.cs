/* DrawingCanvas.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace Ksu.Cis300.TravelingSalesperson
{
    /// <summary>
    /// A control on which lines can be drawn.
    /// </summary>
    public partial class DrawingCanvas : UserControl
    {
        /// <summary>
        /// The lines contained in the drawing.
        /// </summary>
        private List<(Point, Point)> _lines = new();

        /// <summary>
        /// Constructs the drawing.
        /// </summary>
        public DrawingCanvas()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles a Paint event on the control.
        /// </summary>
        /// <param name="e">Information about the event.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            Pen pen = new(ForeColor);
            foreach((Point, Point) line in _lines)
            {
                g.DrawLine(pen, line.Item1, line.Item2);
            }
        }

        /// <summary>
        /// Draws the given line.
        /// </summary>
        /// <param name="a">One endpoint of the line segment.</param>
        /// <param name="b">The other endpoint.</param>
        public void DrawLine(Point a, Point b)
        {
            _lines.Add((a, b));
            Invalidate();
        }

        /// <summary>
        /// Clears all the lines from the drawing.
        /// </summary>
        public void Clear()
        {
            _lines.Clear();
            Invalidate();
        }
    }
}
