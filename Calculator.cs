using System;
using System.Text;

public class Calculator
{
    private StringBuilder currentNumber;
    private string operation;
    private decimal result;
    private bool newNumber;

    public Calculator()
    {
        currentNumber = new StringBuilder("0");
        operation = "";
        result = 0.0m;
        newNumber = true;
    }

    public void AppendDigit(string digit)
    {
        if (newNumber)
        {
            currentNumber.Clear();
            newNumber = false;
        }

        if (currentNumber.ToString() == "0" && digit != ".")
        {
            currentNumber.Clear();
        }

        if (digit == "." && currentNumber.ToString().Contains("."))
        {
            return;
        }

        currentNumber.Append(digit);
    }

    public void SetOperation(string newOperation)
    {
        if (!newNumber)
        {
            Calculate();
        }
        operation = newOperation;
        result = decimal.Parse(currentNumber.ToString());
        newNumber = true;
    }

    public void Calculate()
    {
        if (!newNumber && !string.IsNullOrEmpty(operation))
        {
            decimal secondNumber = decimal.Parse(currentNumber.ToString());
            switch (operation)
            {
                case "+":
                    result += secondNumber;
                    break;
                case "-":
                    result -= secondNumber;
                    break;
                case "*":
                    result *= secondNumber;
                    break;
                case "/":
                    if (secondNumber != 0)
                    {
                        result /= secondNumber;
                    }
                    else
                    {
                        ClearAll();
                        return;
                    }
                    break;
            }
            operation = "";
            currentNumber.Clear();
            currentNumber.Append(result.ToString());
        }
    }

    public void AppendDecimalPoint()
    {
        if (newNumber || currentNumber.ToString() == "")
        {
            currentNumber.Clear();
            currentNumber.Append("0");
        }

        if (!currentNumber.ToString().Contains(","))
        {
            currentNumber.Append(",");
        }
    }

    public void Clear()
    {
        currentNumber.Clear();
        currentNumber.Append("0");
    }

    public void ClearAll()
    {
        currentNumber.Clear();
        operation = "";
        result = 0.0m;
        newNumber = true;
    }

    public void RemoveLastDigit()
    {
        if (!newNumber && currentNumber.Length > 0)
        {
            currentNumber.Remove(currentNumber.Length - 1, 1);
            if (currentNumber.Length == 0)
            {
                currentNumber.Append("0");
                newNumber = true;
            }
        }
    }

    public string GetHistory()
    {
        return operation != "" ? result.ToString() + " " + operation : "";
    }

    public string GetCurrentNumber()
    {
        return currentNumber.ToString();
    }
}
