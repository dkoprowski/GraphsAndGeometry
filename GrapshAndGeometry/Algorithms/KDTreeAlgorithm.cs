using System;
using System.Collections.Generic;

namespace GrapshAndGeometry.Algorithms
{
    class Region
    {
        public float leftHorizontalBorder;
        public float rightHorizontalBorder;
        public float bottomVerticalBorder;
        public float topVerticalBorder;

        public bool Includes(Point point)
        {
            if (point.x >= leftHorizontalBorder &&
                point.x <= rightHorizontalBorder &&
                point.y >= bottomVerticalBorder &&
                point.y <= topVerticalBorder)
                return true;
            else
                return false;
        }
        public bool Includes(Region region)
        {
            if (region.leftHorizontalBorder >= leftHorizontalBorder &&
                region.rightHorizontalBorder <= rightHorizontalBorder &&
                region.bottomVerticalBorder >= bottomVerticalBorder &&
                region.topVerticalBorder <= topVerticalBorder)
                return true;
            else
                return false;
        }
        public bool Includes(List<Point> regionPoints)
        {
            foreach (var item in regionPoints)
            {
                if (!Includes(item))
                {
                    return false;
                }
            }
            return true;
        }
        public bool Intersect(List<Point> regionPoints)
        {
            foreach (var item in regionPoints)
            {
                if (Includes(item))
                {
                    return true;
                }
            }
            return false;
        }
        public Region()
        {

        }

        public Region(float positive, float negative)
        {
            leftHorizontalBorder = negative;
            rightHorizontalBorder = positive; ;
            bottomVerticalBorder = negative; ;
            topVerticalBorder = positive; ;
        }

        public Region(float lHorizont, float rHorizont, float bVertical, float tVertical)
        {
            leftHorizontalBorder = lHorizont;
            rightHorizontalBorder = rHorizont; ;
            bottomVerticalBorder = bVertical; ;
            topVerticalBorder = tVertical; ;
        }
        
        public override string ToString()
        {
            return "[" + leftHorizontalBorder + "," + rightHorizontalBorder + "|" + bottomVerticalBorder + "," + topVerticalBorder + "]";
        }
    }

    class KDNode
    {
        public float straightL;
        public int nodeNr;
        public int depth;
        public KDNode leftSon;
        public KDNode rightSon;
        public KDNode parent;
        public Region region;

        public bool leaf;
        public Point point;
    }
    class KDTreeAlgorithm : IAlgorithm
    {
        private int nodeNr = 0;
        public void Run()
        {
            var points = Point.GenerateRandomPoints(25, -500, 500);
            points.Sort();
            points.ForEach(point => Console.WriteLine(point.ToString()));

            var kdTree = BuildKdTree(points, 0, null, new Region(float.PositiveInfinity, float.NegativeInfinity));
            PrintKdTree(kdTree, "", "");

            Console.WriteLine();
            Console.WriteLine("Query for region x[-200:200] y[-200:200]");
            KdTreeQuery(kdTree, new Region(-200, 200));
            Console.WriteLine();
        }

        private KDNode BuildKdTree(List<Point> Set, int depth, KDNode parent, Region range)
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
            float l;
            if (depth % 2 == 0)
            {
                Set.Sort();
                median = Set.Count / 2;
                LeftSet = Set.GetRange(0, median);
                RightSet = Set.GetRange(median, Set.Count - LeftSet.Count);
                l = Set[median].x;
            }
            else
            {
                Set.Sort(Point.ComparePointsByY);
                median = Set.Count / 2;
                LeftSet = Set.GetRange(0, median);
                RightSet = Set.GetRange(median, Set.Count - LeftSet.Count);
                l = Set[median].y;
            }

            var node = new KDNode();
            node.straightL = l;
            node.parent = parent;
            node.region = range;
            node.depth = depth;
            if (depth % 2 == 0)
            {
                node.leftSon = BuildKdTree(
                    LeftSet, depth + 1, node, 
                    new Region(range.leftHorizontalBorder, node.straightL, range.bottomVerticalBorder, range.topVerticalBorder));
                node.rightSon = BuildKdTree(
                    RightSet, depth + 1, node,
                    new Region(node.straightL, range.rightHorizontalBorder, range.bottomVerticalBorder, range.topVerticalBorder));
            }
            else
            {
                node.leftSon = BuildKdTree(
                    LeftSet, depth + 1, node,
                    new Region(range.leftHorizontalBorder, range.rightHorizontalBorder, range.bottomVerticalBorder, node.straightL));
                node.rightSon = BuildKdTree(
                    RightSet, depth + 1, node,
                    new Region(range.leftHorizontalBorder, range.rightHorizontalBorder, node.straightL, range.topVerticalBorder));
            }

            node.nodeNr = nodeNr++;
            return node;
        }

        private void KdTreeQuery(KDNode node, Region region)
        {
            if (node.leaf)
            {
                if (region.Includes(node.point))
                {
                    ReportSubtree(node);
                }
            }
            else
            {
                if (region.Includes(GetLeafs(node.leftSon)))
                {
                    ReportSubtree(node.leftSon);
                }
                else if (region.Intersect(GetLeafs(node.leftSon)))
                {
                    KdTreeQuery(node.leftSon, region);
                }

                if (region.Includes(GetLeafs(node.rightSon)))
                {
                    ReportSubtree(node.rightSon);
                }
                else if (region.Intersect(GetLeafs(node.rightSon)))
                {
                    KdTreeQuery(node.rightSon, region);
                }
            }
        }
        private void ReportSubtree(KDNode node)
        {
            if (node.leaf)
                Console.WriteLine(node.point.ToString());
            else
            {
                ReportSubtree(node.leftSon);
                ReportSubtree(node.rightSon);
            }
        }

        private List<Point> GetLeafs(KDNode node)
        {
            if (node.leaf)
                return new List<Point>() { node.point };
            else
            {

                var leftLeafs = GetLeafs(node.leftSon);
                var rightLeafs = GetLeafs(node.rightSon);
                leftLeafs.AddRange(rightLeafs);
                return leftLeafs;
            }
        }
        private void PrintKdTree(KDNode node, string parent, string depth)
        {
            Console.WriteLine();
            if (node.leaf)
                Console.Write(depth + parent + node.point.ToString());
            else
            {
                Console.Write(depth + parent + "{n" + node.nodeNr+ "}"+node.region.ToString());


                PrintKdTree(node.leftSon, "<n" + node.nodeNr + ">R ", depth + "\t");
                PrintKdTree(node.rightSon, "<n" + node.nodeNr + ">L ", depth + "\t");
            }
        }
    }
}
