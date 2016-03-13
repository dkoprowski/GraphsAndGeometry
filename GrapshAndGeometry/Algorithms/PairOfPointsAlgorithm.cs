using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapshAndGeometry.Algorithms
{
    class Point : IComparable<Point>
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        int IComparable<Point>.CompareTo(Point other)
        {
            if (x < other.x)
            {
                return -1;
            }
            else if (x > other.x)
            {
                return 1;
            }
            else if (y < other.y)
            {
                return -1;
            }
            else if (y > other.y)
            {
                return 1;
            }
            else {
                return 0;
            }
        }

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }

        public float DistanceTo(Point other)
        {
            return (float)Math.Sqrt(Math.Pow((x-other.x),2)+ Math.Pow(y-other.y,2));
        }
    }
    class PairOfPointsAlgorithm : IAlgorithm
    {
        public void Run()
        {
            var points = LoadRandomPoints(10, -10, 10);
            points.Sort();
            RunTests(25);
            Console.WriteLine("Recursive: "+FindTheSolutionRecursively(points));
            Console.WriteLine("Brute: " + FindTheSolutionBrute(points));
        }

        private void RunTests(int count)
        {
            Console.Write("Starting [" + count + "] tests... ");
            for (int i = 0; i < count; i++)
            {
                var points = LoadRandomPoints(100, -100, 100);
                points.Sort();
                if(FindTheSolutionRecursively(points) != FindTheSolutionBrute(points))
                {
                    Console.WriteLine("FAILED!");
                    return;
                }
            }
            Console.WriteLine("PASSED!");
        }
        private float FindTheSolutionBrute(List<Point> points)
        {
            return closestPairOfPoints(points);
        }

        private float FindTheSolutionRecursively(List<Point> points)
        {
            if (points.Count <= 3)
            {
                return closestPairOfPoints(points);
            }
            var middle = points.Count / 2;
            var leftPointsSet = points.GetRange(0, middle);
            var rightPointsSet = points.GetRange(middle, points.Count - leftPointsSet.Count);

            float leftMinDistance = FindTheSolutionRecursively(leftPointsSet);
            float rightMinDistance = FindTheSolutionRecursively(rightPointsSet);

            float minDistance = Math.Min(leftMinDistance, rightMinDistance);

            int dividingStraight = (points[middle - 1].x + points[middle].x) / 2;

            float leftLimit = dividingStraight - minDistance;
            float rightLimit = dividingStraight + minDistance;

            List<Point> leftMiddleAreaPoints = new List<Point>();
            List<Point> rightMiddleAreaPoints = new List<Point>();

            for (int i = leftPointsSet.Count - 1; i >= 0 && leftPointsSet[i].x >= leftLimit; i--)
            {
                leftMiddleAreaPoints.Add(leftPointsSet[i]);
            }

            for (int i = 0; i < rightPointsSet.Count && rightPointsSet[i].x <= rightLimit; i++)
            {
                rightMiddleAreaPoints.Add(rightPointsSet[i]);
            }

            for (var i = 0; i < leftMiddleAreaPoints.Count; i++)
            {
                for (var j = 0; j < rightMiddleAreaPoints.Count; j++)
                {
                    minDistance = Math.Min(minDistance, leftMiddleAreaPoints[i].DistanceTo(rightMiddleAreaPoints[j]));
                }
            }

            return minDistance;
        }
        private float closestPairOfPoints(List<Point> points)
        {
            var minDistance = points[0].DistanceTo(points[1]);
            for (var i = 0; i < points.Count; ++i)
            {
                for (var j = i + 1; j < points.Count; ++j)
                {
                    minDistance = Math.Min(minDistance, points[i].DistanceTo(points[j]));
                }
            }
            return minDistance;
        }
        private List<Point> LoadRandomPoints(int count, int min, int max)
        {
            var points = new List<Point>();
            Random rand = new Random();
            for (int i = 0; i < count; i++)
            {
                points.Add(new Point(rand.Next(min, max), rand.Next(min, max)));
            }
            return points;
        }
    }
}
