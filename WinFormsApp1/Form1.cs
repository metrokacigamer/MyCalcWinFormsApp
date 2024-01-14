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
            expressionText.Text += 1;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            expressionText.Text += 2;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            expressionText.Text += 3;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            expressionText.Text += 4;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            expressionText.Text += 5;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            expressionText.Text += 6;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            expressionText.Text += 7;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            expressionText.Text += 8;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            expressionText.Text += 9;
        }

        private void button0_Click(object sender, EventArgs e)
        {
            expressionText.Text += 0;
        }

        private void buttonDecimal_Click(object sender, EventArgs e)
        {
            //if(!(expressionText.Text.EndsWith('+')|| expressionText.Text.EndsWith('-') || expressionText.Text.EndsWith('/') || expressionText.Text.EndsWith('*') || expressionText.Text.EndsWith('.')))
            //{
            //    expressionText.Text += '.';
            //}
            //else if (expressionText.Text == string.Empty)
            //{
            //    expressionText.Text += "1.";
            //}
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (!(expressionText.Text.EndsWith('+') || expressionText.Text.EndsWith('-') || expressionText.Text.EndsWith('/') || expressionText.Text.EndsWith('*') || expressionText.Text.EndsWith('.')))
            {
                expressionText.Text += '+';
            }
            else if (expressionText.Text == string.Empty)
            {
                expressionText.Text += "1+";
            }
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (!(expressionText.Text.EndsWith('+') || expressionText.Text.EndsWith('-') || expressionText.Text.EndsWith('/') || expressionText.Text.EndsWith('*') || expressionText.Text.EndsWith('.')))
            {
                expressionText.Text += '-';
            }
            else if (expressionText.Text == string.Empty)
            {
                expressionText.Text += "1-";
            }
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            if (!(expressionText.Text.EndsWith('+') || expressionText.Text.EndsWith('-') || expressionText.Text.EndsWith('/') || expressionText.Text.EndsWith('*') || expressionText.Text.EndsWith('.')))
            {
                expressionText.Text += '*';
            }
            else if (expressionText.Text == string.Empty)
            {
                expressionText.Text += "1*";
            }
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            if (!(expressionText.Text.EndsWith('+') || expressionText.Text.EndsWith('-') || expressionText.Text.EndsWith('/') || expressionText.Text.EndsWith('*') || expressionText.Text.EndsWith('.')))
            {
                expressionText.Text += '/';
            }
            else if (expressionText.Text == string.Empty)
            {
                expressionText.Text += "1/";
            }
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (expressionText.Text.EndsWith('+') || expressionText.Text.EndsWith('-') || expressionText.Text.EndsWith('/') || expressionText.Text.EndsWith('*'))
            {
                expressionText.Text += 1;
            }
            else if (expressionText.Text.EndsWith('.'))
            {
                expressionText.Text += 0;
            }
            else if (expressionText.Text == string.Empty)
            {
                expressionText.Text += "0";
            }
            expressionText.Text = Funcs.ComputeExpression(expressionText.Text).ToString();
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            expressionText.Text = string.Empty;
        }
    }
}
