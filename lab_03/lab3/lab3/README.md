\# Laboratory Work №2: Object-Oriented Programming in C#



\## Author

v01dedknight



\*\*Variant\*\*: Matrix Operations and Linear Algebra Tool



---



\## Objective

Exploration of core OOP principles: encapsulation, operator overloading, and polymorphism. Implementation of the Prototype design pattern, custom exception handling, and structured data processing in C#.



---



\## Project Structure

\* \*\*SquareMatrix.cs\*\*: Core logic class. Includes:

&nbsp;   \* Matrix arithmetic (+, \*) and comparison operators (>, <, ==, !=).

&nbsp;   \* Mathematical algorithms for Determinant (recursive) and Inverse Matrix.

&nbsp;   \* Implementation of the Prototype pattern via `IMyCloneable`.

\* \*\*Exceptions\*\*: Custom hierarchy (`MatrixException`, `SizeMismatchException`, `SingularMatrixException`) for robust error handling.

\* \*\*Program.cs\*\*: Main entry point featuring a console-based calculator, validated data input, and demonstration of various loop structures.



---



\## Coding Standards Applied

This project strictly follows a custom C++ adapted style guide for C#:

\* \*\*Indentation\*\*: Strictly 2 spaces (no tabs).

\* \*\*Naming Convention\*\*: `lowerCamelCase` for local variables and descriptive names for indices (e.g., `rowIndex`, `columnIndex`) instead of single-letter iterators like `i` or `j`.

\* \*\*Logic Separation\*\*: Code is organized into explicit blocks:

&nbsp;   \* `// DECLARATION BLOCK`: All variables are declared at the beginning of the scope.

&nbsp;   \* `// CALCULATION BLOCK`: Logical operations and data processing.

&nbsp;   \* `// OUTPUT BLOCK`: Result formatting and console interaction.

\* \*\*Type Safety\*\*: Use of explicit literals for floating-point numbers (e.g., `0.0`, `1.0`) and `EPSILON` constants for precision comparisons.

\* \*\*Control Structures\*\*: Use of classic `while` and `do-while` loops to demonstrate structural flow control.



---



\## How to Run



1\. Ensure you have \*\*.NET 8.0/9.0 SDK\*\* installed.

2\. Navigate to the project folder:

&nbsp;  ```bash

&nbsp;  cd MatrixCalculator

&nbsp;  ```

3\. Build the project:

&nbsp;  ```

&nbsp;   dotnet build

   ```



4\. Run the application:

   ```

&nbsp;   dotnet run

   ```



---



\## Key Features



* Deep Copying: Using the Prototype pattern to clone matrix objects.
* Recursive Math: Determinant calculation using minor expansion.
* Error Resilience: Prevention of operations on incompatible matrix sizes or singular matrices.



---



\### Design Advice:



If you want to upload this to the repository as a "finishing touch", then create a file with the name`.MD` in the root folder of the project and make the last commit:

"git add README.md " -> "git commit -m " Documents: add a README with a description of the project and coding standards"` -> `git push origin dev` (or `main`).

