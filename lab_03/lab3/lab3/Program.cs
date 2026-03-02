using System;

public double GetDeterminant() {
  return CalculateDeterminantRecursive(this.matrixData, this.matrixSize);
}

private double CalculateDeterminantRecursive(double[,] data, int size) {
  if (size == 1) { return data[0, 0]; }

  // DECLARATION BLOCK
  double determinantResult;
  double sign;
  double[,] minorData;

  determinantResult = 0.0;

  // CALCULATION BLOCK
  for (int columnIndex = 0; columnIndex < size; columnIndex++) {
    sign = Math.Pow(-1.0, (double)columnIndex);
    minorData = GetMinor(data, 0, columnIndex, size);
    determinantResult += sign * data[0, columnIndex] * CalculateDeterminantRecursive(minorData, size - 1);
  }
  return determinantResult;
}

private double[,] GetMinor(double[,] data, int excludedRow, int excludedColumn, int size) {
  double[,] minorData = new double[size - 1, size - 1];
  int minorRow = 0;
  for (int rowIndex = 0; rowIndex < size; rowIndex++) {
    if (rowIndex == excludedRow) continue;
    int minorColumn = 0;
    for (int columnIndex = 0; columnIndex < size; columnIndex++) {
      if (columnIndex == excludedColumn) continue;
      minorData[minorRow, minorColumn] = data[rowIndex, columnIndex];
      minorColumn++;
    }
    minorRow++;
  }
  return minorData;
}