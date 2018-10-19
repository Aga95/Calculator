package com.example.aga.calculator

import android.support.v7.app.AppCompatActivity
import android.os.Bundle

import kotlinx.android.synthetic.main.activity_main.*



class MainActivity : AppCompatActivity()
{

    private var resultHolder: Double = 0.0
    private var operationsPerformed = ""
    private var multipleDigits = false
    private var lastOperationPerformed = ""

    override fun onCreate(savedInstanceState: Bundle?)
    {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)

        //Number Buttons
        zeroBtn.setOnClickListener { numberReceiver("0") }
        oneBtn.setOnClickListener{numberReceiver("1")}
        twoBtn.setOnClickListener{numberReceiver("2")}
        threeBtn.setOnClickListener{numberReceiver("3")}
        fourBtn.setOnClickListener{numberReceiver("4")}
        fiveBtn.setOnClickListener{numberReceiver("5")}
        sixBtn.setOnClickListener{numberReceiver("6")}
        sevenBtn.setOnClickListener{numberReceiver("7")}
        eightBtn.setOnClickListener{numberReceiver("8")}
        nineBtn.setOnClickListener{numberReceiver("9")}
        decimalBtn.setOnClickListener{numberReceiver(".")}

        //Operator Buttons
        additionBtn.setOnClickListener { operatorReceiver("+") }
        subtractBtn.setOnClickListener { operatorReceiver("-") }
        divideBtn.setOnClickListener { operatorReceiver("/") }
        multiplyBtn.setOnClickListener { operatorReceiver("*") }
        equalBtn.setOnClickListener { calculate("=") }

        //Additional Buttons
        clearBtn.setOnClickListener { resetCalculator() }
        backBtn.setOnClickListener { deleteLastEntered() }
    }

    private fun numberReceiver(string: String)
    {
        val decimal = "0."

        //Tests for the different conditions of input.
        //The first two tests is to prevent wrong entry of punctiotation mark
        if((numberView.text == "" && string == ".") || (numberView.text == "-" && string == "."))
        {
            numberView.append(decimal)
        }
        else if(string == ".")
        {
            if((numberView.text.contains(".")))
                numberView.append("")
            else
                numberView.append(".")

        }
        //The third and fourth are for regular number inputs, the tests are to make sure that the input gets deleted for the new number
        //after pressing an operator button and to let the user add more than one digit as well as delete the digit from before.
        else if(!this.multipleDigits)
        {
            numberView.append(string)
            this.multipleDigits = false
        }
        else
        {
            numberView.text = ""
            numberView.append(string)
            this.multipleDigits = false
        }
    }

    private fun operatorReceiver(string: String)
    {

        //First we save the operation that is performed upon the use of the button.
        //This will be used later for calculating
        this.operationsPerformed = string

        //Checks if no operation has been made and the input is empty, or to create a negative number
        if((resultView.text == "" && this.lastOperationPerformed == "") || (resultView.text == "-" && this.lastOperationPerformed == "-") || (numberView.text.startsWith("-")))
        {
            if(numberView.text != "")
            {

                this.resultHolder = (numberView.text).toString().toDouble()
                if(resultView.text == "")
                {
                    resultView.append(numberView.text)
                    resultView.append(string)
                }
                else
                {
                    resultView.append(string)
                    resultView.append(numberView.text)
                }
                numberView.text = ""
            }
        }
        //A check to create negative number
        else if(numberView.text == "" && string == "-")
        {
        numberView.append("-")
        }
        //The third test is when the user presses an operator to calculate instead of pressing the equal button
        else
        {
            calculate(string)
        }
        this.lastOperationPerformed = string

    }

    private fun calculate(string: String)
    {
        //The first test is to make sure not to throw the app making the pressed button a dummy that doesnt do anything
        if (resultView.text != "" && (this.lastOperationPerformed === "+" || this.lastOperationPerformed === "-" || this.lastOperationPerformed === "*" || this.lastOperationPerformed === "÷"))
        {
            if (resultView.text.endsWith("+") || resultView.text.endsWith("-") || resultView.text.endsWith("*") ||
                resultView.text.endsWith("/"))
            {
                resultView.append("")
            }
            else
            {
                resultView.append(this.operationsPerformed)
            }
            //To correctly show the operation used in case the user changes their mind
            if (!resultView.text.endsWith(string) && string !== "=")
            {
                resultView.text = resultView.text.substring(0, resultView.text.length - 1)
                resultView.append(string)
            }
            //Four tests that calculates based on the operator used.
            //This is to calculate properly and have order on what type of calculation needs to be done
            if (this.operationsPerformed == "+")
            {
                this.resultHolder += (numberView.text).toString().toDouble();
                resultView.append(numberView.text)
            }
            else if (this.operationsPerformed == "*")
            {
                this.resultHolder = this.resultHolder * (numberView.text).toString().toDouble()
                resultView.append(numberView.text)
            }
            else if (this.operationsPerformed == "-")
            {
                this.resultHolder = this.resultHolder  - (numberView.text).toString().toDouble()
                resultView.append(numberView.text)
            }
            else if(this.operationsPerformed == "/")
            {
                this.resultHolder = this.resultHolder / (numberView.text).toString().toDouble()
                resultView.append(numberView.text)
            }
        }
        this.multipleDigits = true
        numberView.text = this.resultHolder.toString()
    }
    //To restart the user presses the reset button and it will set everything back to new.
    private fun resetCalculator()
    {
        numberView.text = ""
        resultView.text = ""
        this.resultHolder = 0.0
        this.operationsPerformed = ""
        this.multipleDigits = false
        this.lastOperationPerformed = ""
    }
    //Delets the last entered digit in case the user puts in the wrong input
    private fun deleteLastEntered()
    {
        if(numberView.text != "")
        numberView.text = numberView.text.substring(0, numberView.text.length - 1)
    }
}

