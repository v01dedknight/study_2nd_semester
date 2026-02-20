using System;
using System.Collections.Generic;

namespace ZooProject {
  // Sealed class to prevent inheritance and ensure a single control point
  public sealed class ZooManager {
    // Static field holds the single existing instance of the manager
    private static ZooManager? instance;
    private List<Animal> animalList;

    // Private constructor prevents external instantiation
    private ZooManager() {
      animalList = new List<Animal>();
    }

    // Provides global access point to the instance (Singleton pattern)
    public static ZooManager getInstance() {
      if (instance == null) {
        instance = new ZooManager();
      }
      
      return instance;
    }

    // Adds a new animal to the collection with a null-safety check
    public void addAnimal(Animal animal) {
      if (animal != null) {
        animalList.Add(animal);
      }
    }

    // Iterates through the list using a classic loop to display animal data
    public void showAllAnimals() {
      // Rule 1: Declaration is separated from the calculation
      int animalCount;
      animalCount = animalList.Count;

      if (animalCount == 0) {
        Console.WriteLine("Zoo is empty.");
      } else {
        // Rule 13 & 23: Classic for-loop with a descriptive index instead of 'i'
        for (int animalIndex = 0; animalIndex < animalCount; ++animalIndex) {
          Console.WriteLine(animalList[animalIndex].getInfo());
        }
      }
    }
  }
}