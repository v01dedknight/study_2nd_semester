namespace ZooProject {
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

    public virtual string getInfo() {
      return "Name: " + name + ", Age: " + age + ", Habitat: " + habitat + ", Diet: " + diet;
    }
  }
}