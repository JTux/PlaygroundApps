using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Repository
{
    public class CalculatorRepository
    {
        public decimal RunCalculation(Calculation c)
        {
            var updatedConstants = new List<string>();
            void Reset(char[] op)
            {
                c.Operators.RemoveAll(o => o == op[0] || o == op[1]);
                c.Constants = updatedConstants;
                updatedConstants = new List<string>();
            }

            foreach (var operatorTypes in new char[][] { new char[] { '*', '/' }, new char[] { '+', '-' } })
            {
                if (c.Operators.Contains(operatorTypes[0]) || c.Operators.Contains(operatorTypes[1]))
                {
                    updatedConstants = Operate(c, operatorTypes);
                    Reset(operatorTypes);
                }
            }
            return decimal.Parse(c.Constants[0]);
        }

        private List<string> Operate(Calculation c, char[] operationTypes)
        {
            while (c.Operators.Contains(operationTypes[0]) || c.Operators.Contains(operationTypes[1]))
            {
                int i = c.Operators.FindIndex(o => o == operationTypes[0] || o == operationTypes[1]);
                if (c.Operators[i] == operationTypes[0] || c.Operators[i] == operationTypes[1])
                {
                    var calculatedValue = 0m;
                    switch (c.Operators[i])
                    {
                        case '*':
                            calculatedValue = decimal.Parse(c.Constants[i]) * decimal.Parse(c.Constants[i + 1]);
                            break;
                        case '/':
                            calculatedValue = decimal.Parse(c.Constants[i]) / decimal.Parse(c.Constants[i + 1]);
                            break;
                        case '+':
                            calculatedValue = decimal.Parse(c.Constants[i]) + decimal.Parse(c.Constants[i + 1]);
                            break;
                        case '-':
                            calculatedValue = decimal.Parse(c.Constants[i]) - decimal.Parse(c.Constants[i + 1]);
                            break;
                    }

                    c.Constants[i] = calculatedValue.ToString();
                    c.Constants.RemoveAt(i + 1);
                    c.Operators.RemoveAt(i);
                }
            }

            return c.Constants;
        }
    }
}
