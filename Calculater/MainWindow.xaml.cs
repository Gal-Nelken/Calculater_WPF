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

namespace Calculater
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNum, result;
        bool isCalcDone = false;
        SelectedOperator selectedOperator;

        // --- CONSTRUCTOR --- \\
        public MainWindow()
        {
            InitializeComponent();

            ACBtn.Click += ACBtn_Click;
            NegativeBtn.Click += NegativeBtn_Click;
            PercentageBtn.Click += PercentageBtn_Click;
            DotBtn.Click += DotBtn_Click;
            EquelBtn.Click += EquelBtn_Click;

            DivisionBtn.Click += OperationBtn_Click;
            MultiplyBtn.Click += OperationBtn_Click;
            MinusBtn.Click += OperationBtn_Click;
            PlusBtn.Click += OperationBtn_Click;
        }

        // --- ACTIONS FUNCIONS --- \\
        //CLEAR FUNCTION
        private void ACBtn_Click(object sender, RoutedEventArgs e)
        {
            ResultLabel.Content = "0";
            result = 0;
            lastNum = 0;
        }

        //NEGATIVE FUNCTION
        private void NegativeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out lastNum))
            {
                if (ResultLabel.Content.ToString() == "0") return;
                lastNum *= -1;
                ResultLabel.Content = lastNum.ToString();
            }
        }

        // PERCENTAGE FUNCTION
        private void PercentageBtn_Click(object sender, RoutedEventArgs e)
        {
            double tempNum;
            if (double.TryParse(ResultLabel.Content.ToString(), out tempNum))
            {
                tempNum /= 100;
                if (lastNum != 0) tempNum *= lastNum;
                ResultLabel.Content = tempNum.ToString();
            }
        }

        // EQUEL FUNCTION
        private void EquelBtn_Click(object sender, RoutedEventArgs e)
        {
            double newNum;

            if (double.TryParse(ResultLabel.Content.ToString(), out newNum))
             {
                switch(selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNum, newNum);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Substract(lastNum, newNum);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNum, newNum);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNum, newNum);
                        break;

                }
            }
            ResultLabel.Content = result.ToString();
            isCalcDone = true;
        }

        // ADD DOT FUNCTION
        private void DotBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!ResultLabel.Content.ToString().Contains(".")) ResultLabel.Content = $"{ResultLabel.Content}.";
        }

        // OPERATION FUNCTION 
        private void OperationBtn_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(ResultLabel.Content.ToString(), out lastNum)) ResultLabel.Content = "0";
            isCalcDone = false;

            if (sender == MultiplyBtn) selectedOperator = SelectedOperator.Multiplication;
            if (sender == DivisionBtn) selectedOperator = SelectedOperator.Division;
            if (sender == MinusBtn) selectedOperator = SelectedOperator.Subtraction;
            if (sender == PlusBtn) selectedOperator = SelectedOperator.Addition;
        }



        // NUMBERS FUNCTION
        private void NumberBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isCalcDone)
            {
                ResultLabel.Content = "0";

                isCalcDone = false;
            }
                
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (ResultLabel.Content.ToString() == "0") ResultLabel.Content = $"{selectedValue}";
            else ResultLabel.Content = $"{ResultLabel.Content}{selectedValue}";
        }
        
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }



    public class SimpleMath
    {
        public static double Add (double num1, double num2)
        {
            return num1 + num2; 
        }
        public static double Substract (double num1, double num2)
        {
            return num1 - num2;
        }
          public static double Multiply (double num1, double num2)
        {
            return num1 * num2;
        }
          public static double Divide (double num1, double num2)
        {
            if (num2 == 0)
            {
                MessageBox.Show("Cannot divide by zero", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return num1 / num2;
        }
    }

}
