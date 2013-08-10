using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleParser
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            Symbol f = SimpleFormulaParser.ParseFormula(" (1+3)  ");
            SimpleFormulaParser.DumpSymbolRecursive(sb, f, 0);
            Console.WriteLine(sb.ToString());
        }
    }
}
