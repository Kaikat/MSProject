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

	private RealService() 
	{
		Animals = DataManager.GetAllAnimalData ();
	}
				
	public string CreateAccount(string username, string name, string password, string email)
	{
		return DataManager.CreateAccount (username.Trim ().ToLower(), name, password, email);
	}

	public bool VerifyLogin(string username, string password)
	{
		username = username.Trim ().ToLower ();
		password = password.Trim ();
		if (username.Length == 0 || password.Length == 0)
		{
			return false;
		}
			
		if (!DataManager.ValidLogin (username, password))
		{
			return false;
		}

		CurrentPlayer = DataManager.GetPlayerData (username);
		return true;
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

	public void CatchAnimal(AnimalSpecies species)
	{
		string nickname = "tigecito";
		string animalID = species.ToString().ToLower() + "1";

		float health = 2.0f;
		float age = 4.0f;
		float size = 3.0f;
		string colorFile = "colorfile.txt";

		Animal animal = new Animal (animalID, species, nickname, AnimalEncounterType.Caught, HabitatLevelType.Middle);
		DataManager.NotifyAnimalCaught (CurrentPlayer.Username, animalID, nickname, health, size, age, colorFile);

		CurrentPlayer.AddAnimal(species, animal);
	}
}