/* CircuitFinderTests.cs
 * Author: Rod Howell
 */
using System.Drawing;
using System.Security.Cryptography.Xml;

namespace Ksu.Cis300.TravelingSalesperson.Tests
{
    /// <summary>
    /// Unit tests for the CircuitFinder class.
    /// </summary>
    public class CircuitFinderTests
    {
        /// <summary>
        /// The points used in each of the tests.
        /// </summary>
        Point[][] _testCasePoints = new Point[][]
        {
            new Point[] { new Point(100, 100), new Point(400, 100), new Point(400, 500) }, // ShortTest(0)
            new Point[]
            {
                new Point(200, 200), new Point(700, 200), new Point(200, 600),             // ShortTest(1)
                new Point(400, 600)
            },
            new Point[]
            {
                new Point(200, 200), new Point(400, 600), new Point(700, 200),             // ShortTest(2)
                new Point(200, 600)                                                        //
            },
            new Point[]
            {
                new Point(100, 100), new Point(156, 100), new Point(120, 115),             // ShortTest(3)
                new Point(156, 135), new Point(100, 135)                                   //
            },
            new Point[]
            {
                new Point(229, 71), new Point(165, 149), new Point(301, 152),              // ShortTest(4)
                new Point(227, 220)                                                        // 
            },
            new Point[]
            {
                new Point(100, 500), new Point(100, 100), new Point(105, 100),             // LongTest(5)
                new Point(110, 100), new Point(115, 100), new Point(120, 100),             //
                new Point(125, 100), new Point(125, 900), new Point(120, 900),             //
                new Point(115, 900), new Point(110, 900), new Point(105, 900),             //
                new Point(100, 900)                                                        //
            },
            new Point[]
            {
                new Point(442, 302), new Point(358, 229), new Point(538, 230),             // LongTest(6)
                new Point(359, 374), new Point(551, 372), new Point(441, 158),             //
                new Point(452, 415), new Point(284, 289), new Point(598, 280),             //
                new Point(299, 154), new Point(567, 158), new Point(580, 423),             //
                new Point(292, 409)                                                        //
            },
            new Point[]
            {
                new Point(100, 220), new Point(260, 100), new Point(100, 100),             // ShortTest(7)
                new Point(10, 100), new Point(150, 100)                                    //
            },
            new Point[]
            {
                new Point(448, 209), new Point(1228, 237), new Point(1200, 654),           // LongTest(8)
                new Point(400, 668), new Point(805, 445), new Point(739, 175),             //
                new Point(776, 746), new Point(174, 383), new Point(1339, 423),            //
                new Point(1039, 436), new Point(102, 61), new Point(104, 561),             //
                new Point(901, 720), new Point(866, 264), new Point(1449, 459),            //
                new Point(958, 857), new Point(272, 837)                                   //
            }
        };

        /// <summary>
        /// The expected length of the shortest circuit for each of the tests. The length found
        /// must be within 0.00001 of the expected length.
        /// </summary>
        double[] _testCaseLengths = new double[]
        {
            1200,             // ShortTest(0)
            1600,             // ShortTest(1)
            1600,             // ShortTest(2)
            190,              // ShortTest(3)
            404.029371419559, // ShortTest(4)
            1650,             // LongTest(5)
            1323.87782779161, // LongTest(6)
            600,              // ShortTest(7)
            4204.888988740258 // LongTest(8)
        };

