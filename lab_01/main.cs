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

        // Task 2: Remove second digit and append it to the end

        // Variable declaration
        string inputNumber;
        char secondDigit;
        string numberWithoutSecondDigit;
        string finalNumber;

        // Input
        Console.Write("Enter number x (x >= 100): ");
        inputNumber = Console.ReadLine();

        // Calculations
        // Extract second digit
        secondDigit = inputNumber[1];

        // Remove second digit
        numberWithoutSecondDigit = inputNumber.Remove(1, 1);

        // Append removed digit to the end
        finalNumber = numberWithoutSecondDigit + secondDigit;

        // Block: output
        Console.WriteLine();
        Console.WriteLine("Result after transformation:");
        Console.WriteLine("n = " + finalNumber);

        Console.ReadLine();
    }
  }
}
