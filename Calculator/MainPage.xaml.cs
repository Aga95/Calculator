using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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
        private String lastOperationPerformed = "";

        public MainPage()
        {
            this.InitializeComponent();
            ApplicationView.PreferredLaunchViewSize = new Size(350.5, 600);
            ApplicationView.PreferredLaunchWindowingMode = ApplicationViewWindowingMode.PreferredLaunchViewSize;
        }


        //Event Handler for the number buttons
        private void NumberReciever(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;

            //Tests for the different conditions of input.
            //First test is to create a zero with decimal in case the input is empty and the user presses the decimal button.
            if ((tbInput.Text == "" && (String)bt.Content == ".") || (tbInput.Text == "-" && (String)bt.Content == "."))
            {
                tbInput.Text += "0.";
            }
            //The second test is to not let a second decimal point to be entered
            else if (tbInput.Text.Contains(".") && (String)bt.Content == ".")
            {
                tbInput.Text += "";
            }
            //The third and fourth are for regular number inputs, the tests are to make sure that the input gets deleted for the new number
            //after pressing an operator button.
            else if (multipleDigits == false)
            {
                tbInput.Text += bt.Content;
                multipleDigits = false;
            }
            else
            {
                tbInput.Text = "";
                tbInput.Text += bt.Content;
                multipleDigits = false;
            }
        }

        //Event handler for the operator buttons
        private void OperatorReciever(object sender, RoutedEventArgs e)
        {
            Button bt = (Button)sender;
            //First we save the operation that is performed upon the use of the button.
            //This will be used later for calculating
            operationsPerformed = (String)bt.Content;

            //Tests for the different operator conditions.
            //The first test is if the user presses subract before a number is put in and creating a negative number.
            if (tbInput.Text == "" && (String)bt.Content == "-")
            {
                tbInput.Text += "-";
            }
            //The second test checks if no operation has been made and the input is empty, or to create a negative number
            else if((tbOutput.Text == "" && lastOperationPerformed == "") || (tbOutput.Text == "-" && lastOperationPerformed == "-") || (tbInput.Text.StartsWith("-")))
            {
                if(tbInput.Text != "")
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
            //The third test is when the user presses an operator to calculate instead of pressing the equal button
            else
            {
                Calculate(sender);
            }
            lastOperationPerformed = (String)bt.Content;
        }
        //To restart the user presses the reset button and it will set everything back to new.
        private void ResetCalculator(object sender, RoutedEventArgs e)
        {
            tbInput.Text = "";
            tbOutput.Text = "";
            resultHolder = 0;
            operationsPerformed = "";
            multipleDigits = false;
            lastOperationPerformed = "";
        }

        //Delets the last entered digit in case the user puts in the wrong input
        private void DeleteLastEntered(object sender, RoutedEventArgs e)
        {
            tbInput.Text = tbInput.Text.Substring(0, tbInput.Text.Length - 1);
        }

        private void EqualOperator(object sender, RoutedEventArgs e)
        {
            Calculate(sender);
        }

        private void Calculate(object sender)
        {
            Button bt = (Button)sender;
            //The first test is to make sure not to throw the app making the pressed button a dummy that doesnt do anything
            if(tbOutput.Text != "" && (lastOperationPerformed == "+" || lastOperationPerformed == "-" || lastOperationPerformed == "*" || lastOperationPerformed == "÷") )
            {

                if (tbOutput.Text.EndsWith("+") || tbOutput.Text.EndsWith("-") || tbOutput.Text.EndsWith("*") || tbOutput.Text.EndsWith("÷"))
                {
                    tbOutput.Text += "";
                }
                else
                {
                    tbOutput.Text += operationsPerformed;
                }
                //To correctly show the operation used in case the user changes their mind
                if (!tbOutput.Text.EndsWith((String)bt.Content) && (String)bt.Content != "=")
                {
                    tbOutput.Text = tbOutput.Text.Substring(0, tbOutput.Text.Length - 1);
                    tbOutput.Text += (String)bt.Content;
                }
                //Four tests that calculates based on the operator used.
                //This is to calculate properly and have order on what type of calculation needs to be done
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
            }
             multipleDigits = true;
        }

        //A method to hinder the user from entering alphabetical inputs.
        //The method uses regular expression to match up the input with the variable set as the regex.
        protected override void OnKeyDown(KeyRoutedEventArgs e)
        {
            Regex rxNumber = new Regex("[0-9]");
            Char bt = (Char)e.Key;

            if (rxNumber.IsMatch(bt.ToString()))
            {
                tbInput.Text += bt.ToString();
            }
            else if ((bt.ToString() == "¾") || bt.ToString() == ".")
            {
                if (tbInput.Text == "" || tbInput.Text == "-")
                    tbInput.Text += "0.";
                else if (tbInput.Text.Contains("."))
                    tbInput.Text += "";
                else
                    tbInput.Text += ".";
            }
            else
            {
                tbInput.Text += "";
            }
            base.OnKeyDown(e);
        }

    }
}
