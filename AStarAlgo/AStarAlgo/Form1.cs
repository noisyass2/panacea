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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Node start, end;
            List<Node> nodes = InitMap(out start, out end);

            AStarAlgo aStar = new AStarAlgo(nodes);


            List<Node> pathNodes = aStar.FindPath(start, end);

            textBox1.Text = aStar.Print();
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("--------------");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(aStar.PrintPath(pathNodes));

            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("--------------");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(aStar.PrintAddreses(pathNodes));

        }

        private static List<Node> InitMap(out Node start, out Node end)
        {
            int[,] map = new int[,] 
            { {0,0,0,0,0,0,0,0,0},
              {0,0,0,0,1,1,1,1,0},
              {0,2,1,0,1,0,3,0,0},
              {0,1,1,0,1,0,0,0,0},
              {0,0,0,0,1,0,0,0,0}
            };

            List<Node> nodes = new List<Node>();
            start = new Node(0,0);
            end = new Node(0,0);
            for (int r = 0; r < 5; r++)
            {
                for (int c = 0; c < 9; c++)
                {
                    Node n = new Node(c,r);
                    
                    if (map[r, c] == 1) n.IsWall = true;
                    if (map[r, c] == 2) start = n;
                    if (map[r, c] == 3) end = n;
                    
                    n.AddTo(nodes);
                    n.Nodes = nodes;
                }
            }

            return nodes;
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
                    Node n = new HexNode(c-4, r-2);
                    if (map[r, c] == 1) n.IsWall = true;
                    if (map[r, c] == 2) start = n;
                    if (map[r, c] == 3) end = n;

                    n.AddTo(nodes);
                    n.Nodes = nodes;
                }
            }

            return nodes;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Node start, end;
            List<Node> nodes = InitHexMap(out start, out end);

            AStarAlgo aStar = new AStarAlgo(nodes);


            List<Node> pathNodes = aStar.FindHexPath(start, end);

            textBox1.Text = aStar.Print();
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("--------------");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(aStar.PrintPath(pathNodes));

            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText("--------------");
            textBox1.AppendText(Environment.NewLine);
            textBox1.AppendText(aStar.PrintAddreses(pathNodes));
        }
    }
}
