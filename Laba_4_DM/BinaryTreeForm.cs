using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laba_4_DM
{
    public partial class BinaryTreeForm : Form
    {
        public string Expression { get; set; }
        public BinaryTreeForm()
        {
            InitializeComponent();
        }

        private void BinaryTreeForm_Paint(object sender, PaintEventArgs e)
        {
            //Graphics g = e.Graphics;
            //Font drawFont = new Font("Arial", 16);
            //SolidBrush drawBrush = new SolidBrush(Color.Black);

            //PointF drawPoint = new PointF(150.0F, 150.0F);

            //PointF drawPoint2 = new PointF(300.0F, 150.0F);
            //int index1 = Expression.LastIndexOf('(') + 1;
            //int index2 = Expression.LastIndexOf(')');

            //string firstPart = Expression.Substring(Expression.IndexOf('(')+1, Expression.IndexOf(')')- Expression.IndexOf('(')-1);
            //string secondPart = Expression.Substring(index1, index2 - index1);
            //g.DrawString(firstPart, drawFont, drawBrush, drawPoint);
            //g.DrawString(secondPart, drawFont, drawBrush, drawPoint2);

            Graphics g = e.Graphics;
            Font drawFont = new Font("Arial", 16);
            SolidBrush drawBrush = new SolidBrush(Color.Black);

            PointF startPoint = new PointF(408.0F, 50.0F);
            PointF curPoint = new PointF(408.0F, 50.0F);
            g.DrawString(Expression[0].ToString(), drawFont, drawBrush, startPoint);

            for(int i=1; i < Expression.Length; i++)
            {
                if (Expression[i - 1] == '(')
                {
                    startPoint.X = 408;
                    startPoint.Y = 50;
                    if (i-1==Expression.IndexOf('('))
                    {
                        startPoint.X -= 200;
                        startPoint.Y += 40;
                        g.DrawString(Expression[i].ToString(), drawFont, drawBrush, startPoint);
                        curPoint.X = startPoint.X;
                        curPoint.Y = startPoint.Y;

                    }
                    if (i - 1 == Expression.LastIndexOf('('))
                    {
                        startPoint.X += 200;
                        startPoint.Y += 40;
                        g.DrawString(Expression[i].ToString(), drawFont, drawBrush, startPoint);
                        curPoint.X = startPoint.X;
                        curPoint.Y = startPoint.Y;
                    }
                }
                if (Char.IsDigit(Expression[i]))
                {

                    if ("+-*/^".Contains(Expression[i-1]))
                    {
                        curPoint.Y += 40;
                        g.DrawString(Expression[i].ToString(), drawFont, drawBrush, curPoint);
                    }
                    else
                    {
                        curPoint.X += 100;
                        g.DrawString(Expression[i].ToString(), drawFont, drawBrush, curPoint);
                    }
                }
                if ("+-*/^".Contains(Expression[i]) && i - 1 != Expression.IndexOf('(') && i - 1 != Expression.LastIndexOf('('))
                {

                    if (!Char.IsDigit(Expression[i - 1]))
                    {
                        curPoint.Y += 40;
                        g.DrawString(Expression[i].ToString(), drawFont, drawBrush, curPoint);
                    }
                    else
                    {
                        curPoint.X += 100;

                        g.DrawString(Expression[i].ToString(), drawFont, drawBrush, curPoint);
                    }
                }
            }
        }
    }
}
