# Laboratory Work №2: Object-Oriented Programming in C#

## Author
Me
Variant: Zoo Management System

## Objective
Exploration of core OOP principles: inheritance, encapsulation, and polymorphism. Implementation of the Singleton design pattern and structured data input in C#.

## Project Structure
* **Animals.cs**: Contains the base abstract class `Animal` and derived classes `Mammal` and `Bird`.
* **ZooManager.cs**: Implements the Singleton pattern to manage the animal collection using a `List<Animal>`.
* **Program.cs**: Main entry point featuring a console-based menu and validated data input blocks.

## Coding Standards Applied
* **Indentation**: Strictly 2 spaces (no tabs).
* **Naming Convention**: `lowerCamelCase` for variables and properties.
* **Logic Separation**: Explicit separation between variable declaration and calculation/input blocks.
* **Loops**: Use of classic `for` loops with descriptive indices instead of `foreach` or single-letter iterators.

## How to Run
1. Ensure you have .NET 9.0 SDK installed.
2. Navigate to the project folder: `cd lab_02`.
3. Build the project: `dotnet build`.
4. Run the application: `dotnet run`.