using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace AStar
{
	public class AStarAlgo
	{
		public List<Node> Nodes { get; set; }
		public Node[] TempNodes { get; set; }
		public AStarAlgo(List<Node> nodes)
		{
			Nodes = nodes;

			TempNodes = Nodes.ToArray();
		}

        public List<Node> FindHexPath(Node start, Node end)
        {
            return FindPath(start, end, true);
        }


		public List<Node> FindPath(Node start, Node end,bool HexMode = false)
		{
            List<Node> nodes = new List<Node>();
			List<Node> open = new List<Node>();
			List<Node> closed = new List<Node>();

			//Start 
			bool pathFound = false;
			Node currentNode = start;
			//Add Current Node to Open
			currentNode.AddTo(open);
			//Process Open List
			while(open.Count > 0)
			{
				//Get lowest F node
				Node nodeToProcess = open.OrderBy(p => p.F).FirstOrDefault();
				if (nodeToProcess == null){
					pathFound = false;
				}
				if (nodeToProcess == end)
				{
					pathFound = true;                  
				}

				if (pathFound)
				{
					break;
				}
				//Switch to closed
                pathFound = ProcessNode(end, open, closed, currentNode, nodeToProcess, HexMode);

				if (pathFound)
				{
					break;
				}
			}

			if (pathFound)
			{
				nodes = new List<Node>();
				nodes.Add(end);
				GetPath(nodes, end);               
				return nodes;
			}
			return nodes;
		}

		private List<Node> GetPath(List<Node> nodes,Node end)
		{
			
			if (end.Parent != null)
			{
				nodes.Add(end.Parent);
				GetPath(nodes, end.Parent); 
			}

			return nodes;
		}

		private bool ProcessNode(Node end, List<Node> open, List<Node> closed, Node currentNode, Node nodeToProcess,bool HexMode = false)
		{
			Debug.WriteLine(string.Format("Processing Node: {0}", nodeToProcess.ToAddress()));
			nodeToProcess.AddTo(closed);
			open.RemoveAll(p => p.X == nodeToProcess.X && p.Y == nodeToProcess.Y);
			//Get Adjacents
            
            List<Node> adjacentNodes = new List<Node>();
            //if (HexMode) adjacentNodes  = GetHexAdjacent(nodeToProcess);
            //if (!HexMode) adjacentNodes = GetAllAdjacent(nodeToProcess);\
            adjacentNodes = nodeToProcess.GetAdjacent();
			Debug.WriteLine(string.Format(" Adjacent Nodes:{0}",adjacentNodes.Count));
			foreach (var node in adjacentNodes)
			{
				if (node.IsWall || closed.Contains(node))
				{
					//Do Nothing

				}
				else if (node == end)
				{
					node.Parent = nodeToProcess;
					return true;
				}
				else if (open.Contains(node))
				{                    
					if(node.F < nodeToProcess.F)
                        ProcessNode(end, open, closed, node, node, HexMode);
				}
				else
				{
					node.AddTo(open);
					node.Parent = nodeToProcess;
					node.DistanceTo(end);
				}
			}

			return false;
		}

		private List<Node> GetAdjacent(Node node)
		{
			List<Node> nodes = new List<Node>();

			Node lNode = GetNode(node.X - 1, node.Y);
			Node rNode = GetNode(node.X + 1, node.Y);
			Node tNode = GetNode(node.X, node.Y-1);
			Node bNode = GetNode(node.X, node.Y+1);

			nodes = new List<Node>();
			AddIfExistAndSetG(nodes, lNode,10);
			AddIfExistAndSetG(nodes, rNode, 10);
			AddIfExistAndSetG(nodes, tNode, 10);
			AddIfExistAndSetG(nodes, bNode, 10);

			return nodes;
		}

        private List<Node> GetAllAdjacent(Node node)
        {
            List<Node> nodes = new List<Node>();

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

        private List<Node> GetHexAdjacent(Node node)
        {
            List<Node> nodes = new List<Node>();

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

        private static void AddNodeIfCanStep(List<Node> nodes, Node adjNode1, Node adjNode2, Node nodeToAdd)
        {
           if(adjNode1 != null && adjNode2 != null && nodeToAdd != null) 
               if (!adjNode1.IsWall && !adjNode2.IsWall) 
                   AddIfExistAndSetG(nodes, nodeToAdd, 14);
        }


		private static void AddIfExistAndSetG(List<Node> nodes, Node lNode, int G)
		{
			if (lNode != null) { nodes.Add(lNode); lNode.G = G; }
		}

		private Node GetNode(int x, int y)
		{
			var node = Nodes.Find(p => p.X == x && p.Y == y);
			return node;
		}

		public string Print()
		{
			return Print(Nodes);
		}

        public string Print(List<Node> pathNodes)
		{
			int r = 0;
			string s = "";
			string sep = " ";
			foreach (var node in pathNodes)
			{
				if (node.Y != r)
				{
					r = node.Y;
					s += Environment.NewLine;
				}
				s += node.ToString();
				s += sep;
			}
			return s;
		}

        public string PrintPath(List<Node> pathNodes)
		{
			pathNodes.Reverse();
			return string.Join(" ", pathNodes.Select(p => p.GetDirection()));
		}

        public string PrintAddreses(List<Node> pathNodes)
        {
            
            return string.Join(" ", pathNodes.Select(p => p.ToAddress()));
        }

        
    }

	


}
