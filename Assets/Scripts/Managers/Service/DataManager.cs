using UnityEngine;
using System.Collections;
using System.Collections.Generic;

using JsonResponse;

public static class DataManager 
{
	private const string HTTP_ADDRESS = "http://tamuyal.mat.ucsb.edu:8888/";
	private const string CREATE_ACCOUNT = "CreateAccount.php";
	private const string VERIFY_LOGIN = "VerifyLogin.php";
	private const string ANIMAL_DATA = "GetAllAnimals.php";
	private const string PLAYER_DATA = "GetPlayerData.php";
	private const string PLAYER_ANIMALS = "GetPlayerAnimals.php";
	private const string GENERATE_ANIMAL = "GenerateAnimal.php";
	private const string NOTIFY_ANIMAL_CAUGHT = "CaughtAnimal.php";

	public static Dictionary<AnimalSpecies, AnimalData> GetAllAnimalData()
	{
		Dictionary<AnimalSpecies, AnimalData> Animals = new Dictionary<AnimalSpecies, AnimalData> ();
		ListResponse response = WebManager.GetHttpResponse<ListResponse> (HTTP_ADDRESS + ANIMAL_DATA);
		foreach (DataAnimal anim in response.AnimalData)
		{
			AnimalData animal = new AnimalData (anim.species.ToEnum<AnimalSpecies> (), anim.description, anim.habitat_level.ToEnum<HabitatLevelType> (),
				anim.min_size, anim.max_size, anim.min_age, anim.max_age, anim.min_weight, anim.max_weight, anim.colorkey_map_file);
			Animals.Add (animal.Species, animal);
		}

		return Animals;
	}

	public static string CreateAccount (string username, string name, string password, string email)
	{
		BasicResponse response =  WebManager.GetHttpResponse<BasicResponse> (
			HTTP_ADDRESS + CREATE_ACCOUNT + 
			"?username=" + username.ToLower() + "&password=" + password + "&name=" + name + "&email=" + email);
		
		return response.message;
	}

	public static bool ValidLogin(string username, string password)
	{
		// send username to server and get salt from the server if it exists
		// hash the password with the salt
		// send hashed password to server
		BasicResponse loginSession = WebManager.GetHttpResponse<BasicResponse> (
			HTTP_ADDRESS + VERIFY_LOGIN + 
			"?username=" + username.ToLower() + "&password=" + password);
		
		return !loginSession.error;
	}

	public static Player GetPlayerData(string username)
	{
		PlayerDataResponse playerData = WebManager.GetHttpResponse<PlayerDataResponse> (
			HTTP_ADDRESS + PLAYER_DATA + "?username=" + username);

		return new Player (username, playerData.name, playerData.avatar, playerData.currency, 
			0, 0, GetPlayerAnimals(username), GetAnimalEncounters(username));
	}

	private static Dictionary<AnimalSpecies, List<Animal>> GetPlayerAnimals(string username)
	{
		List<Animal> PlayerAnimals = new List<Animal> ();
		OwnedAnimalResponse response = WebManager.GetHttpResponse<OwnedAnimalResponse> (
			HTTP_ADDRESS + PLAYER_ANIMALS + "?username=" + username);

		if (response.empty)
		{
			return new Dictionary<AnimalSpecies, List<Animal>> ();
		}
		foreach (OwnedAnimalData r in response.OwnedAnimalData)
		{
			PlayerAnimals.Add(new Animal(r.animal_species.ToEnum<AnimalSpecies>(), r.animal_id, 
				new AnimalStats(r.health_1, r.health_2, r.health_3, r.age, r.size, r.weight), 
				GenerateColorFile(r.health_1, r.health_2, r.health_3)));
		}

		Dictionary<AnimalSpecies, List<Animal>> Animals = new Dictionary<AnimalSpecies, List<Animal>> ();
		for (int i = 0; i < PlayerAnimals.Count; i++) 
		{
			if (Animals.ContainsKey (PlayerAnimals [i].Species)) 
			{
				Animals [PlayerAnimals [i].Species].Add (PlayerAnimals[i]);
			} 
			else 
			{
				Animals.Add (PlayerAnimals [i].Species, new List<Animal>());
				Animals[PlayerAnimals[i].Species].Add(PlayerAnimals [i]);
			}
		}

		return Animals;	
	}

