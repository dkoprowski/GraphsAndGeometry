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
            FurthestFirst(settlements, 3);


        }

        private void FurthestFirst(Dictionary<Point, float> settlements, int centresCount)
        {
            var hospitals = new Dictionary<Point, float>();
            hospitals.Add(settlements.First().Key, settlements.First().Value);

            for (int i = 1; i < centresCount; i++)
            {
                var FarestSettlement = UpdateDistanceToHospital(settlements, hospitals.Last().Key);
                hospitals.Add(FarestSettlement.Key, FarestSettlement.Value);
            }

            foreach (var item in hospitals)
            {
                Console.WriteLine(item.Key);
            }
        }

        private KeyValuePair<Point, float> UpdateDistanceToHospital(Dictionary<Point, float> settlements, Point hospital)
        {
            float longestDistance = float.NegativeInfinity;
            Point farestSettlement = null;
            foreach (var item in settlements.Keys.ToList())
            {
                float distance = hospital.DistanceTo(item);

                if (settlements[item] < distance)
                {
                    settlements[item] = distance;                   
                }

                if (settlements[item] > longestDistance)
                {
                    farestSettlement = item;
                    longestDistance = settlements[item];
                }
            }

            return new KeyValuePair<Point, float>(farestSettlement, longestDistance);
        }

        private Dictionary<Point, float> GetSettlements()
        {
            return new Dictionary<Point, float>()
            {
                {new Point(1,1), float.NegativeInfinity},
                {new Point(1,7), float.NegativeInfinity},
                {new Point(3,2), float.NegativeInfinity},
                {new Point(3,5), float.NegativeInfinity},
                {new Point(5,7), float.NegativeInfinity},
                {new Point(6,3), float.NegativeInfinity},
                {new Point(7,5), float.NegativeInfinity},
                {new Point(7,2), float.NegativeInfinity},
                {new Point(10,1), float.NegativeInfinity}
            };
        }
    }
}
