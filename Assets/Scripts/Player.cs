using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player {

	public string Username { private set; get; }
	public string Name { private set; get; }
	public string Avatar { private set; get; }
	public int Currency { private set; get; }

	public int AnimalsDiscovered { private set; get; }
	public int AnimalsCaught { private set; get; }
	public int AnimalsReleased { private set; get; }

	private Dictionary<AnimalSpecies, List<Animal>> Animals;
	private Dictionary<AnimalSpecies, List<Animal>> ReleasedAnimals;

	public Player(string username, string name, string avatar, int currency, int discovered, int caught, int released,
		Dictionary<AnimalSpecies, List<Animal>> ownedAnimals, Dictionary<AnimalSpecies, List<Animal>> releasedAnimals)
	{
		Username = username;
		Name = name;
		Avatar = avatar;
		Currency = currency;

		Animals = ownedAnimals;
		ReleasedAnimals = releasedAnimals;

		AnimalsDiscovered = discovered;
		AnimalsCaught = caught;
		AnimalsReleased = released;
	}

	public void Destroy()
	{
	}

	public Dictionary<AnimalSpecies, List<Animal>> GetAnimals() 
	{
		return Animals;
	}

	public Dictionary<AnimalSpecies, List<Animal>> GetReleasedAnimals()
	{
		return ReleasedAnimals;
	}

	public void AddAnimal(AnimalSpecies species, Animal animal)
	{
		
		if (Animals.ContainsKey (species)) 
		{				
			Animals [species].Add (animal);
		} 
		else 
		{
			Animals.Add (species, new List<Animal>());
			Animals[species].Add(animal);
		}
	}
}



/*private void SpawnFunction()
	{
		Debug.Log ("Spawning");
		tigerAsset = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (AnimalSpecies.Tiger));


		//tigerAsset = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (AnimalSpecies.Tiger), new Vector3(0.0f, 0.0f, -5.0f), Quaternion.identity);
		//Animal animal = new Animal ("tiger1", AnimalSpecies.Tiger, "Tigecito", AnimalEncounterType.Caught, HabitatLevelType.Middle);
		//tigerAsset = (GameObject)GameObject.Instantiate(Resources.Load("AnimalPrefabs/Tiger"));//, new Vector3 (0f, 0f, 0f), Quaternion.identity);
	}*/
