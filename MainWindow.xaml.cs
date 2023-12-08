using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CalculatorWpf
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        private double lastNumber;
        private Operator selectedOperator;
        private bool EqualClicked;

        public MainWindow()
        {
            InitializeComponent();

            btnOne.Click += btnNumber_Click;
            btnTwo.Click += btnNumber_Click;
            btnThree.Click += btnNumber_Click;
            btnFour.Click += btnNumber_Click;
            btnFive.Click += btnNumber_Click;
            btnSix.Click += btnNumber_Click;
            btnSeven.Click += btnNumber_Click;
            btnEight.Click += btnNumber_Click;
            btnNine.Click += btnNumber_Click;
            btnZero.Click += btnNumber_Click;
            btnPoint.Click += BtnPoint_Click;
            btnPlus.Click += btnOperation_Click;
            btnMinus.Click += btnOperation_Click;
            btnMultiply.Click += btnOperation_Click;
            btnDivide.Click += btnOperation_Click;
            btnEqual.Click += BtnEqual_Click;
            btnNegative.Click += BtnNegative_Click;
            btnPercentage.Click += BtnPercentage_Click;            
        }

        private void btnNumber_Click(object sender, RoutedEventArgs e)
        {
            string selectedNumber = (sender as Button).Content.ToString();
            lblResult.Content = lblResult.Content.ToString() == "0" ? selectedNumber : lblResult.Content + selectedNumber;
        }

        private void BtnPoint_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = lblResult.Content.ToString().Contains(".") ? lblResult.Content : lblResult.Content + ".";
        }

        private void btnOperation_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = double.TryParse(lblResult.Content.ToString(), out lastNumber) ? "0" : lblResult.Content;
            if (sender == btnPlus)
            {
                selectedOperator = Operator.Addition;
            }
            else if (sender == btnMinus)
            {
                selectedOperator = Operator.Sustraction;
            }
            else if (sender == btnMultiply)
            {
                selectedOperator = Operator.Multiplication;
            }
            else if (sender == btnDivide)
            {
                selectedOperator = Operator.Division;
            }
            EqualClicked = false;
        }

        private void btnAc_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = "0";
            EqualClicked = false;
        }

        private void BtnEqual_Click(object sender, RoutedEventArgs e)
        {
            if (!EqualClicked)
            {
                lblResult.Content = double.TryParse(lblResult.Content.ToString(), out double labelContent) ? Calculate(lastNumber, labelContent, selectedOperator) : lblResult.Content;
            }
            EqualClicked = true;
        }

        private void BtnNegative_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = double.TryParse(lblResult.Content.ToString(), out double labelContent) ? (labelContent * -1) : lblResult.Content;
        }

        private void BtnPercentage_Click(object sender, RoutedEventArgs e)
        {
            lblResult.Content = double.TryParse(lblResult.Content.ToString(), out double labelContent) ? labelContent / 100 : lblResult.Content;
        }

        private double Calculate(double lastNumber, double newNumber, Operator selectedOperator)
        {
            double resultNumber = 0;
            switch (selectedOperator)
            {
                case Operator.Addition:
                    resultNumber = Calculation.Addition(lastNumber, newNumber);
                    break;
                case Operator.Sustraction:
                    resultNumber = Calculation.Substraction(lastNumber, newNumber);
                    break;
                case Operator.Multiplication:
                    resultNumber = Calculation.Multiply(lastNumber, newNumber);
                    break;
                case Operator.Division:
                    resultNumber = Calculation.Division(lastNumber, newNumber);
                    break;
            }
            return resultNumber;
        }
    }

    public enum Operator
    {
        Addition,
        Sustraction,
        Multiplication,
        Division
    }

    public class Calculation
    {
        public static double Addition(double n1, double n2)
        {
            return n1 + n2;
        }

        public static double Substraction(double n1, double n2)
        {
            return n1 - n2;
        }

        public static double Multiply(double n1, double n2)
        {
            return n1 * n2;
        }

        public static double Division(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("Division by 0 is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return n1;
            }

            return n1 / n2;
        }
    }
}
