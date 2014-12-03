using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Diagnostics;

namespace AStar
{
    public partial class HexMap : UserControl
    {
        public HexMap()
        {
            Map = new List<Node>();
            InitializeComponent();
            
        }

        public List<Node> Map { get; set; }
        public List<Node> PathToEnd { get; set; }
        public List<Unit> Units { get; set; }
        public Node Start { get; set; }
        public Node End { get; set; }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);            
            if (Map != null && Map.Count > 0)
            {
                DrawNodes(Map, e);
            }
            if (PathToEnd != null && PathToEnd.Count > 0)
            {
                PathToEnd.Remove(Start);
                PathToEnd.Remove(End);
                DrawNodes(PathToEnd, e, Brushes.Green);
            }

            if (!string.IsNullOrEmpty(MouseCap))
            {
                e.Graphics.DrawString(MouseCap, DefaultFont, Brushes.Black, 100, 100);
                Debug.WriteLine(MouseCap);
            }

            if (Units != null && Units.Count > 0)
            {
                DrawUnits(Units,e);
            }
            
        }

        private void DrawUnits(List<Unit> Units, PaintEventArgs e)
        {
            foreach (var unit in Units)
            {
                DrawUnit(e, unit);
            }
        }

        private void DrawUnit(PaintEventArgs e, Unit unit)
        {
            throw new NotImplementedException();
        }

        private void DrawNodes(List<Node> nodes, PaintEventArgs e, Brush brush)
        {
            foreach (var node in nodes)
            {
                DrawNode(e, node,brush);
            }    
        }

        private void DrawNodes(List<Node> nodes, PaintEventArgs e)
        {
            foreach (var node in nodes)
            {
                DrawNode(e, node);
            }            
        }

        

        private void DrawNode(PaintEventArgs e, Node node)
        {
            if (node != null)
            {
                float side = 30;
                float XOFFSET = 6;
                float YOFFSET = 3;
                float r = CalculateR(side);
                float h = CalculateH(side);
                float x = (node.X + XOFFSET)* r * 2 + (node.Y * r);
                float y = (node.Y + YOFFSET)* r * 1.75f; 

                var points = new System.Drawing.PointF[6];
                points[0] = new PointF(x, y);
                points[1] = new PointF(x + r, y + h);
                points[2] = new PointF(x + r, y + side + h);
                points[3] = new PointF(x, y + side + h + h);
                points[4] = new PointF(x - r, y + side + h);
                points[5] = new PointF(x - r, y + h);

                float cx = points[0].X;
                float cy = points[0].Y + r + 5;

                if (node.IsWall)
                    e.Graphics.FillPolygon(Brushes.Blue, points);
                if (node == Start)
                {
                    e.Graphics.FillPolygon(Brushes.Yellow, points);
                   
                    DrawStringAtPoint(e,"S", cx, cy);
                }
                if (node == End)
                { 
                    e.Graphics.FillPolygon(Brushes.Red, points);
                    DrawStringAtPoint(e,"E", cx, cy);
                }

                e.Graphics.DrawPolygon(linePen, points);
            }
        }

        private static void DrawStringAtPoint(PaintEventArgs e,string s, float x, float y)
        {
            e.Graphics.DrawString(s, new Font("Tahoma", 15, GraphicsUnit.Point), Brushes.White, new PointF(x, y), new StringFormat() { LineAlignment = StringAlignment.Center, Alignment = StringAlignment.Center });
        }

        private void DrawNode(PaintEventArgs e, Node node, Brush brush)
        {
            if (node != null)
            {
                float side = 30;
                float XOFFSET = 6;
                float YOFFSET = 3;
                float r = CalculateR(side);
                float h = CalculateH(side);
                float x = (node.X + XOFFSET) * r * 2 + (node.Y * r);
                float y = (node.Y + YOFFSET) * r * 1.75f;

                var points = new System.Drawing.PointF[6];
                points[0] = new PointF(x, y);
                points[1] = new PointF(x + r, y + h);
                points[2] = new PointF(x + r, y + side + h);
                points[3] = new PointF(x, y + side + h + h);
                points[4] = new PointF(x - r, y + side + h);
                points[5] = new PointF(x - r, y + h);

                e.Graphics.FillPolygon(brush, points);

                e.Graphics.DrawPolygon(linePen, points);
            }
        }

        public static float CalculateH(float side)
        {
            return (float)(Math.Sin(DegreesToRadians(30)) * side);
        }

        public static float CalculateR(float side)
        {
            return (float)(Math.Cos(DegreesToRadians(30)) * side);
        }
        public static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            DrawBG(e);
        }

        private void DrawBG(PaintEventArgs e)
        {
            e.Graphics.FillRectangles(Brushes.Gray,new Rectangle[] { e.ClipRectangle } );
        }

        public Pen linePen { get; set; }

        private void HexMap_Load(object sender, EventArgs e)
        {

        }

        private void HexMap_MouseMove(object sender, MouseEventArgs e)
        {
            MouseCap = string.Format("{0},{1}", e.X, e.Y);
            this.DoubleBuffered = true;
            this.Invalidate();
        }

        public string MouseCap { get; set; }
    }
}
