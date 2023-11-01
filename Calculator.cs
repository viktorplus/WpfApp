using System;
using System.Text;

namespace WpfApp
{
    public class Calculator
    {
        private StringBuilder currentNumber;
        private StringBuilder history;
        private string lastOperation;
        private double result;
        private double previousResult;
        private string previousOperation;

        public Calculator()
        {
            currentNumber = new StringBuilder();
            history = new StringBuilder();
            lastOperation = "";
            result = 0;
            previousResult = 0;
            previousOperation = "";
        }

        public void AppendDigit(string digit)
        {
            if (digit == "0" && currentNumber.ToString() == "0")
            {
                // Не добавляем ведущий ноль
                return;
            }
            if (digit == "." && currentNumber.ToString().Contains("."))
            {
                // Не добавляем вторую точку
                return;
            }
            currentNumber.Append(digit);
        }

        public void SetOperation(string operation)
        {
            if (currentNumber.Length > 0)
            {
                if (previousOperation != "")
                {
                    Calculate();
                }
                previousOperation = lastOperation;
                previousResult = double.Parse(currentNumber.ToString());
                lastOperation = operation;
                history.Append(currentNumber);
                history.Append(" ");
                history.Append(operation);
                history.Append(" ");
                currentNumber.Clear();
            }
        }

        public void Calculate()
        {
            if (currentNumber.Length > 0)
            {
                double secondOperand = double.Parse(currentNumber.ToString());
                switch (previousOperation)
                {
                    case "+":
                        previousResult += secondOperand;
                        break;
                    case "-":
                        previousResult -= secondOperand;
                        break;
                    case "*":
                        previousResult *= secondOperand;
                        break;
                    case "/":
                        if (secondOperand != 0)
                            previousResult /= secondOperand;
                        else
                            previousResult = double.NaN; // Обработка деления на ноль
                        break;
                }
                currentNumber.Clear();
                lastOperation = "";
                history.Clear();
            }
        }

        public void AppendDecimalPoint()
        {
            if (!currentNumber.ToString().Contains("."))
            {
                currentNumber.Append(".");
            }
        }

        public void Clear()
        {
            currentNumber.Clear();
        }

        public void ClearAll()
        {
            currentNumber.Clear();
            history.Clear();
            lastOperation = "";
            result = 0;
        }

        public void RemoveLastDigit()
        {
            if (currentNumber.Length > 0)
            {
                currentNumber.Remove(currentNumber.Length - 1, 1);
            }
        }

        public string GetHistory()
        {
            return history.ToString();
        }

        public string GetCurrentNumber()
        {
            if (currentNumber.Length == 0)
            {
                return previousResult.ToString();
            }
            return currentNumber.ToString();
        }
        public bool HasPendingOperation()
        {
            return !string.IsNullOrEmpty(lastOperation);
        }

    }
}
