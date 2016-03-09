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
            Console.WriteLine("Type [1] for Test");
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
                    var test1 = new TestAlg();
                    test1.Run();
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
