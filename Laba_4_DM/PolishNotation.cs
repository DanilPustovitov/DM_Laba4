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
    public partial class PolishNotation : Form
    {
        public string Expression { get; set; }
        public PolishNotation()
        {
            InitializeComponent();
        }

        private void PolishNotation_Load(object sender, EventArgs e)
        {
            List<char> last =new List<char>();
            List<char> operands = new List<char>();
            List<char> operators = new List<char>();
            string result = "";
            foreach(var i in Expression)
            {
                if (i=='(')
                {
                    if (operators.Count == 1 && operands.Count == 0)
                    {
                        last.Add(operators.Last());
                    }
                    else
                    {
                        foreach (var j in operands)
                        {
                            result += j;
                        }
                        foreach (var j in operators)
                        {
                            result += j;
                        }
                    }
                    operands.Clear();
                    operators.Clear();                    
                }
                else if (i == ')')
                {
                    List<int> removable = new List<int>();
                    int counter = 0;
                    foreach (var j in operators)
                    {
                        
                        if (getPriority(j) == 1)
                        {
                            result += operands[counter];
                            counter += 1;
                        }
                        else if (getPriority(j)==2 || getPriority(j) == 3)
                        {
                            result += operands[counter];
                            result += operands[counter+1];
                            result += j;
                            counter += 2;
                            removable.Add(operators.IndexOf(j));
                        }
                    }
                    for(int j=0; j<removable.Count; j++)
                    {
                        operators.RemoveAt(removable[j]);
                    }
                    for(int j=counter; j < operands.Count; j++)
                    {
                        result += operands[j++];
                    }
                    foreach (var j in operators)
                    {
                        result += j;
                    }
                    operands.Clear();
                    operators.Clear();
                }
                else
                {
                    if (Char.IsDigit(i))
                    {
                        operands.Add(i);
                    }
                    else 
                    { 
                        switch(i)
                        {
                            case '+':
                                operators.Add(i);
                                break;
                            case '-':
                                operators.Add(i);
                                break;
                            case '*':
                                operators.Add(i);
                                break;
                            case '/':
                                operators.Add(i);
                                break;
                            case '^':
                                operators.Add(i);
                                break;
                        }
                    }
                }
            }
            foreach(var i in last)
            {
                result += i;
            }
            richTextBox1.Text = result;
            string postfix = result;


            List<char> first = new List<char>();
            operands.Clear();
            operators.Clear();
            result = "";
            foreach (var i in Expression)
            {
                if (i == '(')
                {
                    result += "(";
                    if (operators.Count == 1 && operands.Count == 0)
                    {
                        first.Add(operators.Last());
                    }
                    else
                    {
                        foreach (var j in operands)
                        {
                            result += j;
                        }
                        foreach (var j in operators)
                        {
                            result += j;
                        }
                    }
                    operands.Clear();
                    operators.Clear();
                }
                else if (i == ')')
                {
                    List<int> removable = new List<int>();
                    List<int> removable2 = new List<int>();
                    int counter = 0;
                    int needed = 0;
                    foreach(var j in operators)
                    {
                        if (counter > 1 && getPriority(j) == 1)
                        {
                            result = result.Insert(needed, j.ToString());
                        }
                        else
                        {
                            needed = result.Length;
                            result += j;
                        }
                        if (getPriority(j) == 1)
                        {
                            result += operands[counter];
                            removable.Add(counter);
                            counter += 1;
                        }
                        if (getPriority(j) == 2 || getPriority(j) == 3)
                        {
                            result += operands[counter];
                            result += operands[counter+1];
                            removable.Add(counter);
                            removable.Add(counter+1);
                            counter += 2;
                        }
                        
                    }

                    for (int j = removable.Count-1; j >=0; j--)
                    {
                        operands.RemoveAt(removable[j]);
                    }
                    foreach(var j in operands)
                    {
                        result += j;
                    }

                    operands.Clear();
                    operators.Clear();
                    result += ")";
                }
                else
                {
                    if (Char.IsDigit(i))
                    {
                        operands.Add(i);
                    }
                    else
                    {
                        switch (i)
                        {
                            case '+':
                                operators.Add(i);
                                break;
                            case '-':
                                operators.Add(i);
                                break;
                            case '*':
                                operators.Add(i);
                                break;
                            case '/':
                                operators.Add(i);
                                break;
                            case '^':
                                operators.Add(i);
                                break;
                        }
                    }
                }
            }
            foreach(var i in first)
            {
                result = result.Insert(0, i.ToString());
            }
            richTextBox2.Text = result;

            int size = postfix.Length;
            int a = 0, b=0;
            Stack<int> s = new Stack<int>();
            string stepbystep = "";

            for (var i = 0; i < size; i++)
            {
                if (Char.IsDigit(postfix[i]))
                {
                    a = (int)(postfix[i]) - (int)('0');
                    s.Push(a);
                }
                else if (s.Count > 1)
                {
                    a = s.Pop();
                    b = s.Pop();
                    if (postfix[i] == '+')
                    {
                        s.Push(b + a);
                        stepbystep += b.ToString() + "+" + a.ToString() + "\n";
                    }
                    else if (postfix[i] == '-')
                    {
                        s.Push(b - a);
                        stepbystep += b.ToString() + "-" + a.ToString() + "\n";
                    }
                    else if (postfix[i] == '*')
                    {
                        s.Push(b * a);
                        stepbystep += b.ToString() + "*" + a.ToString() + "\n";
                    }
                    else if (postfix[i] == '/')
                    {
                        s.Push((int)(b / a));
                        stepbystep += b.ToString() + "/" + a.ToString() + "\n";
                    }
                    else if (postfix[i] == '^')
                    {
                        s.Push(Convert.ToInt32(Math.Pow(b,a)));
                        stepbystep += b.ToString() + "^" + a.ToString() + "\n";
                    }
                    
                }
                else if (s.Count == 1)
                {
                    if (postfix[i] == '-')
                    {
                        a = s.Pop();
                        s.Push(-a);
                    }
                }
                
            }
            richTextBox3.Text = stepbystep;
            richTextBox3.Text += "Ответ: " + s.Pop().ToString() + "\n";
        }

        public int getPriority(char sym1)
        {
            int prior1 = 0;
            if (sym1 == '+' || sym1 == '-')
            {
                prior1 = 1;
            }
            if (sym1 == '*' || sym1 == '/')
            {
                prior1 = 2;
            }
            if (sym1 == '^')
            {
                prior1 = 3;
            }
            return prior1;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BinaryTreeForm form = new BinaryTreeForm();
            form.Expression = richTextBox2.Text;
            form.Show();
        }
    }
}
