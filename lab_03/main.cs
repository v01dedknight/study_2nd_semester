using System;
using System.Text;

namespace MatrixCalculator {
  // CUSTOM EXCEPTIONS BLOCK
  public class MatrixException : Exception {
    public MatrixException(string message) : base(message) { }
  }

  public class SizeMismatchException : MatrixException {
    public SizeMismatchException(string message) : base(message) { }
  }

  public class SingularMatrixException : MatrixException {
    public SingularMatrixException(string message) : base(message) { }
  }

  // PROTOTYPE PATTERN INTERFACE
  public interface IMyCloneable {
    object Clone();
  }

  public class SquareMatrix : IMyCloneable, IComparable<SquareMatrix> {
    // DECLARATION BLOCK
    private double[,] matrixData;
    private int matrixSize;
    private const double DOUBLE_ZERO = 0.0;
    private const double EPSILON = 1e-9;

    // CONSTRUCTORS
    public SquareMatrix(int size) {
      if (size <= 0) {
        throw new MatrixException("Size must be greater than zero.");
      }

      matrixSize = size;
      matrixData = new double[size, size];
    }

    public SquareMatrix(int size, int minValue, int maxValue) : this(size) {
      // DECLARATION BLOCK
      Random randomGenerator;
      randomGenerator = new Random();

      // CALCULATION BLOCK
      for (int rowIndex = 0; rowIndex < size; rowIndex++) {
        for (int columnIndex = 0; columnIndex < size; columnIndex++) {
          matrixData[rowIndex, columnIndex] = (double)randomGenerator.Next(minValue, maxValue);
        }
      }
    }

    // PROPERTIES
    public int Size {
      get {
        return matrixSize;
      }
    }

    // INDEXER
    public double this[int rowIndex, int columnIndex] {
      get {
        return matrixData[rowIndex, columnIndex];
      }
      set {
        matrixData[rowIndex, columnIndex] = value;
      }
    }

    // OPERATOR OVERLOADING
    public static SquareMatrix operator +(SquareMatrix firstMatrix, SquareMatrix secondMatrix) {
      if (firstMatrix.Size != secondMatrix.Size) {
        throw new SizeMismatchException("Matrices must have the same size.");
      }

      // DECLARATION BLOCK
      int size;
      SquareMatrix resultMatrix;

      size = firstMatrix.Size;
      resultMatrix = new SquareMatrix(size);

      // CALCULATION BLOCK
      for (int rowIndex = 0; rowIndex < size; rowIndex++) {
        for (int columnIndex = 0; columnIndex < size; columnIndex++) {
          resultMatrix[rowIndex, columnIndex] = firstMatrix[rowIndex, columnIndex] + secondMatrix[rowIndex, columnIndex];
        }
      }

      return resultMatrix;
    }

    public static SquareMatrix operator *(SquareMatrix firstMatrix, SquareMatrix secondMatrix) {
      if (firstMatrix.Size != secondMatrix.Size) {
        throw new SizeMismatchException("Matrices must have the same size.");
      }

      // DECLARATION BLOCK
      int size;
      SquareMatrix resultMatrix;

      size = firstMatrix.Size;
      resultMatrix = new SquareMatrix(size);

      // CALCULATION BLOCK
      for (int rowIndex = 0; rowIndex < size; rowIndex++) {
        for (int columnIndex = 0; columnIndex < size; columnIndex++) {
          for (int innerIndex = 0; innerIndex < size; innerIndex++) {
            resultMatrix[rowIndex, columnIndex] += firstMatrix[rowIndex, innerIndex] * secondMatrix[innerIndex, columnIndex];
          }
        }
      }

      return resultMatrix;
    }

    // BOOLEAN OPERATORS (Using Determinant for Comparison)
    public static bool operator >(SquareMatrix firstMatrix, SquareMatrix secondMatrix) {
      return firstMatrix.GetDeterminant() > secondMatrix.GetDeterminant();
    }

    public static bool operator <(SquareMatrix firstMatrix, SquareMatrix secondMatrix) {
      return firstMatrix.GetDeterminant() < secondMatrix.GetDeterminant();
    }

    public static bool operator >=(SquareMatrix firstMatrix, SquareMatrix secondMatrix) {
      return firstMatrix.GetDeterminant() >= secondMatrix.GetDeterminant();
    }

