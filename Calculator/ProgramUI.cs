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
            List<string> constants = new List<string>();
            List<char> operators = new List<char>();
            string equationPart = "";
            while (true)
            {
                var key = Console.ReadKey(true).KeyChar;

                if (key == '\r')
                    key = '=';

                if (CheckIsOperator(key))
                {
                    if (equationPart == "")
                        continue;

                    operators.Add(key);
                    constants.Add(equationPart);
                    equationPart = "";

                    PrintEquation(new Calculation(constants, operators));

                    if (constants.Count != 0 && key == '=')
                    {
                        PrintEquation(new Calculation(constants, operators));
                        Console.WriteLine($"{_calculatorRepo.RunCalculation(new Calculation(constants, operators))}");
                        constants = new List<string>();
                        operators = new List<char>();
                    }
                }
                else
                {
                    if (CheckIsNumber(key) && equationPart.Length <= 15)
                        equationPart += key;
                    else if (key == '\b')
                    {
                        if (equationPart.Length > 0)
                        {
                            equationPart = equationPart.Substring(0, equationPart.Length - 1);
                        }
                        else if (constants.Count > 0)
                        {
                            equationPart = constants[constants.Count - 1];
                            operators.RemoveAt(operators.Count - 1);
                            constants.RemoveAt(constants.Count - 1);
                        }
                    }

                    PrintEquation(new Calculation(constants, operators));
                    Console.Write(equationPart);
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