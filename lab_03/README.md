# Laboratory Work №3: Matrix Operations and Operator Overloading

### Author
**Me**

**Variant:** Matrix Calculator System

---

### Objective
Exploration of advanced OOP principles in C#: operator overloading, custom exceptions, and the Prototype design pattern. Implementation of a robust mathematical tool for square matrix manipulations.

---

### Project Structure
* **Program.cs**: Main entry point featuring the "Matrix Calculator" demonstration, input handling, and exception management.
* **lab3.csproj**: Modern SDK-style project file configured for .NET Framework 4.7.2 compatibility.
* **MatrixCalculator.slnx**: Visual Studio solution file for project organization.

---

### Coding Standards Applied
* **Indentation**: Strictly 2 spaces (no tabs).
* **Naming Convention**: lowerCamelCase for variables and local properties.
* **Logic Separation**: Explicit separation between variable declarations and calculation/input blocks.
* **Error Handling**: Use of custom exception classes to handle non-invertible matrices and dimension mismatches.
* **Deep Cloning**: Implementation of the Prototype pattern via a custom `IMyCloneable` interface for independent object copying.

---

### Key Features
* **Operator Overloading**: Support for `+`, `*`, `>`, `<`, `>=`, `<=`, `==`, `!=`.
* **Mathematical Operations**: Determinant calculation and matrix inversion.
* **Type Conversion**: Custom implicit/explicit type casting and `true`/`false` operators.
* **Standard Overrides**: Fully implemented `ToString()`, `CompareTo()`, `Equals()`, and `GetHashCode()`.

---

### How to Run
1.  **Environment**: Ensure you have .NET SDK installed (compatible with .NET Framework 4.7.2 targets).
2.  **Navigate**: Open your terminal in the project folder: `cd lab_03`.
3.  **Clean (Optional)**: `dotnet clean lab3.csproj`.
4.  **Run**: Execute the application using:
    ```powershell
    dotnet run --project lab3.csproj
    ```