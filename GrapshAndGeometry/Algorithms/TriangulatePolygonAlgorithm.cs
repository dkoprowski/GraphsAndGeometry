using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapshAndGeometry.Algorithms
{
    class TriangulatePolygonAlgorithm : IAlgorithm
    {
        private bool upperLine;
        private Stack<Point> stackedVerticles;
        public void Run()
        {
            /*
            var p = new Point(0, 0);
            var q = new Point(4, 2);
            var r = new Point(1, 4);
            var s = new Point(3, -1);

            Console.WriteLine("o1 " + PointOrientation(p, q, r));
            Console.WriteLine("o2 " + PointOrientation(p, q, s));
            Console.WriteLine("o3 " + PointOrientation(r, s, p));
            Console.WriteLine("o4 " + PointOrientation(r, s, q));
            */
            
            /*
            List<Point> polygon = GenerateMonotonePolygon();
            
            Console.WriteLine(IsLineSegmentInsidePolygon(polygon, new Point(2,2), new Point(12,8)));
            Console.WriteLine(IsLineSegmentInsidePolygon(polygon, new Point(4,8), new Point(13,2)));

            Console.WriteLine(IsLineSegmentInsidePolygon(polygon, new Point(4, 8), new Point(8, 3)));
            Console.WriteLine(IsLineSegmentInsidePolygon(polygon, new Point(4, 8), new Point(6,5)));
            Console.WriteLine(IsLineSegmentInsidePolygon(polygon, new Point(4, 8), new Point(10,6)));
            
            Console.WriteLine(IsLineSegmentInsidePolygon(polygon, new Point(6,5), new Point(10,6)));
            Console.WriteLine(IsLineSegmentInsidePolygon(polygon, new Point(8,3), new Point(11,2)));
            */

            SweepTriangulation();
        }

        private void SweepTriangulation()
        {
            stackedVerticles = new Stack<Point>();
            List<Point> polygon = GenerateMonotonePolygon();
            polygon.ForEach(vert => Console.WriteLine(vert));
            Console.WriteLine();
            var sortedPolygon = new List<Point>(polygon);
            sortedPolygon.Sort();

            if (sortedPolygon[0].y <= sortedPolygon[1].x)
                upperLine = true;
            else
                upperLine = false;

            stackedVerticles.Push(sortedPolygon[0]);
            stackedVerticles.Push(sortedPolygon[1]);

            for (int i = 2; i < sortedPolygon.Count - 1; i++)
            {
                Console.Write("<" + sortedPolygon[i] + ">  \tStack[" + i + "] ");
                stackedVerticles.ToList().ForEach(vert => Console.Write(vert));
                Console.WriteLine();

                if (!IsOnTheSameChain(polygon, sortedPolygon[i], stackedVerticles.Peek()))
                {
                    upperLine = !upperLine;
                    var peekedVerticle = stackedVerticles.Peek();

                    for (int j = 1; j <= stackedVerticles.Count; j++)
                    {
                        var popedVerticle = stackedVerticles.Pop();
                        Console.WriteLine(sortedPolygon[i] + " <-A-> " + popedVerticle);
                    }
                    stackedVerticles.Clear();


                    stackedVerticles.Push(peekedVerticle);
                    stackedVerticles.Push(sortedPolygon[i]);
                    // Console.Write(sortedPolygon[i]);
                }
                else
                {
                    var popedVerticle = stackedVerticles.Pop();

                    while (stackedVerticles.Count > 0 && IsLineSegmentInsidePolygon(polygon, sortedPolygon[i], stackedVerticles.Peek(), popedVerticle))
                    {
                        popedVerticle = stackedVerticles.Pop();

                        Console.WriteLine(sortedPolygon[i] + " <-B-> " + popedVerticle);

                    }
                    /*
                    for (int j = 0; j < stackedVerticles.Count; j++)
                    {
                        Console.WriteLine(sortedPolygon[i] + " <-B-> " + popedVerticle);
                    }
                    */
                    stackedVerticles.Push(popedVerticle);
                    stackedVerticles.Push(sortedPolygon[i]);
                }
            }
        }
        private void CheckPoint(Point straightA, Point straightB, Point testPoint)
        {
            int placement = PointPlacement(straightA, straightB, testPoint);
            if (placement == 0)
                Console.WriteLine(testPoint + " On Straight");
            if (placement > 0)
                Console.WriteLine(testPoint + " On left");
            if (placement < 0)
                Console.WriteLine(testPoint + " On right");

        }
        private int PointPlacement(Point straightA, Point straightB, Point testPoint)
        {
            if (((testPoint.y - straightA.y) * (straightB.x - straightA.x) - (straightB.y - straightA.y) * (testPoint.x - straightA.x)) == 0)
                return 0;
            else if (((testPoint.y - straightA.y) * (straightB.x - straightA.x) - (straightB.y - straightA.y) * (testPoint.x - straightA.x)) > 0)
                return 1;
            else
                return -1;
        }

        private int PointOrientation(Point p, Point q, Point r)
        {
            int det = (p.x * p.y) + (p.y * r.x) + (q.x * r.y) - ((q.y * r.x) + (p.x * r.y) + (p.y * q.x));

            if (det < 0)
                return -1;
            if (det > 0)
                return 1;
            return 0;
        }
        private bool IsLineSegmentInsidePolygon(List<Point> polygon, Point checkedPoint, Point previousPoint, Point prePreviousPoint)
        {
            var orientation = PointOrientation(previousPoint, prePreviousPoint, checkedPoint);
            if (upperLine)
            {
                if (orientation == 0 || orientation > 0)
                    return false;
            }
            else
            {
                if (orientation == 0 || orientation < 0)
                    return false;
            }


            return true;
        }

        private bool AreSegmentsHardInterset(Point Segment1A, Point Segment1B, Point Segment2A, Point Segment2B)
        {
            int orientation1 = PointOrientation(Segment1A, Segment1B, Segment2A);
            int orientation2 = PointOrientation(Segment1A, Segment1B, Segment2B);
            int orientation3 = PointOrientation(Segment2A, Segment2B, Segment1A);
            int orientation4 = PointOrientation(Segment2A, Segment2B, Segment1B);

            if (orientation1 != orientation2 && orientation3 != orientation4)
                return true;


            return false;
        }

        private bool IsPointOnSection(Point p, Point q, Point test)
        {
            if (!((test.x >= p.x && test.x <= q.x) || (test.x >= q.x && test.x <= p.x)))
                return false;
            if (!((test.y >= p.y && test.y <= q.y) || (test.y >= q.y && test.y <= p.y)))
                return false;

            return true;
        }

        private bool IsOnTheSameChain(List<Point> polygon, Point point1, Point point2)
        {
            if ((PreviousPoint(polygon, point1) == point2) || ((PreviousPoint(polygon, point2) == point1)))
                return true;
            else
                return false;
        }

        private Point PreviousPoint(List<Point> polygon, Point point)
        {
            int index = polygon.IndexOf(point);
            if (index > 0)
                return polygon[index - 1];
            else
                return polygon.Last();
        }

        public List<Point> GenerateMonotonePolygon()
        {
            return new List<Point>{
                new Point(1,3),
                new Point(2,1),
                new Point(4,1),
                new Point(5,2),
                new Point(13,0),
                new Point(14,3),
                new Point(12,5),
                new Point(11,4),
                new Point(10,7),
                new Point(9,4),
                new Point(7,3),
                new Point(3,5)

            };
            /*
            return new List <Point>{
                new Point(2,2),
                new Point(4,1),
                new Point(6,3),
                new Point(10,5),
                new Point(12,8),
                new Point(7,9),
                new Point(3,6)
            };
            */
            /*
            return new List<Point>{
                new Point(2,2),
                new Point(6,5),
                new Point(8,3),
                new Point(10,6),
                new Point(11,2),
                new Point(13,2),
                new Point(12,8),
                new Point(4,8)
            };
            */
        }
    }
}
