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

        public Calculator()
        {
            currentNumber = new StringBuilder();
            history = new StringBuilder();
            lastOperation = "";
            result = 0;
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
                if (lastOperation != "")
                {
                    Calculate();
                }
                lastOperation = operation;
                result = double.Parse(currentNumber.ToString());
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
                switch (lastOperation)
                {
                    case "+":
                        result += secondOperand;
                        break;
                    case "-":
                        result -= secondOperand;
                        break;
                    case "*":
                        result *= secondOperand;
                        break;
                    case "/":
                        if (secondOperand != 0)
                            result /= secondOperand;
                        else
                            result = double.NaN; // Обработка деления на ноль
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
                return result.ToString();
            }
            return currentNumber.ToString();
        }
    }
}
