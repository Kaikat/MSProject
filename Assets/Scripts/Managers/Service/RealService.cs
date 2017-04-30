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

	public Animal AnimalToCatch(AnimalSpecies species)
	{
		return DataManager.GenerateAnimal (CurrentPlayer.Username, species);
	}

	public void CatchAnimal(Animal animal)
	{
		CurrentPlayer.AddOwnedAnimal(animal);
		DataManager.NotifyAnimalCaught(CurrentPlayer.Username, animal);
	}

	public void ReleaseAnimal(Animal animal)
	{
		CurrentPlayer.RemoveOwnedAnimal (animal);
		CurrentPlayer.AddReleasedAnimal (animal);
		DataManager.NotifyAnimalReleased (CurrentPlayer.Username, animal);
	}
}