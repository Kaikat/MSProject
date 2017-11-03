using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

using JsonResponse;

public class PhpDataManager : IDataManager
{
	private static PhpDataManager _Instance;
	public static PhpDataManager instance
	{
		get 
		{
			if (_Instance == null)
			{
				_Instance = new PhpDataManager ();
			}
			return _Instance;
		}
	}

	private const string HTTP_ADDRESS = "http://tamuyal.mat.ucsb.edu:8888/";
	private const string CREATE_ACCOUNT = "CreateAccount.php";
	private const string VERIFY_LOGIN = "VerifyLogin.php";
	private const string GPS_LOCATIONS = "GetGPSLocations.php";
	private const string ANIMAL_DATA = "GetAllAnimals.php";
	private const string PLAYER_DATA = "GetPlayerData.php";
	private const string PLAYER_ANIMALS = "GetPlayerAnimals.php";
	private const string PLAYER_DISCOVERED_LIST = "GetDiscoveredList.php";
	private const string GENERATE_ANIMAL = "GenerateAnimal.php";
	private const string NOTIFY_ANIMAL_DISCOVERED = "NotifyDiscovery.php";
	private const string NOTIFY_ANIMAL_CAUGHT = "CaughtAnimal.php";
	private const string NOTIFY_ANIMAL_RELEASED = "ReleasedAnimal.php";
	private const string ENCOUNTER_COUNT = "GetPlayersDiscoveredAnimals.php";
	private const string ANIMAL_ENCOUNTERS = "GetPlayerEncounters.php";
	private const string LATEST_X_ENCOUNTERS = "GetXMostRecentEncounters.php";

	private const int JOURNAL_ENTRY_LIMIT = 5;

	public Dictionary<AnimalSpecies, AnimalData> GetAllAnimalData()
	{
		Dictionary<AnimalSpecies, AnimalData> Animals = new Dictionary<AnimalSpecies, AnimalData> ();
		ListResponse response = WebManager.GetHttpResponse<ListResponse> (HTTP_ADDRESS + ANIMAL_DATA);
		foreach (DataAnimal anim in response.AnimalData)
		{
			AnimalData animal = new AnimalData (anim.species.ToEnum<AnimalSpecies> (), anim.name, anim.nahuatl_name, anim.spanish_name, 
				anim.description, anim.habitat_level.ToEnum<HabitatLevelType> (), anim.min_size, anim.max_size, anim.min_age, anim.max_age, 
				anim.min_weight, anim.max_weight, anim.colorkey_map_file);
			Animals.Add (animal.Species, animal);
		}

		return Animals;
	}

	public string CreateAccount (string username, string name, string password, string email, string gender, string birthdate)
	{
		BasicResponse response =  WebManager.GetHttpResponse<BasicResponse> (
			HTTP_ADDRESS + CREATE_ACCOUNT + 
			"?username=" + username.ToLower() + "&password=" + password + "&name=" + name + "&email=" + email);
		
		return response.message;
	}

	public JsonResponse.LoginResponse ValidLogin(string username, string password)
	{
		// send username to server and get salt from the server if it exists
		// hash the password with the salt
		// send hashed password to server
		BasicResponse loginSession = WebManager.GetHttpResponse<BasicResponse> (
			HTTP_ADDRESS + VERIFY_LOGIN + 
			"?username=" + username.ToLower() + "&password=" + password);
		
		//return !loginSession.error;
		return new JsonResponse.LoginResponse(loginSession.error, "");
	}

	public void UpdateAvatar(string sessionKey, Avatar avatar)
	{
	}

	public List<AnimalLocation> GetGPSLocations()
	{
		List<AnimalLocation> pointsOfInterest = new List<AnimalLocation> ();
		LocationResponse response = WebManager.GetHttpResponse<LocationResponse> (
			HTTP_ADDRESS + GPS_LOCATIONS);

		if (response.empty)
		{
			return pointsOfInterest;
		}

		foreach (LocationData d in response.LocationData)
		{
			pointsOfInterest.Add (new AnimalLocation(d.species.ToEnum<AnimalSpecies> (), 
				new PointOfInterest(d.location_id, d.location_name, d.description, d.x_coordinate, d.y_coordinate)));
		}

		return pointsOfInterest;
	}

	public Player GetPlayerData(string username)
	{
		PlayerDataResponse playerData = WebManager.GetHttpResponse<PlayerDataResponse> (
			HTTP_ADDRESS + PLAYER_DATA + "?username=" + username);

		//TODO: Possibly only have get the list of discovered animals and calculate the numbers from there
		return new Player (username, playerData.name, playerData.avatar.ToEnum<Avatar>(), playerData.currency, 
			GetEncounterCount(username, AnimalEncounterType.Discovered), 
			GetEncounterCount(username, AnimalEncounterType.Caught), 
			GetEncounterCount(username, AnimalEncounterType.Released), 
			GetPlayerAnimals(username, false), GetPlayerAnimals(username, true), GetDiscoveredAnimals(username));
		//owned, released
	}

