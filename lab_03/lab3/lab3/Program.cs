using System;
using System.Text;

namespace MatrixCalculator {
  public class MatrixException : Exception {
    public MatrixException(string message) : base(message) { }
  }

  public class SizeMismatchException : MatrixException {
    public SizeMismatchException(string message) : base(message) { }
  }

  public class SingularMatrixException : MatrixException {
    public SingularMatrixException(string message) : base(message) { }
  }

  public interface IMyCloneable {
    object Clone();
  }

  public class SquareMatrix : IMyCloneable, IComparable<SquareMatrix> {
    private double[,] matrixData;
    private int matrixSize;
    private const double EPSILON = 1e-9;

    public SquareMatrix(int size) {
      if (size <= 0) throw new MatrixException("Invalid size.");
      matrixSize = size;
      matrixData = new double[size, size];
    }

    public SquareMatrix(int size, int minValue, int maxValue) : this(size) {
      Random randomGenerator = new Random();
      for (int rowIndex = 0; rowIndex < size; rowIndex++) {
        for (int columnIndex = 0; columnIndex < size; columnIndex++) {
          matrixData[rowIndex, columnIndex] = (double)randomGenerator.Next(minValue, maxValue);
        }
      }
    }

    public int Size { get { return matrixSize; } }

    public double this[int rowIndex, int columnIndex] {
      get { return matrixData[rowIndex, columnIndex]; }
      set { matrixData[rowIndex, columnIndex] = value; }
    }

    // OPERATOR OVERLOADING
    public static SquareMatrix operator +(SquareMatrix first, SquareMatrix second) {
      if (first.Size != second.Size) throw new SizeMismatchException("Size mismatch.");
      SquareMatrix result = new SquareMatrix(first.Size);
      for (int rowIndex = 0; rowIndex < first.Size; rowIndex++)
        for (int columnIndex = 0; columnIndex < first.Size; columnIndex++)
          result[rowIndex, columnIndex] = first[rowIndex, columnIndex] + second[rowIndex, columnIndex];
      return result;
    }

    public static SquareMatrix operator *(SquareMatrix first, SquareMatrix second) {
      if (first.Size != second.Size) throw new SizeMismatchException("Size mismatch.");
      SquareMatrix result = new SquareMatrix(first.Size);
      for (int rowIndex = 0; rowIndex < first.Size; rowIndex++)
        for (int columnIndex = 0; columnIndex < first.Size; columnIndex++)
          for (int innerIndex = 0; innerIndex < first.Size; innerIndex++)
            result[rowIndex, columnIndex] += first[rowIndex, innerIndex] * second[innerIndex, columnIndex];
      return result;
    }

    public static bool operator >(SquareMatrix first, SquareMatrix second) => first.GetDeterminant() > second.GetDeterminant();
    public static bool operator <(SquareMatrix first, SquareMatrix second) => first.GetDeterminant() < second.GetDeterminant();
    public static bool operator ==(SquareMatrix first, SquareMatrix second) {
      if (ReferenceEquals(first, second)) return true;
      if (first is null || second is null) return false;
      for (int rowIndex = 0; rowIndex < first.Size; rowIndex++)
        for (int columnIndex = 0; columnIndex < first.Size; columnIndex++)
          if (Math.Abs(first[rowIndex, columnIndex] - second[rowIndex, columnIndex]) > EPSILON) return false;
      return true;
    }
    public static bool operator !=(SquareMatrix first, SquareMatrix second) => !(first == second);
    public static bool operator true(SquareMatrix matrix) => Math.Abs(matrix.GetDeterminant()) > EPSILON;
    public static bool operator false(SquareMatrix matrix) => Math.Abs(matrix.GetDeterminant()) <= EPSILON;

    // MATH LOGIC
    public double GetDeterminant() {
      return CalculateDeterminantRecursive(this.matrixData, this.matrixSize);
    }

    private double CalculateDeterminantRecursive(double[,] data, int size) {
      if (size == 1) return data[0, 0];
      double res = 0.0;
      for (int col = 0; col < size; col++) {
        double sign = Math.Pow(-1.0, (double)col);
        res += sign * data[0, col] * CalculateDeterminantRecursive(GetMinor(data, 0, col, size), size - 1);
      }
      return res;
    }

    private double[,] GetMinor(double[,] data, int excludedRow, int excludedColumn, int size) {
      double[,] minor = new double[size - 1, size - 1];
      int mRow = 0;
      for (int r = 0; r < size; r++) {
        if (r == excludedRow) continue;
        int mCol = 0;
        for (int c = 0; c < size; c++) {
          if (c == excludedColumn) continue;
          minor[mRow, mCol] = data[r, c];
          mCol++;
        }
        mRow++;
      }
      return minor;
    }

    public int CompareTo(SquareMatrix other) => this.GetDeterminant().CompareTo(other?.GetDeterminant() ?? 0.0);
    public object Clone() {
      SquareMatrix copy = new SquareMatrix(this.matrixSize);
      for (int r = 0; r < matrixSize; r++)
        for (int c = 0; c < matrixSize; c++)
          copy[r, c] = this.matrixData[r, c];
      return copy;
    }
  }

  class Program { static void Main(string[] args) { } }
}