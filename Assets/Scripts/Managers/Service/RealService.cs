using UnityEngine;
using System;
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
	Dictionary<AnimalSpecies, AnimalData> Animals = null;
	List<AnimalLocation> GPSLocations = null;
	//List<Venue> Venues;

	private Animal temporaryUnreleasedAnimal = null;

	private RealService() 
	{
		Animals = DataManager.Data.GetAllAnimalData ();
		GPSLocations = DataManager.Data.GetGPSLocations ();

		if (Animals == null || GPSLocations == null)
		{
			Event.Request.RegisterEvent (GameEvent.ScreenManagerInitialized, ReloadGame);
			WifiManager.SetWifiAvailability (false);
		}
		//Venues = DataManager.Data.GetVenues();
	}

	~RealService()
	{
	}

	public void ReloadGame()
	{
		Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.WifiError);
		Event.Request.RegisterEvent (GameEvent.WifiAvailable, InitAgain);
		Event.Request.UnregisterEvent (GameEvent.ScreenManagerInitialized, ReloadGame);
	}

	public void InitAgain()
	{
		Animals = DataManager.Data.GetAllAnimalData ();
		GPSLocations = DataManager.Data.GetGPSLocations ();

		if (Animals == null || GPSLocations == null)
		{
			WifiManager.SetWifiAvailability (false);
			Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.WifiError);
			return;
		}

		AssetManager.Init ();
		Event.Request.UnregisterEvent (GameEvent.WifiAvailable, InitAgain);
		Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Login);
		WifiManager.SetWifiAvailability (true);
	}
				
	public string CreateAccount(string username, string name, string password, string email, string gender, string birthday)
	{
		string createAccountResult = DataManager.Data.CreateAccount(username.Trim().ToLower(), name, password, email, gender, birthday);
		if (createAccountResult == string.Empty)
		{
			WifiManager.SetWifiAvailability (false);
			return "Please find internet access and try again.";
		}

		WifiManager.SetWifiAvailability (true);
		return createAccountResult;
	}

	public string VerifyLogin(string username, string password)
	{
		username = username.Trim ().ToLower ();
		password = password.Trim ();
		if (username.Length == 0 || password.Length == 0)
		{
			return "false";
		}

		JsonResponse.LoginResponse response =  DataManager.Data.ValidLogin (username, password);
		if (response == null)
		{
			WifiManager.SetWifiAvailability (false);
			return "null";
		}
		if (!response.error)
		{
			CurrentPlayer = DataManager.Data.GetPlayerData (response.session_key);
			CurrentPlayer.SessionKey = response.session_key;
			CurrentPlayer.Username = username;

			CurrentPlayer.Print ();
			WifiManager.SetWifiAvailability (true);
		} 

		return !response.error ? "true" : "false";
	}

	public void UpdateAvatar(Avatar avatar)
	{
		bool error = DataManager.Data.UpdateAvatar (CurrentPlayer.SessionKey, avatar);
		if (error)
		{
			WifiManager.SetWifiAvailability (false);
			return;
		}

		WifiManager.SetWifiAvailability (true);
		CurrentPlayer.SetAvatar (avatar);
	}

	public List<AnimalLocation> PlacesToVisit()
	{
		return GPSLocations;
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

	public string AnimalEnglishName(AnimalSpecies species)
	{
		return Animals [species].Name;
	}

	public string AnimalSpanishName(AnimalSpecies species)
	{
		return Animals [species].SpanishName;
	}

	public string AnimalNahuatlName(AnimalSpecies species)
	{
		return Animals [species].NahuatlName;
	}

	public Animal AnimalToCatch(AnimalSpecies species)
	{
		if (!CurrentPlayer.HasDiscoveredAnimal (species))
		{
			string discovery_date = DataManager.Data.NotifyAnimalDiscovered (CurrentPlayer.SessionKey, species);
			if (discovery_date != null || discovery_date != string.Empty)
			{
				CurrentPlayer.AddDiscoveredAnimal (species, discovery_date);
				WifiManager.SetWifiAvailability (true);
			} 
			else
			{
				WifiManager.SetWifiAvailability (false);
				return null;
			}
		}

		Animal animal = DataManager.Data.GenerateAnimal (CurrentPlayer.SessionKey, species);
		if (animal == null)
		{
			WifiManager.SetWifiAvailability (false);
		} 
		else
		{
			WifiManager.SetWifiAvailability (true);
		}
		return animal;
	}

	public bool CatchAnimal(Animal animal)
	{
		CurrentPlayer.AddOwnedAnimal(animal);
		bool error = DataManager.Data.NotifyAnimalCaught(CurrentPlayer.SessionKey, animal);
		if (error)
		{
			WifiManager.SetWifiAvailability (false);
		}
		else
		{
			WifiManager.SetWifiAvailability (true);
		}
		return error;
	}

	public bool ReleaseAnimal(Animal animal)
	{
		CurrentPlayer.RemoveOwnedAnimal (animal);
		CurrentPlayer.AddReleasedAnimal (animal);
		bool error = DataManager.Data.NotifyAnimalReleased (CurrentPlayer.SessionKey, animal);
		if (error)
		{
			temporaryUnreleasedAnimal = animal;
			WifiManager.SetWifiAvailability (false);
			Event.Request.RegisterEvent (GameEvent.WifiAvailable, AttemptAnimalReleaseAgain);
		}
		else
		{
			WifiManager.SetWifiAvailability (true);
		}
		return error;
	}

	public void AttemptAnimalReleaseAgain()
	{
		if (temporaryUnreleasedAnimal != null)
		{
			bool error = DataManager.Data.NotifyAnimalReleased (CurrentPlayer.SessionKey, temporaryUnreleasedAnimal);
			if (!error)
			{
				WifiManager.SetWifiAvailability (true);
				Event.Request.UnregisterEvent (GameEvent.WifiAvailable, AttemptAnimalReleaseAgain);
				temporaryUnreleasedAnimal = null;
			}
		}
	}

	public List<JournalEntry> PlayerJournal()
	{
		List<JournalEntry> journalEntries = DataManager.Data.GetJournalEntryData (CurrentPlayer.SessionKey);
		if (journalEntries == null)
		{
			WifiManager.SetWifiAvailability (false);
		}
		else
		{
			WifiManager.SetWifiAvailability (true);
		}
		return journalEntries;
	}

	public bool SendPlayerRatings(List<InterestValue> playerInterests)
	{
		bool error = false;
		if (CurrentPlayer.Survey == false)
		{
			error = DataManager.Data.SendRatings (CurrentPlayer.SessionKey, playerInterests);
		}
		if (error)
		{
			WifiManager.SetWifiAvailability (false);
			return true;
		}

		CurrentPlayer.Survey = true;

		Dictionary<string, MajorLocationData> majorRecommendations = DataManager.Data.GetRecommendations (CurrentPlayer.SessionKey);
		if (majorRecommendations == null)
		{
			WifiManager.SetWifiAvailability (false);
			return true;
		}
		else
		{
			WifiManager.SetWifiAvailability (true);
		}

		CurrentPlayer.SetRecommendations (majorRecommendations);
		return error;
	}

	public List<Venue> AllVenues()
	{
		List<Venue> venues = DataManager.Data.GetVenueList(CurrentPlayer.SessionKey);
		if (venues == null)
		{
			WifiManager.SetWifiAvailability (false);
		}
		else
		{
			WifiManager.SetWifiAvailability (true);
		}
		return venues;
	}

	public Dictionary<string, List<Major>> GetMajorsAtLocation()
	{
		Dictionary<string, List<Major>> majorData = DataManager.Data.GetMajorsAtLocation (CurrentPlayer.SessionKey);
		if (majorData == null)
		{
			WifiManager.SetWifiAvailability (false);
		}
		else
		{
			WifiManager.SetWifiAvailability (true);
		}
		return majorData;
	}

	public Dictionary<Major, MajorData> AllMajors()
	{
		Dictionary<Major, MajorData> majors = DataManager.Data.AllMajors ();
		if (majors == null)
		{
			WifiManager.SetWifiAvailability (false);
		}
		else
		{
			WifiManager.SetWifiAvailability (true);
		}
		return majors;
	}
}