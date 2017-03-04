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

	private RealService() {}

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
			HTTP_ADDRESS + "CreateAccount.php" + "?username=" + username + "&password=" + password);


		/*if (users.Contains (username)) 
		{
			message = "A player with that username already exists. Please select a different username.";
		}

		if (emailList.Contains (email)) 
		{
			message = "An account with that email already exists!";	
		}*/

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
			HTTP_ADDRESS + "VerifyLogin.php" + "?username=" + username + "&password=" + password);

		if (loginSession.error == true)
		{
			return false;
		}
			
		return true;
	}

	public List<BasicAnimal> GetAllAnimals()
	{
		List<BasicAnimal> basicAnimals = new List<BasicAnimal> ();
		DictResponse response = WebManager.GetHttpResponse<DictResponse> (HTTP_ADDRESS + "GetAllAnimals.php");

		foreach (AnimalData r in response.AnimalData)
		{
			basicAnimals.Add(new BasicAnimal(r.animal_id, 
				r.name.ToEnum<AnimalSpecies>(), 
				r.habitat_level.ToEnum<HabitatLevelType>()));
			Debug.Log (r.animal_id);
		}

		return basicAnimals;
	}

	public List<Animal> GetPlayerAnimals()
	{
		List<Animal> PlayerAnimals = new List<Animal> ();

		//EchoResponse response = WebManager.instance.GetHttpResponse<EchoResponse> ("http://localhost:8888/echo.php?tag=stuff");
		//Debug.Log (response.ResultingData[1]);

		Animal animal = new Animal ("tiger1", AnimalSpecies.Tiger, "Tigecito", AnimalEncounterType.Caught, HabitatLevelType.Middle);
		PlayerAnimals.Add (animal);

		return PlayerAnimals;
	}

	[System.Serializable]
	public class JsonResponse
	{
		public string id;
		public string message;
		public bool error;
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
		public string habitat_level;
		public string sensor_id;
		public float min_size;
		public float max_size;
		public float min_age;
		public float max_age;
		public string colorkey_map_file;
	}

}
