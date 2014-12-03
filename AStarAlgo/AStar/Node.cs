using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AStar
{
    public class Node
    {

        public Node(int r, int c)
        {
            // TODO: Complete member initialization
            X = r;
            Y = c;
        }
        public int X { get; set; }
        public int Y { get; set; }

        public int F { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        

        public bool IsWall { get; set; }

        public Node Parent { get; set; }

        public string GetDirection()
        {
            string s = "";
            if (this.Parent != null)
            {
                if (Y < this.Parent.Y) s += "Up";
                if (Y > this.Parent.Y) s += "Down";

                if (X < this.Parent.X) s += "Left";
                if (X > this.Parent.X) s += "Right";

            }
            return s;
        }
        public List<Node> AdjacentNodes { get; set; }

        public override string ToString()
        {
            //return GetDirection();

            if (IsWall) return "1";
            return "0";
        }

        public string ToAddress()
        {
            return string.Format("[{0},{1}]", X, Y);
        }


        public void AddTo(List<Node> nodes)
        {
            if (!nodes.Contains(this))
                nodes.Add(this);                
        }

        internal void DistanceTo(Node end)
        {
            //this.G = 10;
            this.H = GetHeuristic(end);
            Debug.WriteLine(string.Format("   H={0}", this.H));
            this.F = this.G + this.H;
        }

        public virtual int GetHeuristic(Node end)
        {
            return (Math.Abs(end.X - this.X) + Math.Abs(end.Y - this.Y)) * 10;
        }

        public virtual List<Node> GetAdjacent()
        {
            List<Node> nodes = new List<Node>();
            Node node = this;

            Node lNode = GetNode(node.X - 1, node.Y);
            Node rNode = GetNode(node.X + 1, node.Y);
            Node tNode = GetNode(node.X, node.Y - 1);
            Node bNode = GetNode(node.X, node.Y + 1);


            Node tlNode = GetNode(node.X - 1, node.Y - 1);
            Node trNode = GetNode(node.X + 1, node.Y - 1);
            Node blNode = GetNode(node.X - 1, node.Y + 1);
            Node brNode = GetNode(node.X + 1, node.Y + 1);


            nodes = new List<Node>();
            AddIfExistAndSetG(nodes, lNode, 10);
            AddIfExistAndSetG(nodes, rNode, 10);
            AddIfExistAndSetG(nodes, tNode, 10);
            AddIfExistAndSetG(nodes, bNode, 10);

            AddNodeIfCanStep(nodes, lNode, tNode, tlNode);
            AddNodeIfCanStep(nodes, lNode, bNode, blNode);
            AddNodeIfCanStep(nodes, rNode, bNode, brNode);
            AddNodeIfCanStep(nodes, rNode, tNode, trNode);

            return nodes;
        }



        private void AddNodeIfCanStep(List<Node> nodes, Node adjNode1, Node adjNode2, Node nodeToAdd)
        {
            if (adjNode1 != null && adjNode2 != null && nodeToAdd != null)
                if (!adjNode1.IsWall && !adjNode2.IsWall)
                    AddIfExistAndSetG(nodes, nodeToAdd, 14);
        }


        protected void AddIfExistAndSetG(List<Node> nodes, Node lNode, int G)
        {
            if (lNode != null) { nodes.Add(lNode); lNode.G = G; }
        }

        protected Node GetNode(int x, int y)
        {
            var node = Nodes.Find(p => p.X == x && p.Y == y);
            return node;
        }


        public List<Node> Nodes { get; set; }

        public bool IsPath { get; set; }
    }
}
