using System;

namespace ZooProject {
  // Base abstraction for all animals in the hierarchy
  public abstract class Animal {
    public string name { get; set; }
    public int age { get; set; }
    public string habitat { get; set; }
    public string diet { get; set; }

    protected Animal(string name, int age, string habitat, string diet) {
      this.name = name;
      this.age = age;
      this.habitat = habitat;
      this.diet = diet;
    }

    // Returns a formatted string for polymorphic display in ZooManager
    public virtual string getInfo() {
      return "Name: " + name + ", Age: " + age + ", Habitat: " + habitat + ", Diet: " + diet;
    }
  }

  // Representative of the mammal group with unique fur property
  public class Mammal : Animal {
    public bool hasFur { get; set; }

    public Mammal(string name, int age, string habitat, string diet, bool hasFur)
      : base(name, age, habitat, diet) {
      this.hasFur = hasFur;
    }

    public override string getInfo() {
      return base.getInfo() + ", Type: Mammal, Fur: " + (hasFur ? "Yes" : "No");
    }
  }

  // Representative of the avian group with wingspan measurement
  public class Bird : Animal {
    public double wingSpan { get; set; }

    public Bird(string name, int age, string habitat, string diet, double wingSpan)
      : base(name, age, habitat, diet) {
      this.wingSpan = wingSpan;
    }

    public override string getInfo() {
      return base.getInfo() + ", Type: Bird, Wing Span: " + wingSpan + "m";
    }
  }
}