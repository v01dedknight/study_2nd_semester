using System;

namespace ZooProject {
  class Program {
    static void Main(string[] args) {
      ZooManager zoo;
      bool isRunning;
      string userChoice;

      zoo = ZooManager.getInstance();
      isRunning = true;

      // Seed data
      zoo.addAnimal(new Mammal("Barsik", 5, "Forest", "Predator", true));
      zoo.addAnimal(new Bird("Kesha", 2, "Jungle", "Herbivore", 0.5));

      while (isRunning) {
        Console.WriteLine("\n--- Zoo Management ---");
        Console.WriteLine("1. Show animals");
        Console.WriteLine("2. Add mammal");
        Console.WriteLine("3. Exit");
        Console.Write("Enter choice: ");
        
        userChoice = Console.ReadLine() ?? "";

        if (userChoice == "1") {
          zoo.showAllAnimals();
        } else if (userChoice == "2") {
          addNewMammal(zoo);
        } else if (userChoice == "3") {
          isRunning = false;
        } else {
          Console.WriteLine("Invalid input.");
        }
      }
    }

    private static void addNewMammal(ZooManager zoo) {
      // Var declaration
      string name;
      string habitat;
      string diet;
      int age;
      bool hasFur;
      Mammal newMammal;

      // Input data
      try {
        Console.Write("Name: ");
        name = Console.ReadLine() ?? "Unknown"; 
        Console.Write("Age: ");
        age = int.Parse(Console.ReadLine() ?? "0");
        Console.Write("Habitat: ");
        habitat = Console.ReadLine() ?? "Wild";
        Console.Write("Diet: ");
        diet = Console.ReadLine() ?? "Omnivore";
        Console.Write("Has fur (true/false): ");
        hasFur = bool.Parse(Console.ReadLine() ?? "false");

        // Creation
        newMammal = new Mammal(name, age, habitat, diet, hasFur);
        
        // Action
        zoo.addAnimal(newMammal);
        Console.WriteLine("Success.");
      } catch (Exception error) {
        Console.WriteLine("Input error: " + error.Message);
      }
    }
  }
}