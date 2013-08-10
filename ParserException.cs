using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleParser
{
    public class ParserException :Exception
    {
        public ParserException(string message) : base(message) { }
    }
}
