using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleParser
{
    public abstract class Symbol
    {
        public List<Symbol> ConstituentSymbols { get; set; }
        
        public override string ToString()
        {
            string s = ConstituentSymbols.Select(ct => ct.ToString()).StringConcatenate();
            return s;
        }
        
        public Symbol(params Object[] symbols)
        {
            List<Symbol> ls = new List<Symbol>();
            foreach (var item in symbols)
            {
                if (item is Symbol)
                    ls.Add((Symbol)item);
                else if (item is IEnumerable<Symbol>)
                    foreach (var item2 in (IEnumerable<Symbol>)item)
                        ls.Add(item2);
                else
                    // If this error is thrown, the parser is coded incorrectly.
                    throw new ParserException("Internal error");
            }
            ConstituentSymbols = ls;
        }
        
        public Symbol() { }
    }

    public class Formula : Symbol
    {
        public static Formula Produce(IEnumerable<Symbol> symbols)
        {
            // formula = expression
 
            Expression e = Expression.Produce(symbols);
            return e == null ? null : new Formula(e);
        }
        public Formula(params Object[] symbols) : base(symbols) { }
    }
 
    public class Expression : Symbol
    {
        public static Expression Produce(IEnumerable<Symbol> symbols)
        {
            // expression = *whitespace nospace-expression *whitespace
 
            int whiteSpaceBefore = symbols.TakeWhile(s => s is WhiteSpace).Count();
            int whiteSpaceAfter = symbols.Reverse().TakeWhile(s => s is WhiteSpace).Count();
            IEnumerable<Symbol> noSpaceSymbolList = symbols
                .Skip(whiteSpaceBefore)
                .SkipLast(whiteSpaceAfter)
                .ToList();
            NospaceExpression n = NospaceExpression.Produce(noSpaceSymbolList);
            if (n != null)
                return new Expression(
                    Enumerable.Repeat(new WhiteSpace(), whiteSpaceBefore),
                    n,
                    Enumerable.Repeat(new WhiteSpace(), whiteSpaceAfter));
            return null;
        }
 
        public Expression (params Object[] symbols) : base(symbols) { }
    }
 
    public class NospaceExpression : Symbol
    {
        public static NospaceExpression Produce(IEnumerable<Symbol> symbols)
        {
            return new NospaceExpression(symbols);
        }
 
        public NospaceExpression(params Object[] symbols) : base(symbols) { }
    }
 
    public class DecimalDigit : Symbol
    {
        private string CharacterValue;
        public override string ToString() { return CharacterValue; }
        public DecimalDigit(char c) { CharacterValue = c.ToString(); }
    }
 
    public class WhiteSpace : Symbol
    {
        public override string ToString() { return " "; }
        public WhiteSpace() { }
    }
 
    public class Plus : Symbol
    {
        public override string ToString() { return "+"; }
        public Plus() { }
    }
 
    public class Asterisk : Symbol
    {
        public override string ToString() { return "*"; }
        public Asterisk() { }
    }
 
    public class Assignment : Symbol
    {
        public override string ToString() { return "="; }
        public Assignment() { }
    }
 
    public class FullStop : Symbol
    {
        public override string ToString() { return "."; }
        public FullStop() { }
    }
 
    public class OpenParenthesis : Symbol
    {
        public override string ToString() { return "("; }
        public OpenParenthesis() { }
    }
 
    public class CloseParenthesis : Symbol
    {
        public override string ToString() { return ")"; }
        public CloseParenthesis() { }
    }
 
}

