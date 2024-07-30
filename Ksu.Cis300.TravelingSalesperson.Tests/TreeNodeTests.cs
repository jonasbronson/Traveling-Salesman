/* TreeNodeTests.cs
 * Author: Rod Howell
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ksu.Cis300.TravelingSalesperson.Tests
{
    /// <summary>
    /// A suite of unit tests for the TreeNode class.
    /// </summary>
    public class TreeNodeTests
    {
        /// <summary>
        /// The points used for the tests.
        /// </summary>
        private static Point[] _points = new Point[]
        {
            new Point(100, 100), new Point(100, 500), new Point(400, 500), new Point(400, 100),
            new Point(400, 725)
        };

        /// <summary>
        /// Gets a list containing the given number of points.
        /// </summary>
        /// <param name="n">The number of points to get - must be at least 0 and less than 5.</param>
        /// <returns>A list of n points.</returns>
        private static List<Point> GetList(int n)
        {
            List<Point> points = new();
            for (int i = 0; i < n; i++)
            {
                points.Add(_points[i]);
            }
            return points;
        }

        /// <summary>
        /// Tests that the children of the given parent node are generated correctly.
        /// </summary>
        /// <param name="parent">The parent node.</param>
        /// <param name="isComplete">Whether the children are expected to represent complete circuits.</param>
        /// <param name="bounds">The expected lower bound for each child.</param>
        /// <param name="allPoints">All of the points.</param>
        private void TestChildren(TreeNode parent, bool isComplete, double[] bounds,
            List<Point> allPoints)
        {
            List<TreeNode> children = parent.Children;
            List<Point> checkPoints = new();
            checkPoints.AddRange(parent.Path);
            Assert.Multiple(() =>
            {
                Assert.That(children, Has.Count.EqualTo(bounds.Length));
                for (int i = 0; i < children.Count; i++)
                {
                    Assert.That(children[i].IsComplete, Is.EqualTo(isComplete),
                        $"The IsComplete property for child {i} is incorrect.");
                    Assert.That(children[i].Path, Has.Length.EqualTo(parent.Path.Length + 1),
                        $"The length of the Path property for child {i} is incorrect.");
                    for (int j = 0; j < parent.Path.Length; j++)
                    {
                        Assert.That(children[i].Path[j], Is.EqualTo(parent.Path[j]),
                            $"Element {j} of the Path property for child {i} is incorrect.");
                    }
                    checkPoints.Add(children[i].Path.Last());
                    Assert.That(children[i].LowerBound, Is.EqualTo(bounds[i]),
                        $"The lower bound for child {i} is incorrect.");
                }
                if (checkPoints.Count < allPoints.Count)
                {
                    checkPoints.Add(allPoints[0]);
                }
                Assert.That(checkPoints, Is.EquivalentTo(allPoints),
                    $"The last point in at least one child's Path property is incorrect.");
            });
        }

        /// <summary>
        /// Gets the child whose path ends in the given point from the given list of children.
        /// </summary>
        /// <param name="children">The children.</param>
        /// <param name="end">The end of the path.</param>
        /// <returns>The child whose path ends at end.</returns>
        private TreeNode? GetChild(List<TreeNode> children, Point end)
        {
            foreach (TreeNode child in children)
            {
                Point[] points = child.Path;
                if (points.Last() == end)
                {
                    return child;
                }
            }
            return null;
        }

        /// <summary>
        /// Tests that passing a null list to the constructor throws the correct exception.
        /// </summary>
        [Test, Category("A: Constructor tests")]
        [Timeout(1000)]
        public void TestConstructorNull()
        {
            // We'll override the warning in order to check that a null is handled correctly.
            Assert.Throws<ArgumentNullException>(() => new TreeNode(null!));
        }

        /// <summary>
        /// Tests that the constructor works correctly on an empty list.
        /// </summary>
        [Test, Category("A: Constructor tests")]
        [Timeout(1000)]
        public void TestConstructorEmpty()
        {
            Assert.Throws<ArgumentException>(() => new TreeNode(GetList(0)));
        }

        /// <summary>
        /// Tests that the constructor works correctly on a list of 2 points.
        /// </summary>
        [Test, Category("A: Constructor tests")]
        [Timeout(1000)]
        public void TestConstructor2Points()
        {
            Assert.Throws<ArgumentException>(() => new TreeNode(GetList(2)));
        }

        /// <summary>
        /// Tests that the IsComplete, Path, and LowerBound properties return the correct values
        /// when constructing a node from 3 points.
        /// </summary>
        [Test, Category("A: Constructor tests")]
        [Timeout(1000)]
        public void TestConstructor3Points()
        {
            TreeNode t = new(GetList(3));
            Assert.Multiple(() =>
            {
                Assert.That(t.IsComplete, Is.False);
                Assert.That(t.Path, Is.EquivalentTo(new Point[] { _points[2] }));
                Assert.That(t.LowerBound, Is.EqualTo(1000));
            });
        }

        /// <summary>
        /// Tests that the IsComplete, Path, and LowerBound properties return the correct values
        /// when constructing a node from 4 points.
        /// </summary>
        [Test, Category("A: Constructor tests")]
        [Timeout(1000)]
        public void TestConstructor4Points()
        {
            TreeNode t = new(GetList(4));
            Assert.Multiple(() =>
            {
                Assert.That(t.IsComplete, Is.False);
                Assert.That(t.Path, Is.EquivalentTo(new Point[] { _points[3] }));
                Assert.That(t.LowerBound, Is.EqualTo(1200));
            });
        }

        /// <summary>
        /// Tests that the children of the root are correct when 3 points are used.
        /// </summary>
        [Test, Category("B: Children of root tests")]
        [Timeout(1000)]
        public void TestChildren3Points()
        {
            List<Point> allPoints = GetList(3);
            TestChildren(new TreeNode(allPoints), false, new double[] { 1000 }, allPoints);
        }

        /// <summary>
        /// Tests that the children of the root are correct when 4 points are used.
        /// </summary>
        [Test, Category("B: Children of root tests")]
        [Timeout(1000)]
        public void TestChildren4Points()
        {
            List<Point> allPoints = GetList(4);
            TestChildren(new TreeNode(allPoints), false, new double[] { 1400, 1500 }, allPoints);
        }

        /// <summary>
        /// Tests that the children of the root are correct when 4 points are used.
        /// </summary>
        [Test, Category("B: Children of root tests")]
        [Timeout(1000)]
        public void TestChildren5Points()
        {
            List<Point> allPoints = GetList(5);
            TestChildren(new TreeNode(allPoints), false, new double[] { 1750, 1500, 1350 }, allPoints);
        }

        /// <summary>
        /// Tests that the gradchild of the root is correct when 3 points are used.
        /// </summary>
        [Test, Category("C: Grandchildren of root tests")]
        [Timeout(1000)]
        public void TestGrandchildren3Points()
        {
            List<Point> allPoints = GetList(3);
            TreeNode root = new(allPoints);
            TreeNode child = root.Children[0];
            TestChildren(child, true, new double[] { 1200 }, allPoints);
        }

        /// <summary>
        /// Tests that the children of one of the children are correct when using 4 nodes.
        /// </summary>
        [Test, Category("C: Grandchildren of root tests")]
        [Timeout(1000)]
        public void TestGrandchildren4Points()
        {
            List<Point> allPoints = GetList(4);
            TreeNode root = new(allPoints);

            // Because one child should have a path ending in (100, 500), GetChild shouldn't
            // return null.
            TreeNode child = GetChild(root.Children, new Point(100, 500))!;
            TestChildren(child, false, new double[] { 1600, 1700 }, allPoints);
        }

        /// <summary>
        /// Tests that the children of one of the children are correct when using 5 nodes.
        /// </summary>
        [Test, Category("C: Grandchildren of root tests")]
        [Timeout(1000)]
        public void TestGrandchildren5Points()
        {
            List<Point> allPoints = GetList(5);
            TreeNode root = new(allPoints);

            // Because one child should have a path ending in (400, 500), GetChild shouldn't
            // return null.
            TreeNode child = GetChild(root.Children, new Point(400, 500))!;
            TestChildren(child, false, new double[] { 1700, 1500 }, allPoints);
        }

        /// <summary>
        /// Tests that the only grandchild when using 3 nodes has no children.
        /// </summary>
        [Test, Category("C: Grandchildren of root tests")]
        [Timeout(1000)]
        public void TestEmptyChildren()
        {
            List<Point> allPoints = GetList(3);
            TreeNode root = new(allPoints);
            TreeNode child = root.Children[0];
            TreeNode grandchild = child.Children[0];
            Assert.That(grandchild.Children, Is.Empty);
        }
    }
}
