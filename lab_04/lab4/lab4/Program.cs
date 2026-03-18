using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace TextEditorLab {
  // this class represents the document structure and handles its persistence
  // separating data from logic allows for easier serialization and testing
  public class TextDocument {
    public string fileName { get; set; }
    public string content { get; set; }

    public TextDocument() {
      fileName = "Untitled.txt";
      content = string.Empty;
    }

    public TextDocument(string name, string text) {
      fileName = name;
      content = text;
    }

    // xml serialization provides a human-readable format for saved files
    public void SaveToXml(string path) {
      XmlSerializer serializer;

      serializer = new XmlSerializer(typeof(TextDocument));

      using (FileStream stream = new FileStream(path, FileMode.Create)) {
        serializer.Serialize(stream, this);
      }
    }

    public static TextDocument LoadFromXml(string path) {
      XmlSerializer serializer;

      serializer = new XmlSerializer(typeof(TextDocument));

      using (FileStream stream = new FileStream(path, FileMode.Open)) {
        return (TextDocument)serializer.Deserialize(stream);
      }
    }

    // binary format is more compact and suitable for large content
    public void SaveToBinary(string path) {
      using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create))) {
        writer.Write(fileName ?? string.Empty);
        writer.Write(content ?? string.Empty);
      }
    }

    public static TextDocument LoadFromBinary(string path) {
      string loadedName;
      string loadedText;

      using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open))) {
        loadedName = reader.ReadString();
        loadedText = reader.ReadString();

        return new TextDocument(loadedName, loadedText);
      }
    }
  }

  // memento object captures the internal state of the document
  // it is immutable to ensure history cannot be accidentally corrupted
  public class EditorMemento {
    public string mementoContent { get; }

    public EditorMemento(string text) {
      mementoContent = text;
    }
  }

  // manages the user interface and document modification history
  public class ConsoleEditor {
    private TextDocument _document;
    private Stack<EditorMemento> _historyStack;

    public ConsoleEditor() {
      _document = new TextDocument();
      _historyStack = new Stack<EditorMemento>();
    }

    // provides the main interaction loop for the text editor
    public void Start() {
      bool isRunning;
      string input;

      isRunning = true;

      while (isRunning) {
        Console.Clear();
        // combined output reduces console flickering and follows clean code practices
        Console.WriteLine($"FILE: {_document.fileName}\n{_document.content}\n" +
                          "---------------------------------------\n" +
                          "1: Add Text | 2: Undo | 3: Save XML | 4: Load XML | 0: Exit");
        Console.Write("Select: ");

        input = Console.ReadLine();

        switch (input) {
          case "1":
            ProcessAddition();
            break;
          case "2":
            ProcessUndo();
            break;
          case "3":
            _document.SaveToXml("data.xml");
            break;
          case "4":
            _document = TextDocument.LoadFromXml("data.xml");
            _historyStack.Clear(); // clear history as the document has been fully replaced
            break;
          case "0":
            isRunning = false;
            break;
        }
      }
    }

    // saves the current state to history before performing modification
    private void ProcessAddition() {
      string newText;

      Console.Write("Enter text: ");
      newText = Console.ReadLine();

      _historyStack.Push(new EditorMemento(_document.content));
      _document.content += newText + Environment.NewLine;
    }

    // restores the previous state from the top of the history stack
    private void ProcessUndo() {
      if (_historyStack.Count > 0) {
        _document.content = _historyStack.Pop().mementoContent;
      }
    }
  }

  // performs scanning and indexing of text files in the system
  public class FileSearchIndexer {
    private Dictionary<string, List<string>> _results;

    public FileSearchIndexer() {
      _results = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
    }

    // initializes the index and starts the recursive scanning process
    public void Index(string path, string[] keywords) {
      int keywordIndex;

      _results.Clear();

      for (keywordIndex = 0; keywordIndex < keywords.Length; ++keywordIndex) {
        _results[keywords[keywordIndex]] = new List<string>();
      }

      RecursiveScan(path, keywords);
      Print();
    }

    // traverses folders recursively to find matching files in all subdirectories
    private void RecursiveScan(string path, string[] keys) {
      try {
        string[] files;
        string[] subDirectories;
        int fileIterator;
        int directoryIterator;

        files = Directory.GetFiles(path, "*.txt");

        for (fileIterator = 0; fileIterator < files.Length; ++fileIterator) {
          string body;
          int currentKeyIndex;

          body = File.ReadAllText(files[fileIterator]);

          for (currentKeyIndex = 0; currentKeyIndex < keys.Length; ++currentKeyIndex) {
            // using IndexOf with case-insensitivity to ensure broad search results
            if (body.IndexOf(keys[currentKeyIndex], StringComparison.OrdinalIgnoreCase) >= 0) {
              _results[keys[currentKeyIndex]].Add(files[fileIterator]);
            }
          }
        }

        subDirectories = Directory.GetDirectories(path);

        for (directoryIterator = 0; directoryIterator < subDirectories.Length; ++directoryIterator) {
          RecursiveScan(subDirectories[directoryIterator], keys);
        }
      } catch (Exception) {
        // access violations are skipped to prevent the app from crashing on system folders
      }
    }

    // displays search statistics and file paths after indexing is complete
    private void Print() {
      string output;
      int fileIndex;

      output = "\n--- SEARCH RESULTS ---\n";

      foreach (var entry in _results) {
        output += $"Keyword [{entry.Key}]: Found in {entry.Value.Count} files.\n";

        // adding nested loop to include each found path into the single output string
        // we use pre-increment and full iterator names as per style guide
        for (fileIndex = 0; fileIndex < entry.Value.Count; ++fileIndex) {
          output += $"  -> {entry.Value[fileIndex]}\n";
        }

        // adding a separator between different keywords for better readability
        output += "\n";
      }

      // a single console call to prevent screen flickering
      Console.WriteLine(output);
    }
  }

  // entry point
  class Program {
    static void Main(string[] args) {
      bool appLoop;

      appLoop = true;

      while (appLoop) {
        string choice;

        Console.Clear();
        // single WriteLine call for the menu interface
        Console.WriteLine("1. Editor\n2. Indexer\n0. Exit");
        Console.Write("Input: ");

        choice = Console.ReadLine();

        if (choice == "1") {
          ConsoleEditor editorInstance;
          editorInstance = new ConsoleEditor();
          editorInstance.Start();
        } else if (choice == "2") {
          PerformSearch();
        } else if (choice == "0") {
          appLoop = false;
        }
      }
    }

    // encapsulates the search parameters collection logic
    private static void PerformSearch() {
      string directoryPath;
      string rawInput;
      string[] keywords;
      FileSearchIndexer indexerInstance;

      Console.Write("Directory: ");
      directoryPath = Console.ReadLine();

      Console.Write("Keywords (comma separated): ");
      rawInput = Console.ReadLine();

      keywords = rawInput.Split(',')
                         .Select(s => s.Trim())
                         .Where(s => !string.IsNullOrEmpty(s))
                         .ToArray();

      indexerInstance = new FileSearchIndexer();
      indexerInstance.Index(directoryPath, keywords);

      Console.WriteLine("Process finished. Press Enter to return to menu.");
      Console.ReadLine();
    }
  }
}