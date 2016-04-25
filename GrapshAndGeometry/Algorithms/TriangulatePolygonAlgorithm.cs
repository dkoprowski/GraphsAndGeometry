using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapshAndGeometry.Algorithms
{
    class TriangulatePolygonAlgorithm : IAlgorithm
    {
        public void Run()
        {

        }

        public List<Point> GenerateMonotonePolygon()
        {
            return new List <Point>{
                new Point(2,2),
                new Point(3,6),
                new Point(4,1),
                new Point(6,3),
                new Point(7,9),
                new Point(10,5),
                new Point(12,8)
            };
        }
    }
}
