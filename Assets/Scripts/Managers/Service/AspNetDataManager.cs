using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using JsonResponse;
using HttpHeaderBodies;
using MapzenJson;
using System.Text;
using System.Collections;

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
	private const string RECOMMENDATIONS_CONTROLLER = "recommendation";

	private const int JOURNAL_ENTRY_LIMIT = 5;

	public Dictionary<AnimalSpecies, AnimalData> GetAllAnimalData()
	{
		Dictionary<AnimalSpecies, AnimalData> Animals = new Dictionary<AnimalSpecies, AnimalData> ();
		ListResponse response = WebManager.GetHttpResponse<ListResponse> (WEB_ADDRESS + ANIMAL_CONTROLLER);
		foreach (DataAnimal anim in response.AnimalData)
		{
			AnimalData animal = new AnimalData (anim.species.ToEnum<AnimalSpecies> (), anim.name, anim.nahuatl_name, anim.spanish_name, 
				anim.description, anim.habitat_level.ToEnum<HabitatLevelType> (), anim.min_size, anim.max_size, anim.min_age, anim.max_age, 
				anim.min_weight, anim.max_weight, anim.colorkey_map_file);
			Animals.Add (animal.Species, animal);
		}

		return Animals;
	}

	public string CreateAccount (string username, string name, string password, string email, string gender, DateTime birthday)
	{
        // TODO: implement gender and birthday handle
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

	public void UpdateAvatar(string sessionKey, Avatar avatar)
	{
		BasicResponse response = WebManager.GetHttpResponse<BasicResponse> (
			WEB_ADDRESS + PLAYER_CONTROLLER + "?session_key=" + WWW.EscapeURL(sessionKey) + "&avatar=" + avatar.ToString());
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
		return new Player (playerData.name, playerData.avatar.ToEnum<Avatar> (), playerData.currency, playerData.survey,
			GetPlayerAnimals (sessionKey, AnimalEncounterType.Caught), GetPlayerAnimals (sessionKey, AnimalEncounterType.Released), GetDiscoveredAnimals (sessionKey),
			GetRecommendations(sessionKey));
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

		Debug.LogWarning ("JOURNAL: " + WEB_ADDRESS + ANIMAL_ENCOUNTERS_CONTROLLER + "?session_key=" + WWW.EscapeURL (sessionKey));
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
        
        foreach (DiscoveredAnimal animal in GetDiscoveredAnimals(sessionKey).Take(JOURNAL_ENTRY_LIMIT))
        {
            journalEntries.Add(new JournalEntry(AnimalEncounterType.Discovered, animal.Species, animal.Date));
        }

        journalEntries.Sort((x, y) => System.DateTime.Compare(y.LatestEncounterDate, x.LatestEncounterDate));

        return new List<JournalEntry>(journalEntries.Take(JOURNAL_ENTRY_LIMIT));
	}

	public void SendRatings(string sessionKey, List<InterestValue> interests)
	{
		SurveyData data = new SurveyData ();
		data.session_key = sessionKey;
		data.interests = new List<InterestData> ();
		for (int i = 0; i < interests.Count; i++) 
		{
			data.interests.Add (new InterestData (interests [i].Interest.ToString (), interests [i].Value));
		}
		JsonResponse.BasicResponse response = WebManager.PostHttpResponse<BasicResponse> (
			WEB_ADDRESS + RECOMMENDATIONS_CONTROLLER, JsonUtility.ToJson (data)
		);
			
		string sentMessage = "RATINGS SENT: " + response.message;
		Debug.LogWarning (sentMessage);
	}

	private int getIndexOfLocation(string location, List<MajorLocationData> majorListData)
	{
		for(int i = 0; i < majorListData.Count; i++)
		{
			if (majorListData[i].Location == location) 
			{
				return i;
			}
		}

		return -1;
	}

	//TODO: Fix this messy function
	public Dictionary<string, MajorLocationData> GetRecommendations(string sessionKey)
	{
		RecommendationData recommendations = WebManager.GetHttpResponse<RecommendationData> (
			WEB_ADDRESS + RECOMMENDATIONS_CONTROLLER + "?session_key=" + WWW.EscapeURL (sessionKey)
		);
			
		//Debug.LogWarning ("URLLLLLLLLL: " + WEB_ADDRESS + RECOMMENDATIONS_CONTROLLER + "?username=" + username);
		List<MajorLocation> majorListing = recommendations.recommended_majors;
		List<MajorLocationData> majorData = new List<MajorLocationData> ();

		for (int i = 0; i < majorListing.Count; i++) 
		{
			int index = getIndexOfLocation (majorListing [i].Location, majorData);
			if (index == -1) 
			{
				MajorLocationData data = new MajorLocationData (majorListing [i].MajorPreference, majorListing [i].Location);
				majorData.Add (data);
			} 
			else 
			{
				majorData [index].MajorPreferences.Add (majorListing [i].MajorPreference);
			}
		}

		foreach (MajorLocationData data in majorData) 
		{
			data.CalculateAverageValue ();
		}

		majorData.Sort ((y, x) => (x.AverageValue).CompareTo (y.AverageValue));
		Dictionary<string, MajorLocationData> majorRecommendations = new Dictionary<string, MajorLocationData>();
		for (int i = 0; i < majorData.Count; i++) 
		{
			majorData [i].Index = i;
			majorRecommendations.Add(majorData[i].Location, majorData[i]);
		}

		return majorRecommendations;//recommendations.recommended_majors;
	}

	public List<Venue> GetVenueList (string sessionKey)
	{
		RecommendationData recommendations = WebManager.GetHttpResponse<RecommendationData> (
			WEB_ADDRESS + RECOMMENDATIONS_CONTROLLER + "?session_key=" + WWW.EscapeURL (sessionKey)
		);

		List<MajorLocation> majorListing = recommendations.recommended_majors;
		List<Venue> venues = new List<Venue> ();
		List<AnimalLocation> animalListing = GetGPSLocations ();

		//the problem is some locations have the same majors as other places
		//and some places have more than one major
		foreach(MajorLocation majorlocation in majorListing)
		{
			int index = venues.FindIndex(p => p.Location == majorlocation.Location);
			if (index == -1) 
			{
				Venue venue = new Venue (majorlocation.Location, majorlocation.MajorPreference.Major.ToExactEnum<Major>());
				venues.Add (venue);
			} 
			else 
			{
				venues [index].AddMajor (majorlocation.MajorPreference.Major.ToExactEnum<Major>());
			}
		}

		foreach (AnimalLocation animallocation in animalListing) 
		{
			int index = venues.FindIndex(p => p.Location == animallocation.Location.LocationName);
			if (index != -1) 
			{
				venues [index].SetDescription (animallocation.Location.Description);
				venues [index].SetAnimal (animallocation.Animal);
			}
		}

		return venues;
	}

	public void RequestDirections()
	{
		//TODO: Work in progress
		string address = "https://valhalla.mapzen.com/route?json={%22locations%22:" +
			"[{%22lat%22:34.414064," +
			"%22lon%22:-119.847391}," +
			"{%22lat%22:34.412744," +
			"%22lon%22:-119.848396}]," +
			"%22costing%22:%22pedestrian%22," +
			"%22directions_options%22:{%22units%22:%22miles%22}," +
			"%22id%22:%22my_work_route%22}&api_key=";

		address += Keys.MapZenKey;
		Debug.LogWarning (address);
		MapZenResponse response = WebManager.GetHttpResponse<MapZenResponse> (address);

		string resultingPoints = "";
		List<Vector2> polyline = PolylineDecoder.ExtractPolyLine(response.trip.legs [0].shape);
		foreach (Vector2 line in polyline)
		{
			resultingPoints += line.x + ", " + line.y + ", ";
		}
		Debug.LogWarning (resultingPoints);
		//"For n number of break locations, there are n-1 legs. Through locations do not create separate legs."
		//return ;
	}
}