        /// <summary>
        /// The expected circuit for each of the tests. 
        /// </summary>
        Point[][] _testCaseResults = new Point[][]
        {
            new Point[]
            {
                new Point(400, 500), new Point(400, 100), new Point(100, 100) // ShortTest(0)
            },
            new Point[]
            {
                new Point(400, 600), new Point(700, 200), new Point(200, 200),
                new Point(200, 600)                                              // ShortTest(1)
            },
            new Point[]
            {
                new Point(200, 600), new Point(400, 600), new Point(700, 200),
                new Point(200, 200)                                             // ShortTest(2)
            },
            new Point[]
            {
                new Point(100, 135), new Point(156, 135),                       // ShortTest(3)
                new Point(156, 100), new Point(120, 115), new Point(100, 100)
            },
            new Point[]
            {
                new Point(227, 220),                                           // ShortTest(4)
                new Point(165, 149), new Point(229, 71), new Point(301, 152)
            },
            new Point[]
            {
                new Point(100, 900), new Point(105, 900),                      // LongTest(5)
                new Point(110, 900), new Point(115, 900), new Point(120, 900), //
                new Point(125, 900), new Point(125, 100), new Point(120, 100), //
                new Point(115, 100), new Point(110, 100), new Point(105, 100), //
                new Point(100, 100), new Point(100, 500)
            },
            new Point[]
            {
                new Point(292, 409), new Point(284, 289), new Point(358, 229), // LongTest(6)
                new Point(299, 154), new Point(441, 158), new Point(567, 158), //
                new Point(538, 230), new Point(598, 280), new Point(551, 372), //
                new Point(580, 423), new Point(452, 415), new Point(442, 302), //
                new Point(359, 374)
            },
            new Point[]
            {
                new Point(150, 100), new Point(260, 100), new Point(100, 220), // ShortTest(7)
                new Point(10, 100), new Point(100, 100)                        // 
            },
            new Point[]
            {
                new Point(272, 837), new Point(400, 668), new Point(776, 746),     // LongTest(8)
                new Point(901, 720), new Point(958, 857), new Point(1200, 654),    //
                new Point(1449, 459), new Point(1339, 423), new Point(1228, 237),  //
                new Point(1039, 436), new Point(805, 445), new Point(866, 264),    //
                new Point(739, 175), new Point(448, 209), new Point(102, 61),      //
                new Point(174, 383), new Point(104, 561)                           //
            }
        };

        /// <summary>
        /// Runs the test case provided at the given index within the above fields.
        /// </summary>
        /// <param name="index">The index for the test case.</param>
        public void TestGetShortestCircuit(int index)
        {
            List<Point> pointsList = new List<Point>(_testCasePoints[index]);
            double len = CircuitFinder.FindShortestCircuit(pointsList, out Point[] result);
            Assert.Multiple(() =>
            {
                Assert.That(Math.Abs(len - _testCaseLengths[index]), Is.LessThan(0.000001));
                Assert.That(_testCaseResults[index], Is.EqualTo(result));
            });
        }

        /// <summary>
        /// Tests whether calling FindShortestCircuit with a null list throws the proper exception.
        /// </summary>
        [Test, Category("A: Error tests")]
        [Timeout(1000)]
        public void NullTest()
        {
            // We override the compiler warning to check that this case is handled properly.
            Assert.Throws<ArgumentNullException>(() => CircuitFinder.FindShortestCircuit(null!, out Point[] _));
        }

        /// <summary>
        /// Tests whether calling FindShortestCircuit with an empty list of points throws the proper
        /// exception.
        /// </summary>
        [Test, Category("A: Error tests")]
        [Timeout(1000)]
        public void EmptyListTest()
        {
            Assert.Throws<ArgumentException>(() => CircuitFinder.FindShortestCircuit(new List<Point>(),
                out Point[] _));
        }

        /// <summary>
        /// Tests whether calling FindShortestCircuit with a list of 2 points throws the proper
        /// exception.
        /// </summary>
        [Test, Category("A: Error tests")]
        [Timeout(1000)]
        public void ShortListTest()
        {
            List<Point> list = new List<Point>();
            list.Add(new Point(100, 100));
            list.Add(new Point(200, 200));
            Assert.Throws<ArgumentException>(() => CircuitFinder.FindShortestCircuit(list, out Point[] _));
        }

        /// <summary>
        /// Runs the test case provided at the given index with a timeout of 1 second.
        /// </summary>
        /// <param name="index">The index for the test case.</param>
        [Test, Category("B: Short tests")]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        [TestCase(7)]
        [Timeout(1000)]
        public void ShortTest(int index)
        {
            TestGetShortestCircuit(index);
        }

        /// <summary>
        /// Runs the test case provided at the given index with a timeout of 20 seconds.
        /// </summary>
        /// <param name="index">The index for the test case.</param>
        [Test, Category("C: Long tests")]
        [TestCase(5)]
        [TestCase(6)]
        [TestCase(8)]
        [Timeout(20000)]
        public void LongTest(int index)
        {
            TestGetShortestCircuit(index);
        }
    }
}