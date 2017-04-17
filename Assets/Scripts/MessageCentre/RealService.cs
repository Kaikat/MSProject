﻿using UnityEngine;
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

	Dictionary<AnimalSpecies, BasicAnimal> Animals;
	Dictionary<AnimalSpecies, string> AnimalDescriptions;
	List<BasicAnimal> basicAnimals;

	private RealService() 
	{
		//Get All animals
		basicAnimals = new List<BasicAnimal> ();
		Animals = new Dictionary<AnimalSpecies, BasicAnimal> ();
		AnimalDescriptions = new Dictionary<AnimalSpecies, string> ();

		DictResponse response = WebManager.GetHttpResponse<DictResponse> (HTTP_ADDRESS + "GetAllAnimals.php");

		foreach (AnimalData r in response.AnimalData)
		{
			BasicAnimal animal = new BasicAnimal (r.animal_id, 
				r.name.ToEnum<AnimalSpecies> (),
				r.habitat_level.ToEnum<HabitatLevelType> ());
			Animals [animal.Species] = animal;
			AnimalDescriptions[animal.Species] = r.description;
			basicAnimals.Add (animal);
		}
	}

	//private string HTTP_ADDRESS = "https://localhost:8888/";
	//private string HTTP_ADDRESS = "https://localhost/";
	//private string HTTP_ADDRESS = "https://192.168.100.166/";
	//private string HTTP_ADDRESS = "https://192.168.1.118/";
	//private string HTTP_ADDRESS = "https://192.168.1.118/";
	private string HTTP_ADDRESS = "http://tamuyal.mat.ucsb.edu:8888/";
		
	public string CreateAccount(string username, string name, string password, string email)
	{
		string message = "";

		JsonResponse response = WebManager.GetHttpResponse<JsonResponse> (
			HTTP_ADDRESS + "CreateAccount.php" + "?username=" + username.ToLower() + "&password=" + password + "&name=" + name + "&email=" + email);

		return response.message;
	}

	public bool VerifyLogin(string username, string password)
	{
		username = username.Trim ();
		password = password.Trim ();
		if (username.Length == 0 || password.Length == 0)
		{
			return false;
		}

		username = username.ToLower ();
		// send username to server and get salt from the server if it exists
		// hash the password with the salt
		// send hashed password to server
		JsonResponse loginSession = WebManager.GetHttpResponse<JsonResponse> (
			HTTP_ADDRESS + "VerifyLogin.php" + "?username=" + username.ToLower() + "&password=" + password);

		if (loginSession.error == true)
		{
			return false;
		}
			
		return true;
	}

	public List<BasicAnimal> GetAllAnimals()
	{
		return basicAnimals;
	}

	public string AnimalDescription(AnimalSpecies species)
	{
		return AnimalDescriptions [species];
	}

	public string[] PlayerData(string username)
	{
		string url = HTTP_ADDRESS + "GetPlayerData.php?username=" + username;
		PlayerDataResponse playerData = WebManager.GetHttpResponse<PlayerDataResponse> (url);

		string[] playerInfo = new string[3];
		playerInfo [0] = playerData.name;
		playerInfo [1] = playerData.currency.ToString ();
		playerInfo [2] = playerData.avatar;

		return playerInfo;
	}

	public List<Animal> GetPlayerAnimals(string username)
	{
		List<Animal> PlayerAnimals = new List<Animal> ();

		//EchoResponse response = WebManager.instance.GetHttpResponse<EchoResponse> ("http://localhost:8888/echo.php?tag=stuff");
		//Debug.Log (response.ResultingData[1]);

		//Animal animal = new Animal ("tiger1", AnimalSpecies.Tiger, "Tigecito", AnimalEncounterType.Caught, HabitatLevelType.Middle);
		//PlayerAnimals.Add (animal);

		//http://tamuyal.mat.ucsb.edu:8888/GetPlayerAnimals.php?username=kaikat14
		string url = HTTP_ADDRESS + "GetPlayerAnimals.php?username=" + username;
		OwnedAnimalResponse response = WebManager.GetHttpResponse<OwnedAnimalResponse> (url);
		if (response.empty)
		{
			return PlayerAnimals;
		}
		foreach (OwnedAnimalData r in response.OwnedAnimalData)
		{
			AnimalSpecies specie;
			if (r.animal_id == "tiger1")
			{
				specie = AnimalSpecies.Tiger;
			}
			else if (r.animal_id == "butterfly1")
			{
				specie = AnimalSpecies.Butterfly;
			}
			else if (r.animal_id == "horse1")
			{
				specie = AnimalSpecies.Horse;
			}
			else
			{
				specie = AnimalSpecies.Tiger;
			}
			PlayerAnimals.Add (new Animal (r.animal_id, specie, r.nickname, AnimalEncounterType.Caught, HabitatLevelType.Middle));
		}

		return PlayerAnimals;
	}

	//http://tamuyal.mat.ucsb.edu:8888/CaughtAnimal.php?username=kaikat15&animal_id=tiger1&nickname=tigesote&health=40.0&size=2.0&age=4.0&colorfile=color.txt
	public void CatchAnimal(AnimalSpecies species)
	{
		string nickname = "tigecito";
		//string animalID = species == AnimalSpecies.Tiger ? "tiger1" : "butterfly1";
		string animalID = species.ToString().ToLower() + "1";

		float health = 2.0f;
		float age = 4.0f;
		float size = 3.0f;
		string colorFile = "colorfile.txt";

		Animal animal = new Animal (animalID, species, nickname, AnimalEncounterType.Caught, HabitatLevelType.Middle);
		WebManager.GetHttpResponse<JsonResponse> (HTTP_ADDRESS + "CaughtAnimal.php?username=" + StartGame.CurrentPlayer.Username +
			"&animal_id=" + animalID + "&nickname=" + nickname + "&health=" + health.ToString() + "&size=" + size.ToString() + 
			"&age=" + age.ToString() + "&colorfile=" + colorFile);

		StartGame.CurrentPlayer.AddAnimal (species, animal);
			/*kaikat15&animal_id=tiger1
			&nickname=tigesote
			&health=40.0&size=2.0&age=4.0&colorfile=color.txt*/
	}

	[System.Serializable]
	public class JsonResponse
	{
		public string id;
		public string message;
		public bool error;
	}

	[System.Serializable]
	public class PlayerDataResponse
	{
		public string name;
		public string avatar;
		public int currency;
	}

	[System.Serializable]
	public class EchoResponse
	{
		public string[] ResultingData;
	}

	[System.Serializable]
	public class DictResponse
	{
		public List<AnimalData> AnimalData;
	}

	[System.Serializable]
	public class AnimalData
	{
		public string animal_id;
		public string name;
		public string description;
		public string habitat_level;
		public string sensor_id;
		public float min_size;
		public float max_size;
		public float min_age;
		public float max_age;
		public string colorkey_map_file;
	}

	[System.Serializable]
	public class OwnedAnimalResponse
	{		
		public bool empty;
		public List<OwnedAnimalData> OwnedAnimalData;
	}
			
	[System.Serializable]
	public class OwnedAnimalData
	{
		public string animal_id;
		public string nickname;
		public string health;
		public float size;
		public float age;
		public string color_file;
	}
}
