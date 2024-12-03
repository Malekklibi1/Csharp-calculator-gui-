using System;
using System.Drawing;
using System.Windows.Forms;

namespace CSharpCalculator
{
    public partial class Form1 : Form
    {
        private TextBox display;
        private double firstOperand = 0;
        private string operation = "";
        private bool isNewNumber = true;

        public Form1()
        {
            InitializeComponent();
            InitializeCalculator();
        }

        private void InitializeCalculator()
        {
            display = new TextBox();
            display.ReadOnly = true;
            display.Location = new System.Drawing.Point(10, 10);
            display.Size = new System.Drawing.Size(260, 30);
            display.Font = new Font("Arial", 14);
            display.TextAlign = HorizontalAlignment.Right;
            this.Controls.Add(display);

            string[] buttonLabels = { "C", "DEL", "/", "*",
                                    "7", "8", "9", "-",
                                    "4", "5", "6", "+",
                                    "1", "2", "3", "=",
                                    "0", "." };
            int xPos = 10, yPos = 50;

            for (int i = 0; i < buttonLabels.Length; i++)
            {
                Button button = new Button();
                button.Text = buttonLabels[i];
                button.Font = new Font("Arial", 12);
                button.BackColor = Color.LightGray;
                button.Click += new EventHandler(Button_Click);

                if (i >= buttonLabels.Length - 2)
                {
                    button.Size = new System.Drawing.Size(130, 40);
                    button.Location = new System.Drawing.Point(10 + ((i % 2) * 140), yPos);
                }
                else
                {
                    button.Size = new System.Drawing.Size(60, 40);
                    button.Location = new System.Drawing.Point(xPos, yPos);
                    xPos += 70;
                    if ((i + 1) % 4 == 0)
                    {
                        xPos = 10;
                        yPos += 50;
                    }
                }

                this.Controls.Add(button);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string buttonText = button.Text;

            try
            {
                switch (buttonText)
                {
                    case "C":
                        display.Text = "0";
                        firstOperand = 0;
                        operation = "";
                        isNewNumber = true;
                        break;

                    case "DEL":
                        if (display.Text.Length > 1)
                            display.Text = display.Text.Substring(0, display.Text.Length - 1);
                        else
                            display.Text = "0";
                        break;

                    case "+":
                    case "-":
                    case "*":
                    case "/":
                        firstOperand = double.Parse(display.Text);
                        operation = buttonText;
                        isNewNumber = true;
                        break;

                    case "=":
                        CalculateResult();
                        break;

                    default:
                        if (isNewNumber)
                        {
                            display.Text = buttonText;
                            isNewNumber = false;
                        }
                        else
                        {
                            display.Text += buttonText;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                display.Text = "Error";
            }
        }

        private void CalculateResult()
        {
            try
            {
                double secondOperand = double.Parse(display.Text);
                double result = 0;

                switch (operation)
                {
                    case "+":
                        result = firstOperand + secondOperand;
                        break;
                    case "-":
                        result = firstOperand - secondOperand;
                        break;
                    case "*":
                        result = firstOperand * secondOperand;
                        break;
                    case "/":
                        if (secondOperand == 0)
                            throw new DivideByZeroException();
                        result = firstOperand / secondOperand;
                        break;
                }

                display.Text = result.ToString("0.##########");
                isNewNumber = true;
            }
            catch (Exception)
            {
                display.Text = "Error";
            }
        }
    }
}
