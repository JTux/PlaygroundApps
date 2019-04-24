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
            Queue<string> equation = new Queue<string>();
            Queue<char> operators = new Queue<char>();
            string equationPart = "";
            while (true)
            {
                var key = Console.ReadKey().KeyChar;

                if (IsOperator(key))
                {
                    if (equationPart == "")
                        continue;

                    operators.Enqueue(key);
                    equation.Enqueue(equationPart);
                    equationPart = "";

                    PrintEquation(equation.ToArray(), operators.ToArray());

                    if (equation.Count != 0 && (key == '=' || key == '\r'))
                    {
                        Console.Clear();
                        Calculate(equation, operators);
                        equation = new Queue<string>();
                        operators = new Queue<char>();
                    }
                }
                else if (IsNumber(key))
                    equationPart += key;
            }
        }

        private void PrintEquation(string[] equation, char[] operators)
        {
            Console.Clear();
            for (int i = 0; i < equation.Length; i++)
            {
                Console.Write($"{equation[i]} {operators[i]} ");
            }
        }

        private decimal Calculate(Queue<string> equation, Queue<char> operators)
        {
            var equationList = new List<string>(equation);
            var operatorList = new List<char>(operators);

            var total = decimal.Parse(equation.Dequeue());
            int iterations = equation.Count;
            Console.Write(total + " ");

            for (int i = 0; i < iterations; i++)
            {
                var number = decimal.Parse(equation.Dequeue());
                var op = operators.Dequeue();
                switch (op)
                {
                    case '+':
                        total += number;
                        break;
                    case '-':
                        total -= number;
                        break;
                    case '/':
                        total /= number;
                        break;
                    case '*':
                        total *= number;
                        break;
                }
                Console.Write($"{op} {number} ");
            }
            Console.WriteLine($"= {RunCalculation(equationList, operatorList)}");
            return total;
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

            if (ops.Contains('*'))
            {
                updatedConstants = Operate(constants, ops, '*');
                Reset('*');
            }
            if (ops.Contains('/'))
            {
                updatedConstants = Operate(constants, ops, '/');
                Reset('/');
            }
            if (ops.Contains('+'))
            {
                updatedConstants = Operate(constants, ops, '+');
                Reset('+');
            }
            if (ops.Contains('-'))
            {
                updatedConstants = Operate(constants, ops, '-');
                Reset('-');
            }
            return decimal.Parse(constants[0]);
        }

        private List<string> Operate(List<string> constants, List<char> operators, char operationType)
        {
            List<string> updatedConstants = new List<string>();
            for (int i = 0; i < operators.Count; i++)
            {
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

                    updatedConstants.Add(calc.ToString());
                    i++;
                }
                else
                {
                    updatedConstants.Add(constants[i]);
                }
            }
            return updatedConstants;
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
                case '\r':
                    return true;
                default:
                    return false;
            }
        }
    }
}