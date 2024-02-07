using System.Text;
using ConsoleApp1.Expression_Formatting;
using ConsoleApp1.Functions_Versions;
using ConsoleApp1.Peripherial_Classes;

namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        Functions _functions = new Functions();
        TrigFunctions _trigFunctions = new TrigFunctions();
        Parenthesis _parenthesis = new Parenthesis();
        ExpressionFormatting _expFormat = new ExpressionFormatting();

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
            var stringToAdd = _expFormat.GetFormattedDecimalString(expression_label.Text);
            expression_label.Text += stringToAdd;
        }

        private void buttonPlus_Click(object sender, EventArgs e)
        {
            if (expression_label.Text == string.Empty || expression_label.Text.EndsWith('.'))
            {
                expression_label.Text += "0+";
            }
            else if (!(Operator.IsOperator(expression_label.Text.Last()) || expression_label.Text.EndsWith('.')))
            {
                expression_label.Text += '+';
            }
        }

        private void buttonMinus_Click(object sender, EventArgs e)
        {
            if (expression_label.Text == string.Empty || expression_label.Text.EndsWith('.') || expression_label.Text.EndsWith('('))
            {
                expression_label.Text += "0-";

                return;
            }

            var lastIsOperator = Operator.IsOperator(expression_label.Text.Last());
            var preLastIsNotOperator = !(Operator.IsOperator(expression_label.Text.Reverse().ToArray()[1]));

            if (lastIsOperator && preLastIsNotOperator)
            {
                expression_label.Text += '-';

                return;
            }

            expression_label.Text += '-';
        }

        private void buttonMultiply_Click(object sender, EventArgs e)
        {
            if (expression_label.Text == string.Empty || expression_label.Text.EndsWith('.') || expression_label.Text.EndsWith('('))
            {
                expression_label.Text += "0*";
            }
            else if (!Operator.IsOperator(expression_label.Text.Last()))
            {
                expression_label.Text += '*';
            }
        }

        private void buttonDivide_Click(object sender, EventArgs e)
        {
            if (expression_label.Text == string.Empty || expression_label.Text.EndsWith('.') || expression_label.Text.EndsWith('('))
            {
                expression_label.Text += "0/";
            }
            else if (!Operator.IsOperator(expression_label.Text.Last()))
            {
                expression_label.Text += '/';
            }
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (_parenthesis.HasUnmatchedParenthesis(expression_label.Text))
            {
                MessageBox.Show("Expression contains unmatched parenthesis");

                return;
            }

            var properEnding = _expFormat.GetFormattedEndingToTheInput(expression_label.Text);
            expression_label.Text += properEnding;
            previous_expression_label.Text = expression_label.Text;
            
            try
            {
                var result = _functions.ComputeExpressionV2_1(expression_label.Text);
                expression_label.Text = _expFormat.GetFormattedOperand(result);
            }
            catch(Exception ex)
            {
                MessageBox.Show("something went wrong");
            }
        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            previous_expression_label.Text = string.Empty;
            expression_label.Text = string.Empty;
        }

        private void button_parenthesis_1_Click(object sender, EventArgs e)
        {
            var whatToAdd = _parenthesis.GetFormattedParenthesis_1(expression_label.Text);
            expression_label.Text += whatToAdd;
        }

        private void button_parenthesis_2_Click(object sender, EventArgs e)
        {
            var whatToAdd = _parenthesis.GetFormattedParenthesis_2(expression_label.Text);
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
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void buttonSin_Click(object sender, EventArgs e)
        {
            try
            {
                var toAdd = _trigFunctions.GetFormattedTrigFunc(expression_label.Text, "sin", out var index);
                var text = new string(expression_label.Text.Remove(index));
                expression_label.Text = $"{text}{toAdd}";
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonCos_Click(object sender, EventArgs e)
        {
            try
            {
                var toAdd = _trigFunctions.GetFormattedTrigFunc(expression_label.Text, "cos", out var index);
                var text = new string(expression_label.Text.Remove(index));
                expression_label.Text = $"{text}{toAdd}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonTan_Click(object sender, EventArgs e)
        {
            try
            {
                var toAdd = _trigFunctions.GetFormattedTrigFunc(expression_label.Text, "tan", out var index);
                var text = new string(expression_label.Text.Remove(index));
                expression_label.Text = $"{text}{toAdd}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void buttonCot_Click(object sender, EventArgs e)
        {
            try
            {
                var toAdd = _trigFunctions.GetFormattedTrigFunc(expression_label.Text, "cot", out var index);
                var text = new string(expression_label.Text.Remove(index));
                expression_label.Text = $"{text}{toAdd}";
            }
            catch (Exception ex)
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
