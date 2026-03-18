using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace TextEditorLab {
  // [TextDocument and EditorMemento classes remain same as above]
  public class TextDocument {
    public string fileName { get; set; }
    public string content { get; set; }
    public TextDocument() { content = string.Empty; fileName = "Untitled.txt"; }
    public TextDocument(string n, string t) { fileName = n; content = t; }
    public void SaveToXml(string p) {
      XmlSerializer s = new XmlSerializer(typeof(TextDocument));
      using (FileStream fs = new FileStream(p, FileMode.Create)) s.Serialize(fs, this);
    }
    public static TextDocument LoadFromXml(string p) {
      XmlSerializer s = new XmlSerializer(typeof(TextDocument));
      using (FileStream fs = new FileStream(p, FileMode.Open)) return (TextDocument)s.Deserialize(fs);
    }
    public void SaveToBinary(string p) {
      using (BinaryWriter w = new BinaryWriter(File.Open(p, FileMode.Create))) { w.Write(fileName ?? ""); w.Write(content ?? ""); }
    }
    public static TextDocument LoadFromBinary(string p) {
      using (BinaryReader r = new BinaryReader(File.Open(p, FileMode.Open))) return new TextDocument(r.ReadString(), r.ReadString());
    }
  }

  public class EditorMemento {
    public string savedContent { get; }
    public EditorMemento(string text) { savedContent = text; }
  }

  // Class for indexing files in directory
  public class FileSearchIndexer {
    private Dictionary<string, List<string>> _index;

    public FileSearchIndexer() {
      _index = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);
    }

    public void Run(string path, string[] keys) {
      int i;
      _index.Clear();

      for (i = 0; i < keys.Length; i++) {
        _index[keys[i]] = new List<string>();
      }

      Scan(path, keys);
      Show();
    }

    private void Scan(string path, string[] keys) {
      try {
        string[] files;
        string[] dirs;
        int fIdx;
        int dIdx;

        files = Directory.GetFiles(path, "*.txt");
        for (fIdx = 0; fIdx < files.Length; fIdx++) {
          string text;
          text = File.ReadAllText(files[fIdx]);

          for (int k = 0; k < keys.Length; k++) {
            if (text.IndexOf(keys[k], StringComparison.OrdinalIgnoreCase) >= 0) {
              _index[keys[k]].Add(files[fIdx]);
            }
          }
        }

        dirs = Directory.GetDirectories(path);
        for (dIdx = 0; dIdx < dirs.Length; dIdx++) {
          Scan(dirs[dIdx], keys);
        }
      } catch (Exception) { /* Skip errors */ }
    }

    private void Show() {
      foreach (var pair in _index) {
        Console.WriteLine($"Key '{pair.Key}': {pair.Value.Count} files");
      }
    }
  }

  public class ConsoleEditor {
    private TextDocument _doc;
    private Stack<EditorMemento> _history;
    public ConsoleEditor() { _doc = new TextDocument(); _history = new Stack<EditorMemento>(); }
    public void Start() {
      bool active = true;
      while (active) {
        Console.Clear();
        Console.WriteLine($"Editor: {_doc.fileName}\n1: Add 2: Undo 0: Exit");
        string c = Console.ReadLine();
        if (c == "1") {
          _history.Push(new EditorMemento(_doc.content));
          Console.Write("> ");
          _doc.content += Console.ReadLine() + "\n";
        } else if (c == "2" && _history.Count > 0) _doc.content = _history.Pop().savedContent;
        else if (c == "0") active = false;
      }
    }
  }

  class Program {
    static void Main(string[] args) {
      bool run = true;
      while (run) {
        Console.Clear();
        Console.WriteLine("1: Editor 2: Search 0: Exit");
        string choice = Console.ReadLine();
        if (choice == "1") new ConsoleEditor().Start();
        else if (choice == "2") {
          Console.Write("Path: ");
          string p = Console.ReadLine();
          Console.Write("Keys (comma): ");
          string[] k = Console.ReadLine().Split(',');
          new FileSearchIndexer().Run(p, k);
          Console.ReadLine();
        } else if (choice == "0") run = false;
      }
    }
  }
}