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
}