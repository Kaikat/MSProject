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

	private Player CurrentPlayer;

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

		Dictionary<AnimalSpecies, List<Animal>> owned = new Dictionary<AnimalSpecies, List<Animal>> ();
		Dictionary<AnimalSpecies, List<Animal>> released = new Dictionary<AnimalSpecies, List<Animal>> ();
		List<AnimalSpecies> discovered = new List<AnimalSpecies> ();

		CurrentPlayer = new Player (username, "name", "avatar", 10000, 0, 0, 0, owned, released, discovered);

		return UserLogins.ContainsKey (username) && UserLogins [username] == password;
	}

	public Player Player()
	{
		return CurrentPlayer;
	}

	public Dictionary<AnimalSpecies, AnimalData> AllAnimals()
	{
		Dictionary<AnimalSpecies, AnimalData> basicAnimals = new Dictionary<AnimalSpecies, AnimalData> ();

		//List<AnimalData> basicAnimals = new List<AnimalData> ();
		//basicAnimals.Add (new BasicAnimal (AnimalSpecies.Tiger, HabitatLevelType.Middle));
		//basicAnimals.Add (new BasicAnimal (AnimalSpecies.Horse, HabitatLevelType.Middle));
		//basicAnimals.Add (new BasicAnimal (AnimalSpecies.Butterfly, HabitatLevelType.Upper));

		return basicAnimals;
	}

	public string AnimalDescription(AnimalSpecies species)
	{
		return "I am an animal!";
	}

	public string[] PlayerData()
	{
		string[] playerInfo = new string[3];
		playerInfo [0] = "Kaikat";
		playerInfo [1] = "1414";
		playerInfo [2] = "Lena";
		return playerInfo;
	}

	public List<Animal> PlayerAnimals()
	{
		List<Animal> PlayerAnimals = new List<Animal> ();

		Animal animal = new Animal (AnimalSpecies.Tiger, 1, new AnimalStats(1.0f, 2.0f, 3.0f, 10.0f, 45.0f, 70.0f), "colorfile.txt");
		animal.SetNickname ("Tigecito");

		PlayerAnimals.Add (animal);

		return PlayerAnimals;
	}
		
	public Animal AnimalToCatch(AnimalSpecies species)
	{
		return new Animal (AnimalSpecies.Tiger, 1, new AnimalStats (1.0f, 2.0f, 3.0f, 10.0f, 40.0f, 35.0f), "colorfile.txt");
	}

	public void CatchAnimal(Animal animal)
	{
	}

	public void ReleaseAnimal(Animal animal)
	{
	}
}
