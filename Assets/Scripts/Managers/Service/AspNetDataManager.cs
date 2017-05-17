using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JsonResponse;

public class AspNetDataManager : IDataManager
{
	private static AspNetDataManager _Instance;
	public static AspNetDataManager instance
	{
		get 
		{
			if (_Instance == null)
			{
				_Instance = new AspNetDataManager ();
			}
			return _Instance;
		}
	}

	public Dictionary<AnimalSpecies, AnimalData> GetAllAnimalData()
	{
		return new Dictionary<AnimalSpecies, AnimalData> ();
	}

	public string CreateAccount (string username, string name, string password, string email)
	{
		return "Error: This is not yet implemented";
	}

	public bool ValidLogin(string username, string password)
	{
		return false;
	}

	public List<AnimalLocation> GetGPSLocations()
	{
		return new List<AnimalLocation> ();
	}

	public Player GetPlayerData(string username)
	{
		return new Player ("fake", "fake", "fake", 0, 0, 0, 0, new Dictionary<AnimalSpecies, List<Animal>> (), 
			new Dictionary<AnimalSpecies, List<Animal>> (), new List<DiscoveredAnimal> ());
	}

	//Dictionary<AnimalSpecies, List<Animal>> GetPlayerAnimals (string username, bool released);
	//List<DiscoveredAnimal> GetDiscoveredAnimals(string username);
	//int GetEncounterCount(string username, AnimalEncounterType encounter);

	public Dictionary<AnimalSpecies, List<JournalAnimal>> GetAnimalEncountersForJournal(string username, AnimalEncounterType encounter)
	{
		return new Dictionary<AnimalSpecies, List<JournalAnimal>> ();
	}

	public Animal GenerateAnimal (string username, AnimalSpecies species)
	{
		return new Animal (AnimalSpecies.Tiger, -1, new AnimalStats (0f, 0f, 0f, 0f, 0f, 0f), "fake color");
	}

	//string GenerateColorFile (float health1, float health2, float health3);
	//string GenerateColorFile(float health1, float health2, float health3);

	public string NotifyAnimalDiscovered(string username, AnimalSpecies species)
	{
		return "hello";
	}

	public void NotifyAnimalCaught(string username, Animal animal)
	{
	}

	public void NotifyAnimalReleased(string username, Animal animal)
	{
	}

	public List<JournalEntry> GetJournalEntryData(string username)
	{
		return new List<JournalEntry> ();
	}
}