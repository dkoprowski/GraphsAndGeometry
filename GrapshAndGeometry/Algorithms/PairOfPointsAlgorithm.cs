using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapshAndGeometry.Algorithms
{
    class Point
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    class PairOfPointsAlgorithm : IAlgorithm
    {
        public void Run()
        {
            var points = LoadPoints();

        }

        private List<Point> LoadPoints()
        {
            return new List<Point>()
            {
                new Point(-3,0),
                new Point(-2,-2),
                new Point(-1,-4),
                new Point(0,-1),
                new Point(1,1),
                new Point(2,-3),
                new Point(3,3),
                new Point(4,2),
                new Point(5,5),
                new Point(6,4)
            };
        }
    }
}