	private Dictionary<AnimalSpecies, List<Animal>> GetPlayerAnimals(string username, bool released)
	{
		List<Animal> PlayerAnimals = new List<Animal> ();
		string wasReleased = released ? "1" : "0";
		OwnedAnimalResponse response = WebManager.GetHttpResponse<OwnedAnimalResponse> (
			HTTP_ADDRESS + PLAYER_ANIMALS + "?username=" + username + "&released=" + wasReleased);

		if (response.empty)
		{
			return new Dictionary<AnimalSpecies, List<Animal>> ();
		}
		foreach (OwnedAnimalData r in response.OwnedAnimalData)
		{
			PlayerAnimals.Add(new Animal(r.animal_species.ToEnum<AnimalSpecies>(), r.animal_id, 
				new AnimalStats(r.health_1, r.health_2, r.health_3, r.age, r.height, r.weight), 
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

	private List<DiscoveredAnimal> GetDiscoveredAnimals(string username)
	{
		List<DiscoveredAnimal> discoveredAnimals = new List<DiscoveredAnimal> ();
		DiscoveredListResponse response = WebManager.GetHttpResponse<DiscoveredListResponse> (
			HTTP_ADDRESS + PLAYER_DISCOVERED_LIST + "?username=" + username);

		if (!response.empty)
		{
			foreach (DiscoveredSpeciesData animal in response.DiscoveredSpeciesData)
			{
				discoveredAnimals.Add (new DiscoveredAnimal(animal.animal_species.ToEnum<AnimalSpecies> (), animal.discovered_date));
			}
		}

		return discoveredAnimals;
	}

	private int GetEncounterCount(string username, AnimalEncounterType encounter)
	{
		BasicIntResponse response = WebManager.GetHttpResponse<BasicIntResponse> (
			HTTP_ADDRESS + ENCOUNTER_COUNT + "?username=" + username + "&encounter_type=" + encounter.ToString ());

		return response.count;
	}

	//POSSIBLY DEAD FUNCTION
	//TODO: Use this to update the Journal Page
	/*public Dictionary<AnimalSpecies, List<JournalAnimal>> GetAnimalEncountersForJournal(string username, AnimalEncounterType encounter)
	{
		Dictionary<AnimalSpecies, List<JournalAnimal>> encounteredAnimals = new Dictionary<AnimalSpecies, List<JournalAnimal>> ();

		EncounteredAnimalResponse response = WebManager.GetHttpResponse<EncounteredAnimalResponse> (
			HTTP_ADDRESS + ANIMAL_ENCOUNTERS + "?username=" + username + "&encounter_type=" + encounter.ToString ());

		foreach (EncounterData j in response.EncounterData)
		{
			AnimalSpecies species = j.species.ToEnum<AnimalSpecies> ();
			if (encounteredAnimals.ContainsKey (species))
			{
				encounteredAnimals [species].Add (new JournalAnimal (j.animal_id, species, 
					j.health_1, j.health_2, j.health_3, j.encounter_date));
			}
			else
			{
				encounteredAnimals.Add (species, new List<JournalAnimal> ());
				encounteredAnimals [species].Add (new JournalAnimal (j.animal_id, species, 
					j.health_1, j.health_2, j.health_3, j.encounter_date));
			}
		}
			
		return encounteredAnimals;
	}*/

	public Animal GenerateAnimal(string username, AnimalSpecies species)
	{
		GennedAnimalData gennedAnimal = WebManager.GetHttpResponse<GennedAnimalData> (
			HTTP_ADDRESS + GENERATE_ANIMAL + 
			"?username=" + username + "&species=" + species.ToString ());

		return new Animal (species, gennedAnimal.animal_id,
			new AnimalStats(gennedAnimal.health_1, gennedAnimal.health_2, gennedAnimal.health_3, 
				gennedAnimal.age, gennedAnimal.size, gennedAnimal.weight), 
			GenerateColorFile(gennedAnimal.health_1, gennedAnimal.health_2, gennedAnimal.health_3));
	}

	private string GenerateColorFile(float health1, float health2, float health3)
	{
		return "colorfile.txt";
	}
		
	public string NotifyAnimalDiscovered(string username, AnimalSpecies species)
	{
		BasicResponse response = WebManager.GetHttpResponse<BasicResponse> (
			HTTP_ADDRESS + NOTIFY_ANIMAL_DISCOVERED + 
			"?username=" + username + "&species=" + species.ToString()
		);
			
		return response.message;
	}

	public void NotifyAnimalCaught(string username, Animal animal)
	{
		WebManager.GetHttpResponse<BasicResponse> (
			HTTP_ADDRESS + NOTIFY_ANIMAL_CAUGHT +
			"?username=" + username + "&encounter_id=" + animal.AnimalID.ToString() + 
			"&animal_species=" + animal.Species.ToString() + "&nickname=" + animal.Nickname + 
			"&size=" + animal.Stats.Size.ToString() + "&age=" + animal.Stats.Age.ToString() + "&weight=" + animal.Stats.Weight.ToString() +
			"&health1=" + animal.Stats.Health1.ToString() + "&health2=" + animal.Stats.Health2.ToString() + "&health3=" + animal.Stats.Health3.ToString()
		);
	}

	public void NotifyAnimalReleased(string username, Animal animal)
	{
		WebManager.GetHttpResponse<BasicResponse> (
			HTTP_ADDRESS + NOTIFY_ANIMAL_RELEASED +
			"?username=" + username + "&encounter_id=" + animal.AnimalID.ToString() + "&animal_species=" + animal.Species.ToString() + 
			"&health1=" + animal.Stats.Health1.ToString() + "&health2=" + animal.Stats.Health2.ToString() + "&health3=" + animal.Stats.Health3.ToString()
		);
	}

	public List<JournalEntry> GetJournalEntryData(string username)
	{
		List<JournalEntry> journalEntries = new List<JournalEntry> ();
		JournalResponse response = WebManager.GetHttpResponse<JournalResponse> (
			HTTP_ADDRESS + LATEST_X_ENCOUNTERS +
			"?username=" + username + "&encounter_limit=" + JOURNAL_ENTRY_LIMIT.ToString()
		);

		foreach (JournalEntryData entry in response.JournalEntryData)
		{
			AnimalEncounterType encounter = entry.encounter_type.ToEnum<AnimalEncounterType> ();
			if (encounter == AnimalEncounterType.Released)
			{				
				journalEntries.Add (new JournalEntry(entry.animal_id, entry.species.ToEnum<AnimalSpecies> (), entry.encounter_type.ToEnum<AnimalEncounterType> (), 
					entry.released_health_1, entry.released_health_2, entry.released_health_3, 
					System.DateTime.Parse(entry.released_date), System.DateTime.Parse(entry.caught_date), 
					entry.caught_health_1, entry.caught_health_2, entry.caught_health_3));
			}
			else
			{
				journalEntries.Add (new JournalEntry (entry.animal_id, entry.species.ToEnum<AnimalSpecies> (), encounter,
					entry.caught_health_1, entry.caught_health_2, entry.caught_health_3, System.DateTime.Parse(entry.caught_date)));
			}
		}

		List<DiscoveredAnimal> discoveredAnimals = GetDiscoveredAnimals (username);
		int discoveryEntries = discoveredAnimals.Count >= JOURNAL_ENTRY_LIMIT ? JOURNAL_ENTRY_LIMIT : discoveredAnimals.Count;
		for (int i = 0; i < discoveryEntries; i++)
		{
			journalEntries.Add (new JournalEntry (AnimalEncounterType.Discovered, discoveredAnimals [i].Species, discoveredAnimals [i].Date));
		}
		journalEntries.Sort((x, y) => System.DateTime.Compare(y.LatestEncounterDate, x.LatestEncounterDate));

		List<JournalEntry> finalJournalEntries = new List<JournalEntry> ();
		int totalEntries = journalEntries.Count >= JOURNAL_ENTRY_LIMIT ? JOURNAL_ENTRY_LIMIT : journalEntries.Count;
		for (int i = 0; i < totalEntries; i++)
		{
			finalJournalEntries.Add (journalEntries[i]);
		}

		return finalJournalEntries;
	}



	public void SendRatings(string sessionKey, List<InterestValue> interests)
	{
	}

	public Dictionary<string, MajorLocationData> GetRecommendations(string sessionKey)
	{
		return new Dictionary<string, MajorLocationData> ();
	}

	public List<Venue> GetVenueList (string sessionKey)
	{
		return new List<Venue> ();
	}

	public List<Vector2> RequestDirections(List<Vector2> placesToVisit)
	{
		return new List<Vector2> ();
	}

	public Dictionary<string, List<Major>> GetMajorsAtLocation(string sessionKey)
	{
		return new Dictionary<string, List<Major>> ();
	}

	public Dictionary<Major, MajorData> AllMajors()
	{
		return new Dictionary<Major, MajorData> ();
	}
}






//private string HTTP_ADDRESS = "https://localhost:8888/";
//private string HTTP_ADDRESS = "https://localhost/";
//private string HTTP_ADDRESS = "https://192.168.100.166/";
//private string HTTP_ADDRESS = "https://192.168.1.118/";
//private string HTTP_ADDRESS = "https://192.168.1.118/";

//http://tamuyal.mat.ucsb.edu:8888/CaughtAnimal.php?username=kaikat15&animal_id=tiger1&nickname=tigesote&health=40.0&size=2.0&age=4.0&colorfile=color.txt