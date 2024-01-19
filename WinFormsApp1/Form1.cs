using System.Text;
using ConsoleApp1;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Funcs Funcs = new Funcs();
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
            var stringToAdd = Funcs.DecideWhatToAdd(expression_label.Text);
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
                expression_label.Text += "0+";
            }
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            var lastIsOperator = Funcs.IsOperator(expression_label.Text.Last());
            var preLastIsNotOperator = !(Funcs.IsOperator(expression_label.Text.Reverse().ToArray()[1]));
            if (lastIsOperator && preLastIsNotOperator)
            {
                expression_label.Text += '-';
            }
            else if (expression_label.Text == string.Empty || expression_label.Text.EndsWith('.'))
            {
                expression_label.Text += "0-";
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
                expression_label.Text += "0*";
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
                expression_label.Text += "0/";
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
            expression_label.Text = Funcs.ComputeExpressionV2_1(expression_label.Text);
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            previous_expression_label.Text = string.Empty;
            expression_label.Text = string.Empty;
        }

        private void button_parenthesis_1_Click(object sender, EventArgs e)
        {
            var whatToAdd = Funcs.DecideHowToAddParenthesis_1(expression_label.Text);
            expression_label.Text += whatToAdd;
        }

        private void button_parenthesis_2_Click(object sender, EventArgs e)
        {
            var whatToAdd = Funcs.DecideHowToAddParenthesis_2(expression_label.Text);
            expression_label.Text += whatToAdd;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (panel1.Visible)
            {
                panel1.Visible = false;
            }
            else
            {
                panel1.Visible = true;
                // You can also load or update the list items here if needed
                // listBoxItems.Items.Clear();
                // listBoxItems.Items.AddRange(GetYourListItems());
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void buttonTrigFunc_Click(object sender, EventArgs e)
        {
            try
            {
                var result = Funcs.DecideHowToAddTrigFunc(expression_label.Text);
                expression_label.Text = result;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel1.Visible = false;
            if(listBox1.SelectedIndex == 0)
            {
                HideTrigonometric();
                ShowStandard();
            }
            else if(listBox1.SelectedIndex == 1)
            {
                HideStandard();
                ShowTrigonometric();
            }
        }

        private void HideTrigonometric()
        {
            button_sin.Visible = false;
            button_cos.Visible = false;
            button_tan.Visible = false;
            button_cot.Visible = false;
        }

        private void ShowStandard()
        {
            button_minus.Visible = true;
            button_plus.Visible = true;
            button_multiply.Visible = true;
            button_divide.Visible = true;
            button_parenthesis_1.Visible = true;
            button_parenthesis_2.Visible = true;
        }

        private void ShowTrigonometric()
        {
            button_sin.Visible = true;
            button_cos.Visible = true;
            button_tan.Visible = true;
            button_cot.Visible = true;
        }

        private void HideStandard()
        {
            button_minus.Visible = false;
            button_plus.Visible = false;
            button_multiply.Visible = false;
            button_divide.Visible = false;
            button_parenthesis_1.Visible = false;
            button_parenthesis_2.Visible = false;
        }
    }
}
