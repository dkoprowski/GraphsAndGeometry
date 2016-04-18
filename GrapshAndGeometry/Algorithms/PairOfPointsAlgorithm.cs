using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapshAndGeometry.Algorithms
{
    class PairOfPointsAlgorithm : IAlgorithm
    {
        public void Run()
        {
            Console.WriteLine("\nClosest Pair of Points (Divide and Conquer)\n");

            RunTests(25);

            Console.WriteLine("\npoints\t|\ttime\t|\ttime/nlogn");
            RunWithTime(10000);
            RunWithTime(20000);
            RunWithTime(40000);
            RunWithTime(60000);
            RunWithTime(80000);
            RunWithTime(100000);

            MeasureAverageTime(20, 10000);
        }
        private void RunWithTime(int elements)
        {
            TimeSpan time = RunAlgorithm(elements);
            double check = time.TotalMilliseconds / (elements * Math.Log(elements, 2));

            Console.WriteLine(elements + "\t|\t" + time + "\t|\t" + check);

        }

        private void MeasureAverageTime(int testCount, int pointsCount)
        {
            Console.Write("\nAverange time for " + pointsCount + " points is: ");
            TimeSpan sum = new TimeSpan();
            for (int i = 0; i < testCount; i++)
            {
                sum += RunAlgorithm(pointsCount);
            }

            Console.WriteLine(sum.TotalSeconds / testCount + " sec");
        }
        private TimeSpan RunAlgorithm(int pointsCount)
        {
            var points = Point.GenerateRandomPoints(pointsCount, -200000, 200000);
            points.Sort();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            FindTheSolutionRecursively(points);
            sw.Stop();
                    
            return sw.Elapsed;
        }
        private void RunTests(int count)
        {
            Console.Write("Starting [" + count + "] tests... ");
            for (int i = 0; i < count; i++)
            {
                var points = Point.GenerateRandomPoints(100, -100, 100);
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
    }
}
