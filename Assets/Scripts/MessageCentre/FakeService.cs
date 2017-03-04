using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FakeService : IServices
{
	private static FakeService _Instance;
	public static FakeService instance
	{
		get 
		{
			if (_Instance == null)
			{
				_Instance = new FakeService ();
			}
			return _Instance;
		}
	}
		
	private FakeService() {}

	private int USERNAME_MIN_LENGTH = 8;
	private int NAME_MIN_LENGTH = 3;
	private int PASSWORD_MIN_LENGTH = 8;


	public string CreateAccount(string username, string name, string password, string email)
	{
		string message = "";
		List<string> emailList = new List<string> ();
		emailList.Add ("kaikat14@hotmail.com");
		List<string> users = new List<string> ();
		users.Add ("Kaikat");

		if (users.Contains (username)) 
		{
			message = "A player with that username already exists. Please select a different username.";
		}
		if (username.Length < USERNAME_MIN_LENGTH) 
		{
			message = "Username must be at least " + USERNAME_MIN_LENGTH.ToString() +  " characters long.";
		}
		if (name.Length < NAME_MIN_LENGTH) 
		{
			message = "Name must be at least " + NAME_MIN_LENGTH.ToString () + " characters long.";
		}
		if (password.Length < PASSWORD_MIN_LENGTH) 
		{
			message = "Password must be at least " + PASSWORD_MIN_LENGTH.ToString () + " characters long.";
		}
		if (emailList.Contains (email)) 
		{
			message = "An account with that email already exists!";	
		}

		return message;
	}

	public bool VerifyLogin(string username, string password)
	{
		Dictionary<string, string> UserLogins = new Dictionary<string, string> ();
		UserLogins.Add ("user1", "pass1");
		UserLogins.Add ("user2", "pass2");
		UserLogins.Add ("user3", "pass3");

		return UserLogins.ContainsKey (username) && UserLogins [username] == password;
	}

	public List<BasicAnimal> GetAllAnimals()
	{
		List<BasicAnimal> basicAnimals = new List<BasicAnimal> ();
		basicAnimals.Add (new BasicAnimal ("tiger1", AnimalSpecies.Tiger, HabitatLevelType.Middle));
		basicAnimals.Add (new BasicAnimal ("horse1", AnimalSpecies.Horse, HabitatLevelType.Middle));
		basicAnimals.Add (new BasicAnimal ("butterfly1", AnimalSpecies.Butterfly, HabitatLevelType.Upper));

		return basicAnimals;
	}

	public List<Animal> GetPlayerAnimals()
	{
		List<Animal> PlayerAnimals = new List<Animal> ();

		Animal animal = new Animal ("tiger1", AnimalSpecies.Tiger, "Tigecito", AnimalEncounterType.Caught, HabitatLevelType.Middle);
		PlayerAnimals.Add (animal);

		return PlayerAnimals;
	}

	public void CatchAnimal(AnimalSpecies species)
	{
	}
}
