namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            button_1 = new Button();
            button_4 = new Button();
            button_7 = new Button();
            button_2 = new Button();
            button_5 = new Button();
            button_8 = new Button();
            button_3 = new Button();
            button_6 = new Button();
            button_9 = new Button();
            button_0 = new Button();
            button_equals = new Button();
            button_clear = new Button();
            button_plus = new Button();
            button_minus = new Button();
            button_multiply = new Button();
            button_divide = new Button();
            button_decimal = new Button();
            expression_label = new Label();
            previous_expression_label = new Label();
            SuspendLayout();
            // 
            // button_1
            // 
            button_1.Font = new Font("Agency FB", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_1.Location = new Point(0, 0);
            button_1.Name = "button_1";
            button_1.Size = new Size(75, 75);
            button_1.TabIndex = 0;
            button_1.Text = "1";
            button_1.UseVisualStyleBackColor = true;
            button_1.Click += button1_Click;
            // 
            // button_2
            // 
            button_2.Font = new Font("Agency FB", 20.25F, FontStyle.Bold);
            button_2.Location = new Point(80, 0);
            button_2.Name = "button_2";
            button_2.Size = new Size(75, 75);
            button_2.TabIndex = 3;
            button_2.Text = "2";
            button_2.UseVisualStyleBackColor = true;
            button_2.Click += button2_Click;
            // 
            // button_3
            // 
            button_3.Font = new Font("Agency FB", 20.25F, FontStyle.Bold);
            button_3.Location = new Point(160, 0);
            button_3.Name = "button_3";
            button_3.Size = new Size(75, 75);
            button_3.TabIndex = 6;
            button_3.Text = "3";
            button_3.UseVisualStyleBackColor = true;
            button_3.Click += button3_Click;
            // 
            // button_4
            // 
            button_4.Font = new Font("Agency FB", 20.25F, FontStyle.Bold);
            button_4.Location = new Point(0, 80);
            button_4.Name = "button_4";
            button_4.Size = new Size(75, 75);
            button_4.TabIndex = 1;
            button_4.Text = "4";
            button_4.UseVisualStyleBackColor = true;
            button_4.Click += button4_Click;
            // 
            // button_5
            // 
            button_5.Font = new Font("Agency FB", 20.25F, FontStyle.Bold);
            button_5.Location = new Point(80, 80);
            button_5.Name = "button_5";
            button_5.Size = new Size(75, 75);
            button_5.TabIndex = 4;
            button_5.Text = "5";
            button_5.UseVisualStyleBackColor = true;
            button_5.Click += button5_Click;
            // 
            // button_6
            // 
            button_6.Font = new Font("Agency FB", 20.25F, FontStyle.Bold);
            button_6.Location = new Point(160, 80);
            button_6.Name = "button_6";
            button_6.Size = new Size(75, 75);
            button_6.TabIndex = 7;
            button_6.Text = "6";
            button_6.UseVisualStyleBackColor = true;
            button_6.Click += button6_Click;
            // 
            // button_7
            // 
            button_7.Font = new Font("Agency FB", 20.25F, FontStyle.Bold);
            button_7.Location = new Point(0, 160);
            button_7.Name = "button_7";
            button_7.Size = new Size(75, 75);
            button_7.TabIndex = 2;
            button_7.Text = "7";
            button_7.UseVisualStyleBackColor = true;
            button_7.Click += button7_Click;
            // 
            // button_8
            // 
            button_8.Font = new Font("Agency FB", 20.25F, FontStyle.Bold);
            button_8.Location = new Point(80, 160);
            button_8.Name = "button_8";
            button_8.Size = new Size(75, 75);
            button_8.TabIndex = 5;
            button_8.Text = "8";
            button_8.UseVisualStyleBackColor = true;
            button_8.Click += button8_Click;
            // 
            // button_9
            // 
            button_9.Font = new Font("Agency FB", 20.25F, FontStyle.Bold);
            button_9.Location = new Point(160, 160);
            button_9.Name = "button_9";
            button_9.Size = new Size(75, 75);
            button_9.TabIndex = 8;
            button_9.Text = "9";
            button_9.UseVisualStyleBackColor = true;
            button_9.Click += button9_Click;
            // 
            // button_0
            // 
            button_0.Font = new Font("AMGDT_IV25", 27.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_0.Location = new Point(0, 240);
            button_0.Name = "button_0";
            button_0.Size = new Size(155, 75);
            button_0.TabIndex = 9;
            button_0.Text = "0";
            button_0.UseVisualStyleBackColor = true;
            button_0.Click += button0_Click;
            // 
            // button_equals
            // 
            button_equals.Font = new Font("Agency FB", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_equals.Location = new Point(256, 240);
            button_equals.Name = "button_equals";
            button_equals.Size = new Size(75, 39);
            button_equals.TabIndex = 10;
            button_equals.Text = "=";
            button_equals.UseVisualStyleBackColor = true;
            button_equals.Click += buttonEquals_Click;
            // 
            // button_clear
            // 
            button_clear.Font = new Font("Agency FB", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            button_clear.Location = new Point(256, 285);
            button_clear.Name = "button_clear";
            button_clear.Size = new Size(75, 30);
            button_clear.TabIndex = 11;
            button_clear.Text = "CLEAR";
            button_clear.UseVisualStyleBackColor = true;
            button_clear.Click += buttonClear_Click;
            // 
            // button_plus
            // 
            button_plus.Font = new Font("Segoe UI", 13F);
            button_plus.Location = new Point(256, 160);
            button_plus.Name = "button_plus";
            button_plus.Size = new Size(36, 36);
            button_plus.TabIndex = 12;
            button_plus.Text = "+";
            button_plus.UseVisualStyleBackColor = true;
            button_plus.Click += buttonPlus_Click;
            // 
            // button_minus
            // 
            button_minus.Font = new Font("Segoe UI", 13F);
            button_minus.Location = new Point(294, 160);
            button_minus.Name = "button_minus";
            button_minus.Size = new Size(37, 36);
            button_minus.TabIndex = 13;
            button_minus.Text = "-";
            button_minus.UseVisualStyleBackColor = true;
            button_minus.Click += buttonMinus_Click;
            // 
            // button_multiply
            // 
            button_multiply.Font = new Font("Segoe UI", 13F);
            button_multiply.Location = new Point(256, 202);
            button_multiply.Name = "button_multiply";
            button_multiply.Size = new Size(36, 33);
            button_multiply.TabIndex = 14;
            button_multiply.Text = "*";
            button_multiply.UseVisualStyleBackColor = true;
            button_multiply.Click += buttonMultiply_Click;
            // 
            // button_divide
            // 
            button_divide.Font = new Font("Segoe UI", 13F);
            button_divide.Location = new Point(294, 202);
            button_divide.Name = "button_divide";
            button_divide.Size = new Size(37, 33);
            button_divide.TabIndex = 15;
            button_divide.Text = "/";
            button_divide.UseVisualStyleBackColor = true;
            button_divide.Click += buttonDivide_Click;
            // 
            // button_decimal
            // 
            button_decimal.Font = new Font("Stencil", 14.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
            button_decimal.Location = new Point(160, 240);
            button_decimal.Name = "button_decimal";
            button_decimal.Size = new Size(75, 75);
            button_decimal.TabIndex = 17;
            button_decimal.Text = ".";
            button_decimal.UseVisualStyleBackColor = true;
            button_decimal.Click += buttonDecimal_Click;
            // 
            // expression_label
            // 
            expression_label.AutoSize = true;
            expression_label.Font = new Font("ISOCP_IV25", 12F, FontStyle.Bold, GraphicsUnit.Point, 204);
            expression_label.Location = new Point(256, 50);
            expression_label.Name = "expression_label";
            expression_label.Size = new Size(0, 25);
            expression_label.TabIndex = 19;
            // 
            // previous_expression_label
            // 
            previous_expression_label.AutoSize = true;
            previous_expression_label.Location = new Point(256, 25);
            previous_expression_label.Name = "previous_expression_label";
            previous_expression_label.Size = new Size(0, 15);
            previous_expression_label.TabIndex = 20;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(377, 321);
            Controls.Add(previous_expression_label);
            Controls.Add(expression_label);
            Controls.Add(button_decimal);
            Controls.Add(button_divide);
            Controls.Add(button_multiply);
            Controls.Add(button_minus);
            Controls.Add(button_plus);
            Controls.Add(button_clear);
            Controls.Add(button_equals);
            Controls.Add(button_1);
            Controls.Add(button_4);
            Controls.Add(button_7);
            Controls.Add(button_2);
            Controls.Add(button_5);
            Controls.Add(button_8);
            Controls.Add(button_3);
            Controls.Add(button_6);
            Controls.Add(button_9);
            Controls.Add(button_0);
            Name = "Form1";
            Text = "MyCalc";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button_1;
        private Button button_4;
        private Button button_7;
        private Button button_2;
        private Button button_5;
        private Button button_8;
        private Button button_3;
        private Button button_6;
        private Button button_9;
        private Button button_0;
        private Button button_equals;
        private Button button_clear;
        private Button button_plus;
        private Button button_minus;
        private Button button_multiply;
        private Button button_divide;
        private Button button_decimal;
        private Label expression_label;
        private Label previous_expression_label;
    }
}
