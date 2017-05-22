using UnityEngine;
using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

public class RealService : IServices
{
	private static RealService _Instance;
	public static RealService instance
	{
		get 
		{
			if (_Instance == null)
			{
				_Instance = new RealService ();
			}
			return _Instance;
		}
	}

	Player CurrentPlayer;
	Dictionary<AnimalSpecies, AnimalData> Animals;
	List<AnimalLocation> GPSLocations;

	private RealService() 
	{
		Animals = DataManager.Data.GetAllAnimalData ();
		GPSLocations = DataManager.Data.GetGPSLocations ();
	}
				
	public string CreateAccount(string username, string name, string password, string email)
	{
		return DataManager.Data.CreateAccount (username.Trim ().ToLower(), name, password, email);
	}

	public bool VerifyLogin(string username, string password)
	{
		username = username.Trim ().ToLower ();
		password = password.Trim ();
		if (username.Length == 0 || password.Length == 0)
		{
			return false;
		}

		JsonResponse.LoginResponse response =  DataManager.Data.ValidLogin (username, password);
		if (!response.error)
		{
			CurrentPlayer = DataManager.Data.GetPlayerData (response.session_key);
			CurrentPlayer.SessionKey = response.session_key;
			CurrentPlayer.Username = username;

			CurrentPlayer.Print ();
		}

		return !response.error;
	}

	public List<AnimalLocation> PlacesToVisit()
	{
		return GPSLocations;
	}

	public Player Player()
	{
		return CurrentPlayer;
	}

	public Dictionary<AnimalSpecies, AnimalData> AllAnimals()
	{
		return Animals;
	}

	public string AnimalDescription(AnimalSpecies species)
	{
		return Animals [species].Description;
	}

	public string AnimalName(AnimalSpecies species)
	{
		return Animals [species].Name;
	}

	public Animal AnimalToCatch(AnimalSpecies species)
	{
		if (!CurrentPlayer.HasDiscoveredAnimal (species))
		{
			string discovery_date = DataManager.Data.NotifyAnimalDiscovered (CurrentPlayer.SessionKey, species);
			CurrentPlayer.AddDiscoveredAnimal (species, discovery_date);
		}

		return DataManager.Data.GenerateAnimal (CurrentPlayer.SessionKey, species);
	}

	public void CatchAnimal(Animal animal)
	{
		CurrentPlayer.AddOwnedAnimal(animal);
		DataManager.Data.NotifyAnimalCaught(CurrentPlayer.SessionKey, animal);
	}

	public void ReleaseAnimal(Animal animal)
	{
		CurrentPlayer.RemoveOwnedAnimal (animal);
		CurrentPlayer.AddReleasedAnimal (animal);
		DataManager.Data.NotifyAnimalReleased (CurrentPlayer.SessionKey, animal);
	}

	public List<JournalEntry> PlayerJournal()
	{
		return DataManager.Data.GetJournalEntryData (CurrentPlayer.SessionKey);
	}
}