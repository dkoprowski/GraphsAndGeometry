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
            else
            {
                return 0;
            }
        }

        public override string ToString()
        {
            return "(" + x.ToString() + "," + y.ToString() + ")";
        }

        public float DistanceTo(Point other)
        {
            return (float)Math.Sqrt(Math.Pow((x - other.x), 2) + Math.Pow(y - other.y, 2));
        }

        public static List<Point> GenerateRandomPoints(int count, int min, int max)
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
