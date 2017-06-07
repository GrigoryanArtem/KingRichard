using System;

namespace KingRichard.Calculator
{
    public static class RichardMath
    {
        public static double Factorial(double x)
        {
            int numberInt = (int)Math.Round(x);
            int result = numberInt;

            for (int i = 1; i < numberInt; i++)
            {
                result = result * i;
            }

            return result;
        }
    }
}
