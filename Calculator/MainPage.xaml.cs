using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Calculator
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private Double resultHolder = 0;
        private String operationsPerformed = "";
        private Boolean multipleDigits = false;

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(350.5, 525.25);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }


        private void NumberReciever(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            if(multipleDigits == true)
            {
                tbInput.Text = "";
            }
            else
            {
                if (tbInput.Text == "" && (String)bt.Content == "." || tbInput.Text == "-" && (String)bt.Content == ".")
                {
                    tbInput.Text += "0.";
                }
                else
                {
                    tbInput.Text += bt.Content;
                    multipleDigits = false;
                }
            }
        }

        private void OperatorReciever(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            operationsPerformed = "";
            operationsPerformed += (String)bt.Content;
            if (tbInput.Text == "" && (String)bt.Content == "-")
            {
                tbInput.Text += "-";
            }
            else
            {
                resultHolder = Double.Parse(tbInput.Text);
                if (tbOutput.Text == "")
                {
                    tbOutput.Text += tbInput.Text;
                    tbOutput.Text += bt.Content;
                }
                else
                {
                    tbOutput.Text += bt.Content;
                    tbOutput.Text += tbInput.Text;
                }
                tbInput.Text = "";
            }
        }

        private void ResetCalculator(object sender, RoutedEventArgs e)
        {
            tbInput.Text = "";
            tbOutput.Text = "";
            resultHolder = 0;
            operationsPerformed = "";
            multipleDigits = false;
        }

        private void DeleteLastEntered(object sender, RoutedEventArgs e)
        {
            tbInput.Text = "";
        }

        private void EqualOperator(object sender, RoutedEventArgs e)
        {
            Calculate();
        }

        private void Calculate()
        {
            if (tbOutput.Text.EndsWith("+") || tbOutput.Text.EndsWith("-") || tbOutput.Text.EndsWith("*") || tbOutput.Text.EndsWith("÷"))
            {
                tbOutput.Text += "";
            }
            else
            {
                tbOutput.Text += operationsPerformed;
            }

            if (operationsPerformed == "+")
            {
                resultHolder += Double.Parse(tbInput.Text);
                tbOutput.Text += tbInput.Text;
                tbInput.Text = resultHolder.ToString();
            }
            else if (operationsPerformed == "*")
            {
                resultHolder = resultHolder * Double.Parse(tbInput.Text);
                tbOutput.Text += tbInput.Text;
                tbInput.Text = resultHolder.ToString();
            }
            else if (operationsPerformed == "-")
            {
                resultHolder = resultHolder - Double.Parse(tbInput.Text);
                tbOutput.Text += tbInput.Text;
                tbInput.Text = resultHolder.ToString();
            }
            else
            {
                resultHolder = resultHolder / Double.Parse(tbInput.Text);
                tbOutput.Text += tbInput.Text;
                tbInput.Text = resultHolder.ToString();
            }
            multipleDigits = true;
        }
        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            if ((e.Handled = !(Char.IsDigit((Char)e.Key))) && (e.Handled = !(Char.IsPunctuation((Char)e.Key))) && (e.Handled = !(Char.IsSymbol((Char)e.Key))) && (e.Handled = !(Char.IsControl((Char)e.Key))))
            {
                e.Handled = true;
            }
            else
            {
                tbInput.Text += (Char)e.Key;
            }
            base.OnKeyDown(e);
        }
    }
}
