using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GrapshAndGeometry.Algorithms;

namespace GrapshAndGeometry
{
    class Program
    {
        static void Main(string[] args)
        {
            WriteOptions();            
        }

        static void WriteOptions()
        {
            Console.WriteLine("\nChoose an algorithm:");
            Console.WriteLine("Type [1] for Closest Pair of Points (Divide and Conquer)");
            Console.WriteLine("Type [exit] to finish program");

            GetResponse();
        }

        static void GetResponse() 
        {
            string choosedOption = Console.ReadLine();
            Console.WriteLine("-----------------------------------");

            switch (choosedOption)
            {
                case "1":
                    IAlgorithm pairOfPoints = new PairOfPointsAlgorithm();
                    pairOfPoints.Run();
                    break;
                default:
                    Console.WriteLine("I don't understand. Try something from list below: ");
                    break;
                case "exit":
                    return;
            }
            Console.WriteLine("-----------------------------------");
            WriteOptions();
        }
    }
}