	private static Dictionary<AnimalSpecies, List<AnimalEncounterType>> GetAnimalEncounters(string username)
	{
		//this should query the database for the animal encounters
		return new Dictionary<AnimalSpecies, List<AnimalEncounterType>> ();
	}

	public static Animal GenerateAnimal(string username, AnimalSpecies species)
	{
		GennedAnimalData gennedAnimal = WebManager.GetHttpResponse<GennedAnimalData> (
			HTTP_ADDRESS + GENERATE_ANIMAL + 
			"?username=" + username + "&species=" + species.ToString ());

		return new Animal (species, gennedAnimal.animal_id,
			new AnimalStats(gennedAnimal.health_1, gennedAnimal.health_2, gennedAnimal.health_3, 
				gennedAnimal.age, gennedAnimal.size, gennedAnimal.weight), 
			GenerateColorFile(gennedAnimal.health_1, gennedAnimal.health_2, gennedAnimal.health_3));
	}

	private static string GenerateColorFile(float health1, float health2, float health3)
	{
		return "colorfile.txt";
	}

	//TODO: Change the event triggers and listeners for CatchAnimal (DONE)
	//TODO: Update PHP Code for OwnedAnimal (DONE)
	public static void NotifyAnimalSeen(string username, Animal animal)
	{
		//TODO: In the PHP code save only the first encounter, the "discovery", of an animal for each player (DONE)

		//This is done automatically now in PHP when the animal is first genned
	}


	//TODO: CHECK THESE Notify Calls
	//http://tamuyal.mat.ucsb.edu:8888/CaughtAnimal.php?username=kaikat15&animal_id=tiger1&nickname=tigesote&health=40.0&size=2.0&age=4.0&colorfile=color.txt
	//public static void NotifyAnimalCaught(string username, string animalID, string nickname, float health, float size, float age, string colorFile)
	public static void NotifyAnimalCaught(string username, Animal animal)
	{
		//TODO: CHANGE THE PHP CODE AND DATABASE TO TAKE THE RIGHT PARAMETERS
		WebManager.GetHttpResponse<BasicResponse> (
			HTTP_ADDRESS + NOTIFY_ANIMAL_CAUGHT +
			"?username=" + username + "&encounter_id=" + animal.AnimalID.ToString() + 
			"&animal_species=" + animal.Species.ToString() + "&nickname=" + animal.Nickname + 
			"&size=" + animal.Stats.Size.ToString() + "&age=" + animal.Stats.Age.ToString() + "&weight=" + animal.Stats.Weight.ToString() +
			"&health1=" + animal.Stats.Health1.ToString() + "&health2=" + animal.Stats.Health2.ToString() + "&health3=" + animal.Stats.Health3.ToString()
			);
		/*
		WebManager.GetHttpResponse<BasicResponse> (
			HTTP_ADDRESS + NOTIFY_ANIMAL_CAUGHT +
			"?username=" + username + "&animal_id=" + animalID + "&nickname=" + nickname + 
			"&health=" + health.ToString() + "&size=" + size.ToString() + 
			"&age=" + age.ToString() + "&colorfile=" + colorFile);*/
	}

	public static void NotifyAnimalReleased(string username, Animal animal)
	{
		WebManager.GetHttpResponse<BasicResponse> (
			HTTP_ADDRESS + NOTIFY_ANIMAL_CAUGHT +
			"?username=" + username + "&animal_id=" + animal.Species.ToString() + "&nickname=" + animal.Nickname + "&encounter_type=" + AnimalEncounterType.Released +
			"&health1=" + animal.Stats.Health1.ToString() + "&health2=" + animal.Stats.Health2.ToString() + "&health3=" + animal.Stats.Health3.ToString() +
			"&size=" + animal.Stats.Size.ToString() + "&age=" + animal.Stats.Age.ToString() + "&weight=" + animal.Stats.Weight +
			"&colorfile=" + animal.Colorfile);
	}
}



//private string HTTP_ADDRESS = "https://localhost:8888/";
//private string HTTP_ADDRESS = "https://localhost/";
//private string HTTP_ADDRESS = "https://192.168.100.166/";
//private string HTTP_ADDRESS = "https://192.168.1.118/";
//private string HTTP_ADDRESS = "https://192.168.1.118/";