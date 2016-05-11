using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapshAndGeometry.Algorithms
{
    class KCenter : IAlgorithm
    {
        public void Run()
        {
            var settlements = GetSettlements();
        }

        private List<Point> GetSettlements()
        {
            return new List<Point>()
            {
                new Point(1,1),
                new Point(1,7),
                new Point(3,2),
                new Point(3,5),
                new Point(5,7),
                new Point(6,3),
                new Point(7,5),
                new Point(7,2),
                new Point(10,1)
            };
        }
    }
}
