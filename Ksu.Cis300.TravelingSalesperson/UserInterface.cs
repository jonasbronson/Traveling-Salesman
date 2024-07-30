/* UserInterface.cs
 * Author: Rod Howell
 */
namespace Ksu.Cis300.TravelingSalesperson
{
    /// <summary>
    /// A GUI for a program to find shortest circuits through a set of points on the plane.
    /// </summary>
    public partial class UserInterface : Form
    {
        /// <summary>
        /// The points on the canvas.
        /// </summary>
        private List<Point> _points = new();

        /// <summary>
        /// The last known mouse location.
        /// </summary>
        private Point _lastLocation = new Point(-1, -1);

        /// <summary>
        /// Constructs the GUI.
        /// </summary>
        public UserInterface()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Plots the given point.
        /// </summary>
        /// <param name="p">The point to plot.</param>
        private void Plot(Point p)
        {
            uxDrawing.DrawLine(new Point(p.X - 1, p.Y - 1), new Point(p.X + 1, p.Y + 1));
            uxDrawing.DrawLine(new Point(p.X - 1, p.Y + 1), new Point(p.X + 1, p.Y - 1));
        }

        /// <summary>
        /// Handles a MouseClick event on the drawing.
        /// </summary>
        /// <param name="sender">The object signaling the event.</param>
        /// <param name="e">Information about the event.</param>
        private void DrawingMouseClick(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            Plot(p);
            _points.Add(p);
            uxPoints.Items.Add(p);
            if (_points.Count > 2)
            {
                uxFindCircuit.Enabled = true;
            }
        }

        /// <summary>
        /// Handles a MouseMove event on the drawing.
        /// </summary>
        /// <param name="sender">The object signaling the event.</param>
        /// <param name="e">Information about the event.</param>
        private void DrawingMouseMove(object sender, MouseEventArgs e)
        {
            Point p = e.Location;
            if (p != _lastLocation)
            {
                uxMouseLocation.SetToolTip(uxDrawing, p.ToString());
                _lastLocation = p;
            }
        }

        /// <summary>
        /// Handles a Click event on the "Clear" button.
        /// </summary>
        /// <param name="sender">The object signaling the event.</param>
        /// <param name="e">Information about the event.</param>
        private void ClearClick(object sender, EventArgs e)
        {
            uxDrawing.Clear();
            _points.Clear();
            uxPoints.Items.Clear();
            uxFindCircuit.Enabled = false;
        }

        /// <summary>
        /// Draws the given circuit.
        /// </summary>
        /// <param name="circuit">The points forming the circuit, listed in order of the circuit.</param>
        private void DrawCircuit(Point[] circuit)
        {
            for (int i = 1; i < circuit.Length; i++)
            {
                uxDrawing.DrawLine(circuit[i - 1], circuit[i]);
            }
            uxDrawing.DrawLine(circuit[circuit.Length - 1], circuit[0]);
        }

        /// <summary>
        /// Replaces the points in the ListBox of points with the given points in the given order.
        /// </summary>
        /// <param name="circuit">The points to list.</param>
        private void SetCircuitPoints(Point[] circuit)
        {
            uxPoints.Items.Clear();
            foreach (Point p in circuit)
            {
                Plot(p);
                uxPoints.Items.Add(p);
            }
        }

        /// <summary>
        /// Handles a Click event on the "Find Circuit" button.
        /// </summary>
        /// <param name="sender">The object signaling the event.</param>
        /// <param name="e">Information about the event.</param>
        private void FindCircuitClick(object sender, EventArgs e)
        {
            double len = CircuitFinder.FindShortestCircuit(_points, out Point[] circuit);
            uxDrawing.Clear();
            SetCircuitPoints(circuit);
            DrawCircuit(circuit);
            MessageBox.Show("Circuit Length: " + len);
        }
    }
}