using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator_Repository
{
    public class Calculation
    {
        public List<string> Constants { get; set; }
        public List<char> Operators { get; set; }

        public Calculation()
        {
            Constants = new List<string>();
            Operators = new List<char>();
        }

        public Calculation(List<string> constants, List<char> operators)
        {
            Constants = constants;
            Operators = operators;
        }
    }
}