    public static bool operator <=(SquareMatrix firstMatrix, SquareMatrix secondMatrix) {
      return firstMatrix.GetDeterminant() <= secondMatrix.GetDeterminant();
    }

    public static bool operator ==(SquareMatrix firstMatrix, SquareMatrix secondMatrix) {
      // DECLARATION BLOCK
      bool isReferenceEqual;
      bool isSizeDifferent;

      isReferenceEqual = ReferenceEquals(firstMatrix, secondMatrix);
      if (isReferenceEqual) {
        return true;
      }

      if (firstMatrix is null || secondMatrix is null) {
        return false;
      }

      isSizeDifferent = firstMatrix.Size != secondMatrix.Size;
      if (isSizeDifferent) {
        return false;
      }

      // CALCULATION BLOCK
      for (int rowIndex = 0; rowIndex < firstMatrix.Size; rowIndex++) {
        for (int columnIndex = 0; columnIndex < firstMatrix.Size; columnIndex++) {
          if (Math.Abs(firstMatrix[rowIndex, columnIndex] - secondMatrix[rowIndex, columnIndex]) > EPSILON) {
            return false;
          }
        }
      }

      return true;
    }

    public static bool operator !=(SquareMatrix firstMatrix, SquareMatrix secondMatrix) {
      return !(firstMatrix == secondMatrix);
    }

    // TRUE/FALSE OPERATORS (True if non-singular)
    public static bool operator true(SquareMatrix matrix) {
      return Math.Abs(matrix.GetDeterminant()) > EPSILON;
    }

    public static bool operator false(SquareMatrix matrix) {
      return Math.Abs(matrix.GetDeterminant()) <= EPSILON;
    }

    // TYPE CASTING (Explicitly to double returns determinant)
    public static explicit operator double(SquareMatrix matrix) {
      return matrix.GetDeterminant();
    }

    // LOGIC METHODS
    public double GetDeterminant() {
      return CalculateDeterminantRecursive(this.matrixData, this.matrixSize);
    }

    private double CalculateDeterminantRecursive(double[,] data, int size) {
      if (size == 1) {
        return data[0, 0];
      }

      // DECLARATION BLOCK
      double determinantResult;
      double sign;
      double minorDeterminant;
      double[,] minorData;

      determinantResult = DOUBLE_ZERO;

      // CALCULATION BLOCK
      for (int columnIndex = 0; columnIndex < size; columnIndex++) {
        sign = Math.Pow(-1.0, (double)columnIndex);
        minorData = GetMinor(data, 0, columnIndex, size);
        minorDeterminant = CalculateDeterminantRecursive(minorData, size - 1);
        determinantResult += sign * data[0, columnIndex] * minorDeterminant;
      }

      return determinantResult;
    }

    private double[,] GetMinor(double[,] data, int excludedRow, int excludedColumn, int size) {
      // DECLARATION BLOCK
      double[,] minorData;
      int minorRow;
      int minorColumn;

      minorData = new double[size - 1, size - 1];
      minorRow = 0;

      // CALCULATION BLOCK
      for (int rowIndex = 0; rowIndex < size; rowIndex++) {
        if (rowIndex == excludedRow) {
          continue;
        }

        minorColumn = 0;
        for (int columnIndex = 0; columnIndex < size; columnIndex++) {
          if (columnIndex == excludedColumn) {
            continue;
          }

          minorData[minorRow, minorColumn] = data[rowIndex, columnIndex];
          minorColumn++;
        }
        minorRow++;
      }

      return minorData;
    }

    public SquareMatrix GetInverse() {
      // DECLARATION BLOCK
      double determinant;
      SquareMatrix adjugateMatrix;
      double[,] minor;
      double cofactor;

      determinant = GetDeterminant();

      // CHECK BLOCK
      if (Math.Abs(determinant) < EPSILON) {
        throw new SingularMatrixException("Matrix is singular, inverse does not exist.");
      }

      adjugateMatrix = new SquareMatrix(matrixSize);

      // CALCULATION BLOCK
      for (int rowIndex = 0; rowIndex < matrixSize; rowIndex++) {
        for (int columnIndex = 0; columnIndex < matrixSize; columnIndex++) {
          minor = GetMinor(this.matrixData, rowIndex, columnIndex, matrixSize);
          cofactor = Math.Pow(-1.0, (double)(rowIndex + columnIndex)) * CalculateDeterminantRecursive(minor, matrixSize - 1);
          // Transpose for adjugate
          adjugateMatrix[columnIndex, rowIndex] = cofactor / determinant;
        }
      }

      return adjugateMatrix;
    }

