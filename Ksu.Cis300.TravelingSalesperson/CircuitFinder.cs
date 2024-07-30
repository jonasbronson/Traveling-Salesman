/* CircuitFinder.cs
* Author: Rod Howell + Jonas Bronson
*/
using Ksu.Cis300.PriorityQueueLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ksu.Cis300.TravelingSalesperson
{
    /// <summary>
    /// A class containing methods for finding a shortest circuit through a set of points.
    /// </summary>
    public static class CircuitFinder
    {
        /// <summary>
        /// The min amount of points a user is allowed to enter.
        /// </summary>
        public static readonly int _minPoints = 3;

        /// <summary>
        /// Finds the shortest circuit via the children and LowerBound of the tree.
        /// </summary>
        /// <param name="points">All of the points selected on the GUI.</param>
        /// <param name="circuit">The points in order of the shortest circuit.</param>
        /// <returns>A double with the length of the shortest circuit.</returns>
        /// <exception cref="ArgumentNullException">If the points passed are null.</exception>
        /// <exception cref="ArgumentException">If the points passed are not equal or greater than the minimum that points can be.</exception>
        public static double FindShortestCircuit(List<Point> points, out Point[] circuit)
        {
            if (points == null)
            {
                throw new ArgumentNullException();
            }
            if(points.Count < _minPoints)
            {
                throw new ArgumentException();
            }
            TreeNode tree = new TreeNode(points);
            MinPriorityQueue<double, TreeNode> minPriority = new();
            while (tree.IsComplete == false)
            {
                List<TreeNode> children = tree.Children;
                foreach (TreeNode child in children)
                {
                    minPriority.Add(child.LowerBound, child);
                }
                tree = minPriority.RemoveMinPriorityElement();
            }
            circuit = tree.Path;
            return tree.LowerBound;
        }
    }
}
