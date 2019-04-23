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
            Console.WriteLine($"= {PEMDAS(equationList, operatorList)}");
            return total;
        }

        private decimal PEMDAS(List<string> pemdas, List<char> ops)
        {
            var newList = new List<string>();

            int length = ops.Count;
            decimal total = 0m;
            decimal real = 0m;
            //Multiply
            for (int i = 0; i < length; i++)
            {
                if (ops[i] == '*')
                {
                    var prod = decimal.Parse(pemdas[i]) * decimal.Parse(pemdas[i + 1]);
                    total += prod;
                    newList.Add(prod.ToString());
                    i++;
                }
                else
                {
                    newList.Add(pemdas[i]);
                }
            }
            ops.RemoveAll(o => o == '*');
            real = total;
            total = 0;
            pemdas = newList;
            newList = new List<string>();
            //Divide
            for (int i = 0; i < ops.Count; i++)
            {
                if (ops[i] == '/')
                {
                    var prod = decimal.Parse(pemdas[i]) / decimal.Parse(pemdas[i + 1]);
                    total += prod;
                    newList.Add(prod.ToString());
                    i++;
                }
                else
                {
                    newList.Add(pemdas[i]);
                }
            }
            ops.RemoveAll(o => o == '/');
            real = total;
            total = 0;
            pemdas = newList;
            newList = new List<string>();
            //Add
            for (int i = 0; i < ops.Count; i++)
            {
                if (ops[i] == '+')
                {
                    var prod = decimal.Parse(pemdas[i]) + decimal.Parse(pemdas[i + 1]);
                    total += prod;
                    newList.Add(prod.ToString());
                    i++;
                }
                else
                {
                    newList.Add(pemdas[i]);
                }
            }
            ops.RemoveAll(o => o == '+');
            real = total;
            total = 0;
            pemdas = newList;
            newList = new List<string>();
            //Subtract
            for (int i = 0; i < ops.Count; i++)
            {
                if (ops[i] == '-')
                {
                    var prod = decimal.Parse(pemdas[i]) - decimal.Parse(pemdas[i + 1]);
                    total += prod;
                    newList.Add(prod.ToString());
                    i++;
                }
                else
                {
                    newList.Add(pemdas[i]);
                }
            }
            ops.RemoveAll(o => o == '-');
            return real;
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