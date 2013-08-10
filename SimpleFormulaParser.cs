using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleParser
{
    public class SimpleFormulaParser
    {
        public static Symbol ParseFormula(string s)
        {
            IEnumerable<Symbol> symbols = s.Select(c =>
            {
                switch (c)
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
                        return new DecimalDigit(c);
                    case ' ':
                        return new WhiteSpace();
                    case '+':
                        return new Plus();
                    case '*':
                        return new Asterisk();
                    case '=':
                        return new Assignment();
                    case '.':
                        return new FullStop();
                    case '(':
                        return new OpenParenthesis();
                    case ')':
                        return new CloseParenthesis();
                    default:
                        return (Symbol)null;
                }
            });
#if true
            if (symbols.Any())
            {
                Console.WriteLine("Terminal Symbols");
                Console.WriteLine("================");
                foreach (var terminal in symbols)
                    Console.WriteLine("{0} >{1}<", terminal.GetType().Name.ToString(),
                        terminal.ToString());
                Console.WriteLine();
            }
#endif
            Formula formula = Formula.Produce(symbols);
            if (formula == null)
                throw new ParserException("Invalid formula");
            return formula;
        }

        public static void DumpSymbolRecursive(StringBuilder sb, Symbol symbol, int depth)
        {
            sb.Append(string.Format("{0}{1} >{2}<",
                "".PadRight(depth * 2),
                symbol.GetType().Name.ToString(),
                symbol.ToString())).Append(Environment.NewLine);
            if (symbol.ConstituentSymbols != null)
                foreach (var childSymbol in symbol.ConstituentSymbols)
                    DumpSymbolRecursive(sb, childSymbol, depth + 1);
        }
    }
}
