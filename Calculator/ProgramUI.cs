using Calculator_Repository;
using System;
using System.Collections.Generic;

namespace Calculator
{
    public class ProgramUI
    {
        private CalculatorRepository _calculatorRepo;

        public ProgramUI()
        {
            _calculatorRepo = new CalculatorRepository();
            Console.CursorVisible = false;
        }

        public void Run()
        {
            Calculation calc = new Calculation();
            bool continuing = false;
            decimal previousValue = 0m;

            string newNum = "";
            while (true)
            {
                var key = Console.ReadKey(true).KeyChar;

                if (key == '\r')
                    key = '=';

                if (CheckIsOperator(key))
                {
                    if (continuing)
                    {
                        calc.Constants.Add(previousValue.ToString());
                        calc.Operators.Add(key);
                        continuing = false;
                        PrintEquation(calc);
                        continue;
                    }
                    continuing = false;

                    if (newNum == "")
                        continue;

                    calc.Operators.Add(key);
                    calc.Constants.Add(newNum);
                    newNum = "";

                    PrintEquation(calc);

                    if (calc.Constants.Count != 0 && key == '=')
                    {
                        previousValue = _calculatorRepo.RunCalculation(calc);
                        PrintEquation(calc);
                        Console.WriteLine($"{previousValue}");
                        calc = new Calculation();
                        continuing = true;
                    }
                }
                else
                {
                    continuing = false;
                    if (CheckIsNumber(key) && newNum.Length <= 15)
                        newNum += key;
                    else if (key == '\b')
                    {
                        if (newNum.Length > 0)
                        {
                            newNum = newNum.Substring(0, newNum.Length - 1);
                        }
                        else if (calc.Constants.Count > 0)
                        {
                            newNum = calc.Constants[calc.Constants.Count - 1];
                            calc.Operators.RemoveAt(calc.Operators.Count - 1);
                            calc.Constants.RemoveAt(calc.Constants.Count - 1);
                        }
                    }

                    PrintEquation(calc);
                    Console.Write(newNum);
                }
            }
        }

        private void PrintEquation(Calculation c)
        {
            Console.Clear();
            for (int i = 0; i < c.Constants.Count; i++)
                Console.Write($"{c.Constants[i]} {c.Operators[i]} ");
        }

        private bool CheckIsNumber(char key)
        {
            switch (key)
            {
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    return true;
                default:
                    return false;
            }
        }
        private bool CheckIsOperator(char key)
        {
            switch (key)
            {
                case '+':
                case '-':
                case '/':
                case '*':
                case '=':
                    return true;
                default:
                    return false;
            }
        }
    }
}