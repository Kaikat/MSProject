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
	public int AnimalsNursing { private set; get; }

	private Dictionary<AnimalSpecies, List<Animal>> Animals;
	private Dictionary<AnimalSpecies, List<Animal>> ReleasedAnimals;
	private List<AnimalSpecies> DiscoveredAnimals;

	public Player(string username, string name, string avatar, int currency, int discovered, int caught, int released,
		Dictionary<AnimalSpecies, List<Animal>> ownedAnimals, Dictionary<AnimalSpecies, List<Animal>> releasedAnimals,
		List<AnimalSpecies> discoveredAnimals)
	{
		Username = username;
		Name = name;
		Avatar = avatar;
		Currency = currency;

		Animals = ownedAnimals;
		ReleasedAnimals = releasedAnimals;
		DiscoveredAnimals = discoveredAnimals;

		AnimalsDiscovered = discovered;
		AnimalsCaught = caught;
		AnimalsReleased = released;
		AnimalsNursing = Animals.Count;
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

	public bool HasDiscoveredAnimal(AnimalSpecies species)
	{
		return DiscoveredAnimals.Contains (species);
	}

	public void AddDiscoveredAnimal(AnimalSpecies species)
	{
		if (!DiscoveredAnimals.Contains(species))
		{
			DiscoveredAnimals.Add (species);
			AnimalsDiscovered++;
		}
	}

	public void AddOwnedAnimal(Animal animal)
	{
		AnimalSpecies species = animal.Species;
		if (Animals.ContainsKey (species)) 
		{				
			Animals [species].Add (animal);
		} 
		else 
		{
			Animals.Add (species, new List<Animal>());
			Animals[species].Add(animal);
			AnimalsDiscovered++;
		}
		AnimalsCaught++;
		AnimalsNursing++;
	}

	public void RemoveOwnedAnimal(Animal animal)
	{
		AnimalSpecies species = animal.Species;
		if (Animals.ContainsKey (species))
		{
			List<Animal> animalsOfSpecies = Animals [species];
			for (int i = animalsOfSpecies.Count - 1; i >= 0; i--)
			{
				Animal animalToRemove = animalsOfSpecies [i];
				if (animalToRemove.AnimalID == animal.AnimalID)
				{
					animalsOfSpecies.Remove (animalToRemove);
				}
			}
		}
		AnimalsNursing--;
	}

	public void AddReleasedAnimal(Animal animal)
	{
		AnimalSpecies species = animal.Species;
		if (ReleasedAnimals.ContainsKey (species))
		{				
			ReleasedAnimals [species].Add (animal);
		}
		else
		{
			ReleasedAnimals.Add (species, new List<Animal> ());
			ReleasedAnimals [species].Add (animal);
		}
		AnimalsReleased++;
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
