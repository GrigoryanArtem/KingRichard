using CalculatorLexicalScanner;
using CalculatorSyntaxScanner;
using System;

namespace KingRichard.Calculator
{
    public static class RichardCalculator
    {
        public static double Calculate(string expression)
        {
            Scanner scanner = new Scanner();
            scanner.SetSource(expression, 0);

            Parser parser = new Parser(scanner);

            if (!parser.Parse())
                throw new Exception();

            return parser.Result;
        }
    }
}
