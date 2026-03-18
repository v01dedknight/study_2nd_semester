using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace TextEditorLab {
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

    // Standard XML serialization
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

    // Manual Binary serialization for better control
    public void SaveToBinary(string path) {
      using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create))) {
        writer.Write(fileName);
        writer.Write(content);
      }
    }

    public static TextDocument LoadFromBinary(string path) {
      string name;
      string text;

      using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open))) {
        name = reader.ReadString();
        text = reader.ReadString();
        return new TextDocument(name, text);
      }
    }
  }

  class Program {
    static void Main(string[] args) {
      bool isRunning;
      isRunning = true;

      while (isRunning) {
        Console.WriteLine("Menu stub...");
        isRunning = false;
      }
    }
  }
}