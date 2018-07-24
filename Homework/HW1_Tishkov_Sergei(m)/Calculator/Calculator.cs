using System;

namespace CalculatorLib
{
    public static class Calculator
    {
        public static double Add(double a, double b) => a + b;

        public static double Subtract(double numeric, double subtrahend) => numeric - subtrahend;

        public static double Multiply(double a, double b) => a * b;

        public static double Divide(double numeric, double divider) => numeric / divider;

        public static double Sqrt(double numeric) => Math.Sqrt(numeric);

        public static double Pow(double numeric, double power) => Math.Pow(numeric, power);
    }
}
