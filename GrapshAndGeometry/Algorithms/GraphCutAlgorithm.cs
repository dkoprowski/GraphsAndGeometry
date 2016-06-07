using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrapshAndGeometry.Algorithms
{
    class GraphCutAlgorithm : IAlgorithm
    {
        private int[,] graph;
        private List<Point> edges = new List<Point>();
        public void Run()
        {
            graph = GetGraph();
            PrintGraph();
            RandomMinCut();
            /*
            MergePoints(0, 2);
            PrintGraph();
            MergePoints(1, 3);
            */
            PrintGraph();
        }
        private void RandomMinCut()
        {
            var random = new Random();
            while(edges.Count > 2)
            {
                var randomEdge = edges[random.Next(edges.Count)];
                MergePoints(randomEdge.x, randomEdge.y);
                edges.Remove(randomEdge);
            }
        }
        private void PrintGraph()
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    Console.Write(graph[i, j]);
                    if (graph[i, j] == 1)
                        edges.Add(new Point(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        private void MergePoints(int mergeToPoint, int mergeFromPoint)
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                if ((i != mergeToPoint) && (i != mergeFromPoint))
                {
                    graph[i, mergeToPoint] += graph[i, mergeFromPoint];
                    graph[i, mergeFromPoint] = 0;
                }
            }

            for (int i = 0; i < graph.GetLength(1); i++)
            {
                if ((i != mergeToPoint) && (i != mergeFromPoint))
                {
                    graph[mergeToPoint, i] += graph[mergeFromPoint, i];
                    graph[mergeFromPoint, i] = 0;
                }
            }
            graph[mergeFromPoint, mergeToPoint] = 0;
            graph[mergeToPoint, mergeFromPoint] = 0;
           
        }
        public int[,] GetGraph()
        {
            var graph = new int[5, 5];
            edges.Add(new Point(0, 2));
            edges.Add(new Point(0, 4));
            edges.Add(new Point(1, 2));
            edges.Add(new Point(1, 3));
            edges.Add(new Point(2, 3));
            edges.Add(new Point(2, 4));

            foreach (var edge in edges)
            {
                graph[edge.x, edge.y] = 1;
                graph[edge.y, edge.x] = 1;
            }
            return graph;
        }
    }
}
