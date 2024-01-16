using System.Text;
using ConsoleApp1;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            expression_label.Text += 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            expression_label.Text += 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            expression_label.Text += 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            expression_label.Text += 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            expression_label.Text += 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            expression_label.Text += 6;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            expression_label.Text += 7;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            expression_label.Text += 8;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            expression_label.Text += 9;
        }

        private void button0_Click(object sender, EventArgs e)
        {
            expression_label.Text += 0;
        }

        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            var stringToAdd = Funcs.WhatToAdd(expression_label.Text);
            expression_label.Text += stringToAdd;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (!(expression_label.Text.EndsWith('+') || expression_label.Text.EndsWith('-') || expression_label.Text.EndsWith('/') || expression_label.Text.EndsWith('*') || expression_label.Text.EndsWith('.')))
            {
                expression_label.Text += '+';
            }
            else if (expression_label.Text == string.Empty)
            {
                expression_label.Text += "1+";
            }
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (!(expression_label.Text.EndsWith('+') || expression_label.Text.EndsWith('-') || expression_label.Text.EndsWith('/') || expression_label.Text.EndsWith('*') || expression_label.Text.EndsWith('.')))
            {
                expression_label.Text += '-';
            }
            else if (expression_label.Text == string.Empty)
            {
                expression_label.Text += "1-";
            }
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            if (!(expression_label.Text.EndsWith('+') || expression_label.Text.EndsWith('-') || expression_label.Text.EndsWith('/') || expression_label.Text.EndsWith('*') || expression_label.Text.EndsWith('.')))
            {
                expression_label.Text += '*';
            }
            else if (expression_label.Text == string.Empty)
            {
                expression_label.Text += "1*";
            }
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            if (!(expression_label.Text.EndsWith('+') || expression_label.Text.EndsWith('-') || expression_label.Text.EndsWith('/') || expression_label.Text.EndsWith('*') || expression_label.Text.EndsWith('.')))
            {
                expression_label.Text += '/';
            }
            else if (expression_label.Text == string.Empty)
            {
                expression_label.Text += "1/";
            }
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (expression_label.Text.EndsWith('+') || expression_label.Text.EndsWith('-') || expression_label.Text.EndsWith('/') || expression_label.Text.EndsWith('*'))
            {
                expression_label.Text += 1;
            }
            else if (expression_label.Text.EndsWith('.'))
            {
                expression_label.Text += 0;
            }
            else if (expression_label.Text == string.Empty)
            {
                expression_label.Text += 0;
            }
            previous_expression_label.Text = expression_label.Text;
            expression_label.Text = Funcs.ComputeExpression2(expression_label.Text);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            previous_expression_label.Text = string.Empty;
            expression_label.Text = string.Empty;
        }
    }
}
