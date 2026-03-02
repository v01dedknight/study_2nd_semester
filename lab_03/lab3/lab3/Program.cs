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

  class Program {
    static void Main(string[] args) {
      // INITIALIZATION BLOCK
      Console.WriteLine("System: Infrastructure initialized.");
    }
  }
}