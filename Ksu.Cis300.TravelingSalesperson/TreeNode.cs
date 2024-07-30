/* TreeNode.cs
 * Author: Jonas Bronson
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace Ksu.Cis300.TravelingSalesperson
{
    /// <summary>
    /// Creates TreeNodes containing points remaining and points inside the circuit already.
    /// Uses Children to find the optimal path to find the shortest path.
    /// </summary>
    public class TreeNode
    {
        /// <summary>
        /// The min amount of points a user is allowed to enter.
        /// </summary>
        public static readonly int _minPoints = 3;

        /// <summary>
        /// Array of points to find, contains all of the points the user entered.
        /// </summary>
        private readonly Point[] _pointsToFind;

        /// <summary>
        /// The distances between all points given.
        /// </summary>
        private readonly double[,] _distanceBetweenTwo;

        /// <summary>
        /// The indicies in the current path of the shortest circuit.
        /// </summary>
        private readonly int[] _indiciesOfPath;

        /// <summary>
        /// The indicies not yet in the current path of the shortest circuit.
        /// </summary>
        private readonly int[] _indiciesNotInPath;

        /// <summary>
        /// The current length of the circuit to the most recent node in the current path.
        /// </summary>
        private readonly double _lengthOfPathToNode;

        /// <summary>
        /// If all of the circuits should be searched.
        /// </summary>
        private readonly bool _allCircuitsShouldSearch;

        /// <summary>
        /// Gives the lower bound for the circuit.
        /// </summary>
        public double LowerBound { get; }

        /// <summary>
        /// Instantiates the private fields of TreeNode and the LowerBound.
        /// </summary>
        /// <param name="points">The points being used to find the shortest circuit.</param>
        /// <exception cref="ArgumentNullException">If the given points are all null.</exception>
        /// <exception cref="ArgumentException">If the points are not of the minimum amount.</exception>
        public TreeNode(List<Point> points)
        {
            if (points == null)
            {
                throw new ArgumentNullException();
            }
            if (points.Count < _minPoints)
            {
                throw new ArgumentException();
            }

            _distanceBetweenTwo = FindDistances(points);
            
            _pointsToFind = points.ToArray();

            _indiciesOfPath = new int[1];
            _indiciesOfPath[0] = points.Count - 1;

            _indiciesNotInPath = new int[points.Count - 1];
            for (int i = 0; i < points.Count -1; i++)
            {
                _indiciesNotInPath[i] = i;
            }
            
            _lengthOfPathToNode = 0;
            _allCircuitsShouldSearch = false;

            LowerBound = FindLowerBound(_indiciesOfPath[0], _indiciesOfPath[0], _indiciesNotInPath);
        }

        /// <summary>
        /// Instantiates the private fields of TreeNode and the LowerBound. Used when building child nodes.
        /// </summary>
        /// <param name="parent">The node that is the parent of this node (the child).</param>
        /// <param name="indiciesOnPath">The indicies that are currently on the path created.</param>
        /// <param name="indiciesNotOnPath">The indicies that are currently not on the path created.</param>
        /// <param name="length">The length of the path thus far.</param>
        /// <param name="allCircuitsShouldSearch">If the search should be narrowed down.</param>
        private TreeNode(TreeNode parent, int[] indiciesOnPath, int[] indiciesNotOnPath, double length, bool allCircuitsShouldSearch)
        {
            _indiciesOfPath = indiciesOnPath;
            _indiciesNotInPath = indiciesNotOnPath;
            _lengthOfPathToNode = length;
            _allCircuitsShouldSearch = allCircuitsShouldSearch;
            _pointsToFind = parent._pointsToFind;
            _distanceBetweenTwo = parent._distanceBetweenTwo;
            LowerBound = FindLowerBound(indiciesOnPath[indiciesOnPath.Length - 1], indiciesOnPath[0], indiciesNotOnPath) + length;
        }

        /// <summary>
        /// Bool returning if the current node represents a completed circuit.
        /// </summary>
        public bool IsComplete
        {
            get
            {
                if (_indiciesNotInPath.Length == 0) return true;
                else return false;
            }
        }

        /// <summary>
        /// Returns an array of the points in the current path that is traveled.
        /// </summary>
        public Point[] Path
        {
            get
            {
                return FindUsedPoints(_pointsToFind);
            }
        }

        /// <summary>
        /// Makes children of the current node, then returns them in a List of TreeNodes.
        /// </summary>
        public List<TreeNode> Children
        {
            get
            {
                if(IsComplete)
                {
                    return new List<TreeNode>();
                }
                else
                {
                    int start;
                    if(_allCircuitsShouldSearch)
                    {
                        start = 0;
                    }
                    else
                    {
                        start = 1;
                    }
                    List<TreeNode> children = new List<TreeNode>();
                    int temp = _indiciesNotInPath[_indiciesNotInPath.Length - 1];
                    int[] tIndiciesOfPath = MakeNewChild(temp, out int[] remaining);
                    double tLength = _lengthOfPathToNode + _distanceBetweenTwo[tIndiciesOfPath[tIndiciesOfPath.Length - 1], tIndiciesOfPath[tIndiciesOfPath.Length - 2]];
                    TreeNode node = new(this, tIndiciesOfPath, remaining, tLength, _allCircuitsShouldSearch);
                    children.Add(node);

                    for(int i = start; i < _indiciesNotInPath.Length - 1; i++)
                    {
                        tIndiciesOfPath = MakeNewChild(_indiciesNotInPath[i], out remaining);
                        tLength = _lengthOfPathToNode + _distanceBetweenTwo[tIndiciesOfPath[tIndiciesOfPath.Length - 1], tIndiciesOfPath[tIndiciesOfPath.Length - 2]];
                        remaining[i] = temp;
                        TreeNode newNode = new(this, tIndiciesOfPath, remaining, tLength, _allCircuitsShouldSearch || i == 1);
                        children.Add(newNode);
                    }
                    return children;
                }
            }
        }

        /// <summary>
        /// Distance formula calculating the distances between two points.
        /// </summary>
        /// <param name="a">First point</param>
        /// <param name="b">Second point</param>
        /// <returns>The distance between the two given points.</returns>
        private double Distance(Point a, Point b)
        {
            int diffX = a.X - b.X;
            int diffY = a.Y - b.Y;
            return Math.Sqrt(diffX * diffX + diffY * diffY);
        }

        /// <summary>
        /// Finds the distances between all of the possible points given.
        /// </summary>
        /// <param name="points">The points selected on the GUI.</param>
        /// <returns>A 2D array with all of the possible distances between the given points.</returns>
        private double[,] FindDistances(List<Point> points)
        {
            double[,] distances = new double[points.Count, points.Count];
            for (int i = 0; i < points.Count; i++)
            {
                for (int j = 0; j < points.Count; j++)
                {
                    if (i == j)
                    {
                        distances[i, j] = double.PositiveInfinity;
                    }
                    else
                    {
                        distances[i, j] = Distance(points[i], points[j]);
                    }
                }
            }
            return distances;
        }

        /// <summary>
        /// Uses the indicies of used points to return the new path in order of the shortest circuit.
        /// </summary>
        /// <param name="points">The points selected on the GUI.</param>
        /// <returns>An array of points containing the order for the shortest circuit.</returns>
        private Point[] FindUsedPoints(Point[] points)
        {
            Point[] newOrder = new Point[_indiciesOfPath.Length];
            for(int i = 0; i < _indiciesOfPath.Length; i++)
            {
                newOrder[i] = points[_indiciesOfPath[i]];
            }
            return newOrder;
        }

        /// <summary>
        /// Gets the minimum distance between the points in remaining and the points start and end.
        /// </summary>
        /// <param name="start">The start of where to find min distance.</param>
        /// <param name="end">The end of where to find min distance.</param>
        /// <param name="remaining">The array of remaining points not used in the current path.</param>
        /// <returns>The minimum distance found as a double.</returns>
        private double GetMinimumDistance(int start, int end, int[] remaining)
        {
            double endDistance = _distanceBetweenTwo[end, start];
            foreach(int point in remaining)
            {
                endDistance = Math.Min(endDistance, _distanceBetweenTwo[point, end]);
            }
            return endDistance;
        }

        /// <summary>
        /// Finds the LowerBound by using GetMinimumDistance to find min distances, then totaling them to equal a total minimum length.
        /// </summary>
        /// <param name="start">The start of where to find the lower bound.</param>
        /// <param name="end">The end of where to find the lower bound.</param>
        /// <param name="remaining">The array of remaining points not used in the current path.</param>
        /// <returns>Returns the total min lower bound as a double.</returns>
        private double FindLowerBound(int start, int end, int[] remaining)
        {
            double minLength = GetMinimumDistance(start, end, remaining);
            foreach (int point in remaining)
            {
                minLength += GetMinimumDistance(start, point, remaining);
            }
            return minLength;
        }

        /// <summary>
        /// Makes a new child with new lengths for the arrays giving the used points and unused points, then returns the new path.
        /// </summary>
        /// <param name="index">Index stored at the location that needs to be moved.</param>
        /// <param name="remaining">The array of remaining points still to be added to the completed circuit.</param>
        /// <returns>The new path containing the points used in the circuit.</returns>
        private int[] MakeNewChild(int index, out int[] remaining)
        {
            int[] newPath = new int[_indiciesOfPath.Length + 1];
            _indiciesOfPath.CopyTo(newPath, 0);
            newPath[newPath.Length - 1] = index;
            remaining = new int[_indiciesNotInPath.Length - 1];
            Array.Copy(_indiciesNotInPath, 0, remaining, 0, remaining.Length);
            return newPath;
        }
    }
}
