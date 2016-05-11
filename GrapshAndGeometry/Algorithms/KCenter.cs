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
            var linearSettlements = GetSettlementsLinear();
            FurthestFirst(settlements, 3);
            //FurthestFirst(linearSettlements, 5);


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
            UpdateDistanceToHospital(settlements, hospitals.Last().Key);

            Console.WriteLine("\n Hospitals:");
            foreach (var item in hospitals)
            {
                Console.WriteLine(item.Key);
            }

            Console.WriteLine("\n Distances:");
            foreach (var item in settlements)
            {
                Console.WriteLine(item.Key + ":\t" + item.Value);

            }
        }

        private KeyValuePair<Point, float> UpdateDistanceToHospital(Dictionary<Point, float> settlements, Point hospital)
        {
            float longestDistance = float.NegativeInfinity;
            Point farestSettlement = null;
            Console.WriteLine("-- hospital "+hospital+" --");

            foreach (var item in settlements.Keys.ToList())
            {
                float distance = hospital.DistanceTo(item);

                if (settlements[item] > distance)
                {
                    settlements[item] = distance;
                    Console.WriteLine(item + ":\t" + distance);
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
                {new Point(1,1), float.PositiveInfinity},
                {new Point(1,7), float.PositiveInfinity},
                {new Point(3,2), float.PositiveInfinity},
                {new Point(3,5), float.PositiveInfinity},
                {new Point(5,7), float.PositiveInfinity},
                {new Point(6,3), float.PositiveInfinity},
                {new Point(7,5), float.PositiveInfinity},
                {new Point(7,2), float.PositiveInfinity},
                {new Point(10,1), float.PositiveInfinity}
            };
        }

        private Dictionary<Point, float> GetSettlementsLinear()
        {
            return new Dictionary<Point, float>()
            {
                {new Point(1,1), float.PositiveInfinity},
                {new Point(2,1), float.PositiveInfinity},
                {new Point(3,1), float.PositiveInfinity},
                {new Point(4,1), float.PositiveInfinity},
                {new Point(5,1), float.PositiveInfinity},
                {new Point(6,1), float.PositiveInfinity},
                {new Point(7,1), float.PositiveInfinity},
                {new Point(8,1), float.PositiveInfinity},
                {new Point(9,1), float.PositiveInfinity},
                {new Point(10,1), float.PositiveInfinity},

            };
        }
    }
}
