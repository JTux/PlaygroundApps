using System;
using System.Collections.Generic;

namespace Calculator
{
    public class ProgramUI
    {
        public ProgramUI()
        {
        }

        public void Run()
        {
            List<string> constants = new List<string>();
            List<char> operators = new List<char>();
            string equationPart = "";
            while (true)
            {
                void PrintParts()
                {
                    Console.Clear();
                    PrintEquation(constants, operators);
                }

                Console.CursorVisible = false;
                var key = Console.ReadKey(true).KeyChar;

                if (key == '\r')
                    key = '=';

                if (IsOperator(key))
                {
                    if (equationPart == "")
                        continue;

                    operators.Add(key);
                    constants.Add(equationPart);
                    equationPart = "";

                    PrintEquation(constants, operators);

                    if (constants.Count != 0 && key == '=')
                    {
                        Console.Clear();
                        Calculate(constants, operators);
                        constants = new List<string>();
                        operators = new List<char>();
                    }
                }
                else
                {
                    if (IsNumber(key))
                        equationPart += key;
                    else if (key == '\b' && equationPart.Length > 0)
                    {
                        equationPart = equationPart.Substring(0, equationPart.Length - 1);
                    }

                    PrintParts();
                    Console.Write(equationPart);
                }
            }
        }

        private void PrintEquation(List<string> equation, List<char> operators)
        {
            Console.Clear();
            for (int i = 0; i < equation.Count; i++)
            {
                Console.Write($"{equation[i]} {operators[i]} ");
            }
        }

        private decimal Calculate(List<string> constantList, List<char> operatorList)
        {
            PrintEquation(constantList, operatorList);
            Console.WriteLine($"{RunCalculation(constantList, operatorList)}");
            return 0;
        }

        private decimal RunCalculation(List<string> constants, List<char> ops)
        {
            var updatedConstants = new List<string>();

            void Reset(char op)
            {
                ops.RemoveAll(o => o == op);
                constants = updatedConstants;
                updatedConstants = new List<string>();
            }

            foreach (var op in new char[] { '*', '/', '+', '-' })
            {
                if (ops.Contains(op))
                {
                    updatedConstants = Operate(constants, ops, op);
                    Reset(op);
                }
            }
            return decimal.Parse(constants[0]);
        }

        private List<string> Operate(List<string> constants, List<char> operators, char operationType)
        {
            while (operators.Contains(operationType))
            {
                int i = operators.FindIndex(o => o == operationType);
                if (operators[i] == operationType)
                {
                    var calc = 0m;
                    switch (operationType)
                    {
                        case '*':
                            calc = decimal.Parse(constants[i]) * decimal.Parse(constants[i + 1]);
                            break;
                        case '/':
                            calc = decimal.Parse(constants[i]) / decimal.Parse(constants[i + 1]);
                            break;
                        case '+':
                            calc = decimal.Parse(constants[i]) + decimal.Parse(constants[i + 1]);
                            break;
                        case '-':
                            calc = decimal.Parse(constants[i]) - decimal.Parse(constants[i + 1]);
                            break;
                    }

                    constants[i] = calc.ToString();
                    constants.RemoveAt(i + 1);
                    operators.RemoveAt(i);
                }
            }

            return constants;
        }

        private bool IsNumber(char key)
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

        private bool IsOperator(char key)
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