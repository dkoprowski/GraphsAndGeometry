using System;
using System.Collections.Generic;

namespace GrapshAndGeometry.Algorithms
{
    class QueryRegion
    {
        public int leftHorizontalBorder;
        public int rightHorizontalBorder;
        public int leftVerticalBorder;
        public int rightVerticalBorder;
    }

    class KDNode
    {
        public int straightL;
        public int nodeNr;
        public KDNode leftSon;
        public KDNode rightSon;

        public bool leaf;
        public Point point;
    }
    class KDTreeAlgorithm : IAlgorithm
    {
        private int nodeNr = 0;
        public void Run()
        {
            var points = Point.GenerateRandomPoints(25, -500, 500);

            var kdTree = BuildKdTree(points, 0);
            PrintKdTree(kdTree, "", "");
            Console.WriteLine();
        }

        private KDNode BuildKdTree(List<Point> Set, int depth)
        {
            if (Set.Count == 1)
                return new KDNode()
                {
                    leaf = true,
                    point = Set[0]
                };

            List<Point> LeftSet = new List<Point>();
            List<Point> RightSet = new List<Point>();
            int median;
            if (depth % 2 == 0)
            {
                Set.Sort();
                median = Set.Count / 2;
                LeftSet = Set.GetRange(0, median);
                RightSet = Set.GetRange(median, Set.Count - LeftSet.Count);
            }
            else
            {
                Set.Sort(Point.ComparePointsByY);
                median = Set.Count / 2;
                LeftSet = Set.GetRange(0, median);
                RightSet = Set.GetRange(median, Set.Count - LeftSet.Count);
            }

            var node = new KDNode();
            node.straightL = median;
            node.leftSon = BuildKdTree(LeftSet, depth + 1);
            node.rightSon = BuildKdTree(RightSet, depth + 1);
            node.nodeNr = nodeNr++;
            return node;
        }

        private void PrintKdTree(KDNode node, string parent, string depth)
        {
            Console.WriteLine();
            if (node.leaf)
                Console.Write(depth + parent + node.point.ToString());
            else
            {
                Console.Write(depth + parent + "{n" + node.nodeNr+ "}");


                PrintKdTree(node.leftSon, "<n" + node.nodeNr + ">R ", depth + "\t");
                PrintKdTree(node.rightSon, "<n" + node.nodeNr + ">L ", depth + "\t");
            }
        }
    }
}
