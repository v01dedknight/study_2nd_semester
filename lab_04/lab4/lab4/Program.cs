using System;
using System.Collections.Generic;

namespace TextEditorLab {
  // Base class to represent a text file
  public class TextDocument {
    public string fileName { get; set; }
    public string content { get; set; }

    public TextDocument() {
      fileName = "Untitled.txt";
      content = string.Empty;
    }
  }

  class Program {
    static void Main(string[] args) {
      bool isRunning;
      string userChoice;

      isRunning = true;

      while (isRunning) {
        Console.Clear();
        Console.WriteLine("=== TEXT EDITOR v1.0 ===");
        Console.WriteLine("1. Open Editor");
        Console.WriteLine("0. Exit");
        Console.Write("Choice: ");

        userChoice = Console.ReadLine();

        if (userChoice == "1") {
          Console.WriteLine("Editor logic will be here...");
          Console.ReadLine();
        } else if (userChoice == "0") {
          isRunning = false;
        }
      }
    }
  }
}