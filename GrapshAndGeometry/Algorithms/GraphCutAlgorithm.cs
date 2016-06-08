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
        private Random random = new Random();
        public void Run()
        {
            edges = new List<Point>();
            random = new Random();
            graph = GetGraph();
            PrintGraph();
            PrintEdges();
            RandomMinCut();

            /*
            MergePoints(0, 2);
            PrintGraph();
            MergePoints(1, 3);
            */
            PrintGraph();
            PrintEdges();
        }
        private void RandomMinCut()
        {
            while(verticlesCount() > 2)
            {
                var randEdge = randomEdge();
                MergePoints(randEdge.x, randEdge.y);
            }
        }
        private void PrintGraph()
        {
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    Console.Write(graph[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
        private Point randomEdge()
        {
            var currEdges = new List<Point>();
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (graph[i, j] > 0)
                        currEdges.Add(new Point(i, j));
                }
            }

            return currEdges[random.Next(currEdges.Count)];
        }
        private int verticlesCount()
        {
            int[] verts = new int[graph.GetLength(0)];
            int count =0;
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if (graph[i, j] > 0)
                    {
                        verts[i]++;
                    }
                }
            }
            for (int i = 0; i < verts.Length; i++)
            {
                if (verts[i] > 0)
                    count++;
            }

            return count;
        }
        private void PrintEdges()
        {
            Dictionary<Point, int> edgesToPrint = new Dictionary<Point, int>();
            for (int i = 0; i < graph.GetLength(0); i++)
            {
                for (int j = 0; j < graph.GetLength(1); j++)
                {
                    if(graph[i, j] > 0)
                    {
                        if(!edgesToPrint.ContainsKey(new Point(j, i)))
                            edgesToPrint.Add(new Point(i, j), graph[i, j]);
                    }
                }
            }
            Console.WriteLine();
            foreach (var item in edgesToPrint)
            {
                Console.WriteLine(item.Key.x + " --(" + item.Value + ")-- " + item.Key.y);
            }            
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
          //  edges.Add(new Point(0, 2));
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
