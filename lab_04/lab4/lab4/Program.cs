using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TextEditorLab {
  public class TextDocument {
    public string fileName { get; set; }
    public string content { get; set; }
    public TextDocument() { content = string.Empty; fileName = "doc.txt"; }
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
      using (BinaryWriter w = new BinaryWriter(File.Open(p, FileMode.Create))) { w.Write(fileName); w.Write(content); }
    }
    public static TextDocument LoadFromBinary(string p) {
      using (BinaryReader r = new BinaryReader(File.Open(p, FileMode.Open))) return new TextDocument(r.ReadString(), r.ReadString());
    }
  }

  // Memento object to store state
  public class EditorMemento {
    public string savedContent { get; }
    public EditorMemento(string text) { savedContent = text; }
  }

  public class ConsoleEditor {
    private TextDocument _doc;
    private Stack<EditorMemento> _history;

    public ConsoleEditor() {
      _doc = new TextDocument();
      _history = new Stack<EditorMemento>();
    }

    public void Start() {
      bool active;
      string cmd;

      active = true;

      while (active) {
        Console.Clear();
        Console.WriteLine($"Editing: {_doc.fileName}\nContent: {_doc.content}");
        Console.WriteLine("1: Add 2: Undo 3: SaveXML 0: Back");
        cmd = Console.ReadLine();

        if (cmd == "1") {
          string input;
          _history.Push(new EditorMemento(_doc.content));
          Console.Write("Text: ");
          input = Console.ReadLine();
          _doc.content += input;
        } else if (cmd == "2" && _history.Count > 0) {
          _doc.content = _history.Pop().savedContent;
        } else if (cmd == "3") {
          _doc.SaveToXml("autosave.xml");
        } else if (cmd == "0") {
          active = false;
        }
      }
    }
  }

  class Program {
    static void Main(string[] args) {
      ConsoleEditor editor = new ConsoleEditor();
      editor.Start();
    }
  }
}