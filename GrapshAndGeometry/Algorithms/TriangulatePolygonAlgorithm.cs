using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapshAndGeometry.Algorithms
{
    class TriangulatePolygonAlgorithm : IAlgorithm
    {
        private Stack<Point> stackedVerticles;
        public void Run()
        {
            SweepTriangulation();
        }

        private void SweepTriangulation()
        {
            stackedVerticles = new Stack<Point>();
            List<Point> polygon = GenerateMonotonePolygon();
            var sortedPolygon = new List<Point>(polygon);
            sortedPolygon.Sort();

            stackedVerticles.Push(sortedPolygon[0]);
            stackedVerticles.Push(sortedPolygon[1]);

            for (int i = 2; i < sortedPolygon.Count; i++)
            {

                if (!IsOnTheSameChain(polygon, sortedPolygon[i], stackedVerticles.Peek()))
                {
                    var peekedVerticle = stackedVerticles.Peek();

                    for (int j = 1; j < stackedVerticles.Count; j++)
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
                    for (int j = 0; j < stackedVerticles.Count; j++)
                    {
                        if(!IsLineSegmentInsidePolygon(polygon, sortedPolygon[i], stackedVerticles.Peek()))
                        {
                            popedVerticle = stackedVerticles.Pop();
                            Console.WriteLine(sortedPolygon[i] + " <-B-> " + popedVerticle);
                        }
                    }
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
        private bool IsLineSegmentInsidePolygon(List<Point> polygon, Point LineSegmentA, Point LineSegmentB)
        {
            List<Point> pointsOnLeft = new List<Point>();
            List<Point> pointsOnRight = new List<Point>();
            for (int i = 0; i < polygon.Count; i++)
            {
                if ((PointPlacement(LineSegmentA, LineSegmentB, polygon[i]) < 0) && (PointPlacement(LineSegmentA, LineSegmentB, PreviousPoint(polygon, polygon[i])) > 0))
                {
                    return false;
                }
                else if ((PointPlacement(LineSegmentA, LineSegmentB, polygon[i]) > 0) && (PointPlacement(LineSegmentA, LineSegmentB, PreviousPoint(polygon, polygon[i])) < 0))
                {
                    return false;
                }

                if (PointPlacement(LineSegmentA, LineSegmentB, polygon[i]) > 0)
                {
                    pointsOnRight.Add(polygon[i]);
                }
                else if (PointPlacement(LineSegmentA, LineSegmentB, polygon[i]) < 0)
                {
                    pointsOnLeft.Add(polygon[i]);
                }

                if (pointsOnLeft.Count == 0 || pointsOnRight.Count == 0)
                    return false;

                if (IsOnTheSameChain(polygon, PreviousPoint(polygon, polygon[i]), polygon[i]))
                    return false;
            }

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
            return new List <Point>{
                new Point(2,2),
                new Point(4,1),
                new Point(6,3),
                new Point(10,5),
                new Point(12,8),
                new Point(7,9),
                new Point(3,6)
            };
        }
    }
}