    // OVERRIDES
    public override string ToString() {
      // DECLARATION BLOCK
      StringBuilder stringBuilder;
      stringBuilder = new StringBuilder();

      // CALCULATION BLOCK
      for (int rowIndex = 0; rowIndex < matrixSize; rowIndex++) {
        for (int columnIndex = 0; columnIndex < matrixSize; columnIndex++) {
          stringBuilder.Append($"{matrixData[rowIndex, columnIndex],8:F2} ");
        }
        stringBuilder.AppendLine();
      }

      return stringBuilder.ToString();
    }

    public int CompareTo(SquareMatrix other) {
      if (other == null) {
        return 1;
      }

      return this.GetDeterminant().CompareTo(other.GetDeterminant());
    }

    public override bool Equals(object obj) {
      return obj is SquareMatrix matrix && this == matrix;
    }

    public override int GetHashCode() {
      return matrixData.GetHashCode() ^ matrixSize.GetHashCode();
    }

    // PROTOTYPE PATTERN (Deep Copy)
    public object Clone() {
      // DECLARATION BLOCK
      SquareMatrix clonedMatrix;
      clonedMatrix = new SquareMatrix(this.matrixSize);

      // CALCULATION BLOCK
      for (int rowIndex = 0; rowIndex < matrixSize; rowIndex++) {
        for (int columnIndex = 0; columnIndex < matrixSize; columnIndex++) {
          clonedMatrix[rowIndex, columnIndex] = this.matrixData[rowIndex, columnIndex];
        }
      }

      return clonedMatrix;
    }
  }

  // TEST APPLICATION
  class Program {
    static void Main(string[] args) {
      try {
        // DECLARATION BLOCK
        int matrixSize;
        SquareMatrix firstMatrix;
        SquareMatrix secondMatrix;
        SquareMatrix sumResult;
        int loopCounter;
        const int MAX_ITERATIONS = 2;

        // INPUT BLOCK
        Console.WriteLine("=== Matrix Calculator ===");
        Console.Write("Enter matrix size: ");
        matrixSize = int.Parse(Console.ReadLine());

        // INITIALIZATION
        firstMatrix = new SquareMatrix(matrixSize, 1, 10);
        secondMatrix = new SquareMatrix(matrixSize, 1, 5);

        // OUTPUT BLOCK 1
        Console.WriteLine("\nMatrix A:");
        Console.WriteLine(firstMatrix.ToString());
        Console.WriteLine("Matrix B:");
        Console.WriteLine(secondMatrix.ToString());

        // CALCULATION BLOCK
        sumResult = firstMatrix + secondMatrix;

        // OUTPUT BLOCK 2
        Console.WriteLine("Sum A + B:");
        Console.WriteLine(sumResult.ToString());
        Console.WriteLine($"Determinant of A: {firstMatrix.GetDeterminant():F2}");

        // DEMONSTRATING DIFFERENT LOOPS (Rule 11 Adaptation)
        loopCounter = 0;

        // While loop demonstration
        Console.WriteLine("Processing iterations (While):");
        while (loopCounter < MAX_ITERATIONS) {
          Console.WriteLine($"Iteration step: {loopCounter}");
          loopCounter++;
        }

        // Do-while loop demonstration
        loopCounter = 0;
        Console.WriteLine("\nProcessing iterations (Do-While):");
        do {
          Console.WriteLine($"Step recorded: {loopCounter}");
          loopCounter++;
        } while (loopCounter < MAX_ITERATIONS);

      } catch (MatrixException matrixError) {
        Console.WriteLine($"Matrix error occurred: {matrixError.Message}");
      } catch (Exception generalError) {
        Console.WriteLine($"System error: {generalError.Message}");
      }

      Console.WriteLine("\nPress any key to exit...");
      Console.ReadKey();
    }
  }
}