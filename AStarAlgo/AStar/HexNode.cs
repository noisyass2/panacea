using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AStar
{
    public class HexNode : Node
    {
        public HexNode(int r, int c)
            : base(r,c)
        { }

        public override int GetHeuristic(Node end)
        {
            List<int> Ds =new List<int>(){ Math.Abs(end.X - this.X),Math.Abs(end.Y - this.Y),Math.Abs((end.X - this.X) - (end.Y - this.Y)) };
            return (Ds.Max()) * 10;            
        }

        public override List<Node> GetAdjacent()
        {
            List<Node> nodes = new List<Node>();
            Node node = this;

            Node lNode = GetNode(node.X - 1, node.Y);
            Node rNode = GetNode(node.X + 1, node.Y);
            Node tlNode = GetNode(node.X, node.Y - 1);
            Node trNode = GetNode(node.X + 1, node.Y - 1);
            Node blNode = GetNode(node.X - 1, node.Y + 1);
            Node brNode = GetNode(node.X, node.Y + 1);

            AddIfExistAndSetG(nodes, lNode, 10);
            AddIfExistAndSetG(nodes, rNode, 10);
            AddIfExistAndSetG(nodes, tlNode, 10);
            AddIfExistAndSetG(nodes, trNode, 10);
            AddIfExistAndSetG(nodes, blNode, 10);
            AddIfExistAndSetG(nodes, brNode, 10);

            return nodes;
        }

    }
}
