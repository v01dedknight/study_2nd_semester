# Laboratory Work — Basic Concepts of C#

## Option

None

## Description

This console application is written in C# and implements two independent tasks demonstrating basic language concepts, console input/output, loops, string processing, and step-by-step computations without using complex arithmetic operations.

The program contains:

1. Power calculation using only multiplication.
2. Digit manipulation in an integer number using string processing.

## Author

Me

## Objective

Learning the basic concepts of the C# programming language:

* console input/output
* variables and data types
* loops
* step-by-step calculations
* string manipulation
* program structure and logical blocks

## Tasks Description

### Task 1

Calculate the value of a^n, where:

* a is a natural number
* n is a natural number

Restrictions:

* Only the multiplication operation is allowed
* No addition, subtraction, division, modulo, or built-in power functions

### Task 2

Given a number x (x ≥ 100):

* the second digit of the number is removed
* the removed digit is appended to the end of the number

As a result, a new number n is formed.

Example:

x = 121111
n = 111112

## Input

### Task 1

The user enters:

* base number a
* power value n

### Task 2

The user enters:

* integer number x (x ≥ 100)

## Output

### Task 1

The program outputs:

* the calculated value of a^n

### Task 2

The program outputs:

* the transformed number n after digit manipulation

## Program Structure

The program is structured into logical blocks:

* variable declaration
* input block
* calculation block
* output block

Each task is implemented sequentially in a single console application.

## Execution

To run the program:

1. Build the project:

```bash
dotnet build
```

2. Run the program:

```bash
dotnet run
```

## Expected Behavior

The program requests user input from the console and displays the results of both tasks sequentially in the terminal window.
