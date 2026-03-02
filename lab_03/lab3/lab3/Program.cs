using System;

public class SquareMatrix : IMyCloneable, IComparable<SquareMatrix> {
  private double[,] matrixData;
  private int matrixSize;

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

  public int Size { get { return matrixSize; } }

  public double this[int rowIndex, int columnIndex] {
    get { return matrixData[rowIndex, columnIndex]; }
    set { matrixData[rowIndex, columnIndex] = value; }
  }
}