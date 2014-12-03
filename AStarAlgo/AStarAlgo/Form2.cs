using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AStar
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
           
        }

        private static List<Node> InitHexMap(out Node start, out Node end)
        {
            int[,] map = new int[,] 
            { {0,0,0,0,0,0,0,0,0},
              {0,1,1,1,1,0,1,0,0},
              {0,0,0,2,1,3,1,0,0},
              {0,0,0,0,1,1,1,0,0},
              {0,0,0,0,0,0,0,0,0}
            };

            List<Node> nodes = new List<Node>();
            start = new Node(0, 0);
            end = new Node(0, 0);
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    Node n = new HexNode(c - 4, r - 2);
                    if (map[r, c] == 1) n.IsWall = true;
                    if (map[r, c] == 2) start = n;
                    if (map[r, c] == 3) end = n;

                    n.AddTo(nodes);
                    n.Nodes = nodes;
                }
            }

            return nodes;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            Node start, end;
            List<Node> nodes = InitHexMap(out start, out end);

            AStarAlgo aStar = new AStarAlgo(nodes);

            List<Node> pathNodes = aStar.FindPath(start, end);

            this.hexMap1.linePen = new Pen(Color.White, 2f);
            this.hexMap1.Map = nodes;
            this.hexMap1.Start = start;
            this.hexMap1.End = end;
            this.hexMap1.PathToEnd = pathNodes;
            
        }

    }
}
