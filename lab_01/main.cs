using System;

namespace BaseCSharpTasks
{
  class Program
  {
    static void Main(string[] args) {
        // Task 1: Power calculation using only multiplication

        int baseNumber;
        int powerValue;
        long powerResult;

        Console.Write("Enter base number (a): ");
        baseNumber = int.Parse(Console.ReadLine());

        Console.Write("Enter power value (n): ");
        powerValue = int.Parse(Console.ReadLine());

        // Calculations
        powerResult = 1;
        int multiplicationIndex = 0;

        while (multiplicationIndex < powerValue) {
            powerResult = powerResult * baseNumber;
            multiplicationIndex = multiplicationIndex + 1;
        }

        // Output
        Console.WriteLine();
        Console.WriteLine("Result of a^n calculation:");
        Console.WriteLine(baseNumber + "^" + powerValue + " = " + powerResult);
        Console.WriteLine();
    }
  }
}
