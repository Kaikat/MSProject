using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using JsonResponse;
using HttpHeaderBodies;

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

	private const string WEB_ADDRESS = "http://tamuyal.azurewebsites.net/api/";
	private const string ANIMAL_CONTROLLER = "animals";
	private const string LOCATIONS_CONTROLLER = "locations";
	private const string CREATE_ACCOUNT_CONTROLLER = "createaccount";
	private const string LOGIN_CONTROLLER = "login";
	private const string PLAYER_CONTROLLER = "player";
	private const string PLAYER_ANIMALS_CONTROLLER = "playeranimals";
	private const string NOTIFY_ANIMAL_ENCOUNTER_CONTROLLER = "notifyanimalencounter";
	private const string GENERATE_ANIMAL_CONTROLLER = "generateanimal";
	private const string ANIMAL_ENCOUNTERS_CONTROLLER = "animalencounters";

	private const int JOURNAL_ENTRY_LIMIT = 5;

	public Dictionary<AnimalSpecies, AnimalData> GetAllAnimalData()
	{
		Dictionary<AnimalSpecies, AnimalData> Animals = new Dictionary<AnimalSpecies, AnimalData> ();
		ListResponse response = WebManager.GetHttpResponse<ListResponse> (WEB_ADDRESS + ANIMAL_CONTROLLER);
		foreach (DataAnimal anim in response.AnimalData)
		{
			AnimalData animal = new AnimalData (anim.species.ToEnum<AnimalSpecies> (), anim.name, anim.description, anim.habitat_level.ToEnum<HabitatLevelType> (),
				anim.min_size, anim.max_size, anim.min_age, anim.max_age, anim.min_weight, anim.max_weight, anim.colorkey_map_file);
			Animals.Add (animal.Species, animal);
		}

		return Animals;
	}

	public string CreateAccount (string username, string name, string password, string email)
	{
		AccountDetails accountDetails = new AccountDetails (username, name, password, email);
		BasicResponse response = WebManager.PostHttpResponse<BasicResponse> (
			WEB_ADDRESS + CREATE_ACCOUNT_CONTROLLER, JsonUtility.ToJson (accountDetails));

		Debug.LogWarning ("RESPONSE: " + response.message);
		return response.message;
	}

	public LoginResponse ValidLogin(string username, string password)
	{
		LoginDetails loginDetails = new LoginDetails (username, password);
		// send username to server and get salt from the server if it exists
		// hash the password with the salt
		// send hashed password to server
		BasicResponse loginSession = WebManager.PostHttpResponse<BasicResponse> (
			WEB_ADDRESS + LOGIN_CONTROLLER, JsonUtility.ToJson(loginDetails));

		return new LoginResponse (loginSession.error, loginSession.message);
	}

	public List<AnimalLocation> GetGPSLocations()
	{
		List<AnimalLocation> pointsOfInterest = new List<AnimalLocation> ();
		LocationResponse response = WebManager.GetHttpResponse<LocationResponse> (WEB_ADDRESS + LOCATIONS_CONTROLLER);

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

	public Player GetPlayerData(string sessionKey)
	{
		Debug.LogWarning ("SESSION_KEY: " + sessionKey);
		PlayerDataResponse playerData = WebManager.GetHttpResponse<PlayerDataResponse> (
			WEB_ADDRESS + PLAYER_CONTROLLER + "?session_key=" + WWW.EscapeURL (sessionKey));

		//TODO: Possibly only have get the list of discovered animals and calculate the numbers from there
		return new Player (playerData.name, playerData.avatar, playerData.currency, 
			GetPlayerAnimals(sessionKey, AnimalEncounterType.Caught), GetPlayerAnimals(sessionKey, AnimalEncounterType.Released), GetDiscoveredAnimals(sessionKey));
		//owned, released
	}

	private Dictionary<AnimalSpecies, List<Animal>> GetPlayerAnimals(string sessionKey, AnimalEncounterType encounterType)
	{
		List<Animal> PlayerAnimals = new List<Animal> ();
		OwnedAnimalResponse response = WebManager.GetHttpResponse<OwnedAnimalResponse> (
			WEB_ADDRESS + PLAYER_ANIMALS_CONTROLLER + "?session_key=" + WWW.EscapeURL (sessionKey) + "&encounter_type=" + encounterType.ToString().ToLower());

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

	private List<DiscoveredAnimal> GetDiscoveredAnimals(string sessionKey)
	{
		List<DiscoveredAnimal> discoveredAnimals = new List<DiscoveredAnimal> ();
		DiscoveredListResponse response = WebManager.GetHttpResponse<DiscoveredListResponse> (
			WEB_ADDRESS + PLAYER_ANIMALS_CONTROLLER + "?session_key=" + WWW.EscapeURL (sessionKey) + "&encounter_type=discovered");

		if (!response.empty)
		{
			foreach (DiscoveredSpeciesData animal in response.DiscoveredSpeciesData)
			{
				discoveredAnimals.Add (new DiscoveredAnimal(animal.animal_species.ToEnum<AnimalSpecies> (), animal.discovered_date));
			}
		}

		return discoveredAnimals;
	}

	/*private int GetEncounterCount(string username, AnimalEncounterType encounter)
	{
		BasicIntResponse response = WebManager.GetHttpResponse<BasicIntResponse> (
			WEB_ADDRESS + ENCOUNTER_COUNT + "?username=" + username + "&encounter_type=" + encounter.ToString ());

		return response.count;
	}*/

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

	public Animal GenerateAnimal(string sessionKey, AnimalSpecies species)
	{
		GennedAnimalData gennedAnimal = WebManager.GetHttpResponse<GennedAnimalData> (
			WEB_ADDRESS + GENERATE_ANIMAL_CONTROLLER + 
			"?session_key=" + WWW.EscapeURL (sessionKey) + "&species=" + species.ToString ().ToLower());

		string fstring = "GENNED_ANIMAL\nID: " + gennedAnimal.animal_id.ToString ();
		fstring += "\nhealth1 : " + gennedAnimal.health_1.ToString ();
		fstring += "\nhealth2 : " + gennedAnimal.health_2.ToString ();
		fstring += "\nhealth3 : " + gennedAnimal.health_3.ToString ();
		fstring += "\nage : " + gennedAnimal.age.ToString ();
		fstring += "\nsize : " + gennedAnimal.size.ToString ();
		fstring += "\nweight: " + gennedAnimal.weight.ToString ();
		Debug.LogWarning (fstring);


		return new Animal (species, gennedAnimal.animal_id,
			new AnimalStats(gennedAnimal.health_1, gennedAnimal.health_2, gennedAnimal.health_3, 
				gennedAnimal.age, gennedAnimal.size, gennedAnimal.weight), 
			GenerateColorFile(gennedAnimal.health_1, gennedAnimal.health_2, gennedAnimal.health_3));
	}

	private string GenerateColorFile(float health1, float health2, float health3)
	{
		return "colorfile.txt";
	}

	public string NotifyAnimalDiscovered(string sessionKey, AnimalSpecies species)
	{
		AnimalEncounterData animalData = new AnimalEncounterData ();
		animalData.session_key = sessionKey;
		animalData.encounter_type = AnimalEncounterType.Discovered.ToString ().ToLower ();
		animalData.species = species.ToString();

		BasicResponse response = WebManager.PostHttpResponse<BasicResponse> (
			WEB_ADDRESS + NOTIFY_ANIMAL_ENCOUNTER_CONTROLLER, JsonUtility.ToJson(animalData)
		);

		return response.message;
	}

	public void NotifyAnimalCaught(string sessionKey, Animal animal)
	{
		AnimalEncounterData animalData = new AnimalEncounterData ();
		animalData.session_key = sessionKey;
		animalData.encounter_type = AnimalEncounterType.Caught.ToString ().ToLower ();
		animalData.species = animal.Species.ToString ();
		animalData.encounter_id = animal.AnimalID;
		animalData.nickname = "horsy";//animal.Nickname;
		animalData.age = animal.Stats.Age;
		animalData.height = animal.Stats.Size;
		animalData.weight = animal.Stats.Weight;
		animalData.health1 = animal.Stats.Health1;
		animalData.health2 = animal.Stats.Health2;
		animalData.health3 = animal.Stats.Health3;

		JsonResponse.BasicResponse response = WebManager.PostHttpResponse<BasicResponse> (
			WEB_ADDRESS + NOTIFY_ANIMAL_ENCOUNTER_CONTROLLER, JsonUtility.ToJson(animalData)
		);

		string fstring = "CAUGHT: " + response.message;
		Debug.LogWarning (fstring);
	}

	public void NotifyAnimalReleased(string sessionKey, Animal animal)
	{
		AnimalEncounterData animalData = new AnimalEncounterData ();
		animalData.session_key = sessionKey;
		animalData.encounter_type = AnimalEncounterType.Released.ToString ().ToLower ();
		animalData.species = animal.Species.ToString ();
		animalData.encounter_id = animal.AnimalID;
		animalData.nickname = "horsy";//animal.Nickname;
		animalData.age = animal.Stats.Age;
		animalData.height = animal.Stats.Size;
		animalData.weight = animal.Stats.Weight;
		animalData.health1 = animal.Stats.Health1;
		animalData.health2 = animal.Stats.Health2;
		animalData.health3 = animal.Stats.Health3;

		JsonResponse.BasicResponse response = WebManager.PostHttpResponse<BasicResponse> (
			WEB_ADDRESS + NOTIFY_ANIMAL_ENCOUNTER_CONTROLLER, JsonUtility.ToJson(animalData)
		);

		string fstring = "RELEASED: " + response.message;
		Debug.LogWarning (fstring);
	}

	public List<JournalEntry> GetJournalEntryData(string sessionKey)
	{
		List<JournalEntry> journalEntries = new List<JournalEntry> ();
		JournalResponse response = WebManager.GetHttpResponse<JournalResponse> (
			WEB_ADDRESS + ANIMAL_ENCOUNTERS_CONTROLLER + "?session_key=" + WWW.EscapeURL (sessionKey)
		);

		foreach (JournalEntryData entry in response.JournalEntryData)
		{
			AnimalEncounterType encounter = entry.encounter_type.ToEnum<AnimalEncounterType> ();
			if (encounter == AnimalEncounterType.Released)
			{				
				journalEntries.Add (new JournalEntry(entry.animal_id, entry.species.ToEnum<AnimalSpecies> (), entry.encounter_type.ToEnum<AnimalEncounterType> (), 
					entry.health_1, entry.health_2, entry.health_3, 
					System.DateTime.Parse(entry.encounter_date), System.DateTime.Parse(entry.caught_date), 
					entry.caught_health_1, entry.caught_health_2, entry.caught_health_3));
			}
			else
			{
				journalEntries.Add (new JournalEntry (entry.animal_id, entry.species.ToEnum<AnimalSpecies> (), encounter, 
					entry.health_1, entry.health_2, entry.health_3, System.DateTime.Parse(entry.encounter_date)));
			}
		}

		List<DiscoveredAnimal> discoveredAnimals = GetDiscoveredAnimals (sessionKey);
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
}