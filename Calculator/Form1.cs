using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class Calculator : Form
    {

        //GLOBAL VARIABLES
        //double num1, num2;
        int i = 0;
        double[] num = new double[100];
        string operators, fullEqn;
        string[] postfix = { };
        Stack finalPost_fix = new Stack();
        //bool isSign = false;
        //GLOBAL VARIABLE


        public Calculator()
        {
            InitializeComponent();
        }


        private void num_click(object sender, EventArgs e)
        {
            Button num = (Button)sender;
            if ((result.Text == "0"))
            {
                equation.Text = "";
                result.Clear();
            }


            if (num.Text == point.Text)
            {
                if (!result.Text.Contains(point.Text))
                {
                    result.Text = result.Text + point.Text;
                    equation.Text = equation.Text + point.Text;
                }
            }
            else
            {
                result.Text = result.Text + num.Text;
                equation.Text = equation.Text + num.Text;
            }

        }


        private void sign_click(object sender, EventArgs e)
        {
            Button sign = (Button)sender;

            num[i] = Convert.ToDouble(result.Text);
            i++;
            operators = sign.Text;
            equation.Text = equation.Text + sign.Text;
            
            result.Clear();

        }


        private void clear_btn(object sender, EventArgs e)
        {
            equation.Text = "0";
            result.Text = "0";
            i = 0;
            
        }

        private void equals_click(object sender, EventArgs e)
        {
            num[i] = Convert.ToDouble(result.Text);
            i++;
           // num[i] = 999999999;
            fullEqn = equation.Text;
            toPostfix();

            finalResult();

            
        }


        public void toPostfix()
        {

            Stack opp_post_fix = new Stack();

            Stack sign = new Stack(); //stack for handling sign

            //string[] sign = { };

            int x = 0;

            for (int i = 0; i < fullEqn.Length; i++)
            {
                char c = fullEqn[i];
                if (c == '.')
                {
                    continue;
                }
                if (!char.IsLetterOrDigit(c))
                {
                    // c is operator
                    opp_post_fix.Push(num[x]);
                    x++;
                    //char prev_op = Convert.ToChar(sign.Peek());

                    while (sign.Count > 0 && Prec(c) <= Prec(Convert.ToChar(sign.Peek())))
                    {
                        opp_post_fix.Push(sign.Pop());
                    }
                    sign.Push(c);
                }
                if (i == fullEqn.Length-1)
                {
                    opp_post_fix.Push(num[x]);
                    while (sign.Count != 0)
                    {
                        opp_post_fix.Push(sign.Pop());
                    }

                }

            }
            int j = opp_post_fix.Count;
            for (int i = 0; i < j; i++)
            {

                finalPost_fix.Push(opp_post_fix.Pop());

            }

        }


        internal static int Prec(char ch)
        {
            switch (ch)
            {
                case '+':
                case '-':
                    return 1;

                case '*':
                case '/':
                    return 2;

                case '^':
                    return 3;
            }
            return -1;
        }

        private void standardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Width = 255;
            this.Height = 321;
        }

        private void temperatureToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Width = 533;
            this.Height = 321;
        }
        private void developerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            this.Width = 533;
            this.Height = 365;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void celciusToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        Char tempConvertor = 'C';
        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            tempConvertor = 'C';
            sign1.Text = "C";
            sign2.Text = "F";
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            tempConvertor = 'F';
            sign1.Text = "F";
            sign2.Text = "C";
        }

        private void convert_click(object sender, EventArgs e)
        {
            if (tempConvertor == 'C')
            {
                converted.Text = Convert.ToString((Convert.ToDouble(enteredValue.Text) * 1.8) + 32);
            }
            else
            {
                converted.Text = Convert.ToString((Convert.ToDouble(enteredValue.Text) -32)*5/9);
            }
            
        }

        private void button22_Click(object sender, EventArgs e)
        {
            enteredValue.Clear();
            converted.Clear();
        }


        public void finalResult()
        {
            Stack last = new Stack();
            //int answer;
            double temp = 0;
            int j = finalPost_fix.Count;
            for (int i = 0; i < j; i++)
            {

                Type ty = finalPost_fix.Peek().GetType();

                if (ty.Equals(typeof(double)))
                {
                    last.Push(finalPost_fix.Pop());
                    //finalPost_fix.Pop();
                }
                else
                {
                    char op = Convert.ToChar(finalPost_fix.Pop());

                    double s = Convert.ToDouble(last.Pop());

                    double t = Convert.ToDouble(last.Pop());

                    switch (op)
                    {
                        case '+':
                            temp = t + s;

                            last.Push(temp);

                            break;
                        case '-':
                            temp = t - s;
                            last.Push(temp);
                            break;
                        case '/':
                            temp = t / s;
                            last.Push(temp);
                            break;
                        case '*':
                            temp = t * s;

                            last.Push(temp);

                            break;
                        default:
                            break;
                    }
                }
            }
           double finalAns = temp;
           result.Text = Convert.ToString(finalAns);
        }
    }
}