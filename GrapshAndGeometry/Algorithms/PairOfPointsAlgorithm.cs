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
            var points = LoadPoints();
            points.Sort();
            Console.WriteLine( FindTheSolutionRecursively(points));
        }

        private float FindTheSolutionRecursively(List<Point> points)
        {
            if(points.Count <= 3)
            {
                return closestPairOfPoints(points);
            }

            var leftPointsSet = points.GetRange(0, points.Count / 2);
            var rightPointsSet = points.GetRange((points.Count / 2), points.Count - leftPointsSet.Count);

            leftPointsSet.ForEach(point => Console.WriteLine(point.ToString()));
            rightPointsSet.ForEach(point => Console.WriteLine(point.ToString()));

            float leftMinDistance = FindTheSolutionRecursively(leftPointsSet);
            float rightMinDistance = FindTheSolutionRecursively(rightPointsSet);

            float minDistance = Math.Min(leftMinDistance, rightMinDistance);

            int dividingStraight = leftPointsSet[leftPointsSet.Count - 1].x;

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
        private List<Point> LoadPoints()
        {
            return new List<Point>()
            {
                new Point(-3,0),
                new Point(-2,-2),
                new Point(3,3),
                new Point(4,2),
                new Point(5,5),
                new Point(-1,-4),
                new Point(0,-1),
                new Point(1,1),
                new Point(2,-3),
                new Point(6,4)
            };
        }
    }
}
