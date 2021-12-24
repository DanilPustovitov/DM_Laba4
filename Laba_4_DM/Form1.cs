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
    public partial class Form1 : Form
    {
        int[,] matrix = new int[5, 5];
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var list = groupBox1.Controls.OfType<NumericUpDown>();
            var array = list.ToArray();
            int index = 0;
            for(int i=0; i < 5; i++)
            {
                for (int j=0; j < 5; j++)
                {
                    matrix[i, j] = Convert.ToInt32(array[index++].Value);
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool s = true;
            int counter = 0;

            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (matrix[i, j] != matrix[j, i] && i!=j)
                    {
                        s = false;
                        counter++;
                    }

                }
            }
            label1.Text += (s==true) ? " симметричная" : " не симметричная";
            label2.Text += (s == true) ? " можеть быть как ориентированным,\n так и не ориентированным" : " ориентированный";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            PolishNotation form = new PolishNotation();
            form.Expression = textBox1.Text;
            form.Show();
        }
    }
}
