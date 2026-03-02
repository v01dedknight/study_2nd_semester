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

  public class SquareMatrix : IMyCloneable {
    // DECLARATION BLOCK
    private double[,] matrixData;
    private int matrixSize;

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

    public int Size {
      get { return matrixSize; }
    }

    public double this[int rowIndex, int columnIndex] {
      get { return matrixData[rowIndex, columnIndex]; }
      set { matrixData[rowIndex, columnIndex] = value; }
    }

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

  class Program {
    static void Main(string[] args) {
      Console.WriteLine("System: SquareMatrix class added.");
    }
  }
}