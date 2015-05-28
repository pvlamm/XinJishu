using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace XinJishu.Imaging
{

    public class ValuePillMaker
    {
        private Int32 height { get; set; }
        private Int32 width { get; set; }
        private Int32 val { get; set; }
        private Int32 position { get; set; }

        public ValuePillMaker(Int32 height, Int32 width, Int32 val, Int32 position)
        {
            this.height = height;
            this.width = width;
            this.val = val;
            this.position = position;
        }

        public void DrawPolygonWithValue()
        {
            Pen pen = new Pen(Color.Black, 1);
            Brush brush = new System.Drawing.SolidBrush(System.Drawing.Color.White);

            Int32 x1, x2, x3, x4, x5, x6;

            Int32 leftmost = this.position - ((height / 2) + (height / 4));
            Int32 rightmost = this.position + ((height / 2) + (height / 4));

            if (leftmost < 0)
                leftmost = -1 * leftmost;
            else
                leftmost = 0;

            if (rightmost > this.width)
                rightmost = this.width - rightmost;
            else
                rightmost = 0;

            x1 = leftmost + this.position - (height / 2) + rightmost;
            x2 = leftmost + this.position - ((height / 2) + (height / 4)) + rightmost;
            x3 = leftmost + this.position - (height / 2) + rightmost;

            x4 = leftmost + this.position + (height / 2) + rightmost;
            x5 = leftmost + this.position + ((height / 2) + (height / 4)) + rightmost;
            x6 = leftmost + this.position + (height / 2) + rightmost;


            Point p1 = new Point(x1, 0);
            Point p2 = new Point(x2, (height / 2));
            Point p3 = new Point(x3, height);

            Point p4 = new Point(x4, height);
            Point p5 = new Point(x5, (height / 2));
            Point p6 = new Point(x6, 0);

            Point[] points = {
                                 p1, p2, p3,
                                 p4, p5, p6
                             };

            Image img = new Bitmap(this.width, this.height);
            Graphics g = Graphics.FromImage(img);

            g.Clear(Color.Transparent);

            // Draw Polygon White Blob
            g.FillPolygon(brush, points, System.Drawing.Drawing2D.FillMode.Alternate);
            // Draw Black Outline
            g.DrawPolygon(pen, points);

            g.DrawString(this.val.ToString(),
                new Font("Arial Bold", Convert.ToSingle(this.height * 0.75)),
                new SolidBrush(Color.Black),
                // - ( (float)this.height / 3.0f)
                new PointF(leftmost + this.position - (height / 2) - 10 + rightmost, -5));



            g.Save();

            img.Save(Environment.CurrentDirectory + "\\" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".jpg");

        }

    }
}
