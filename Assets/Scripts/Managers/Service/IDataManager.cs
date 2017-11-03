using System;
using System.Collections.Generic;
using UnityEngine;

public interface IDataManager
{
	Dictionary<AnimalSpecies, AnimalData> GetAllAnimalData();
	string CreateAccount (string username, string name, string password, string email, string gender, string birthday);
	JsonResponse.LoginResponse ValidLogin(string username, string password);
	List<AnimalLocation> GetGPSLocations();
	Player GetPlayerData(string username);
	//Dictionary<AnimalSpecies, List<Animal>> GetPlayerAnimals (string username, bool released);
	//List<DiscoveredAnimal> GetDiscoveredAnimals(string username);
	//int GetEncounterCount(string username, AnimalEncounterType encounter);
	//Dictionary<AnimalSpecies, List<JournalAnimal>> GetAnimalEncountersForJournal(string username, AnimalEncounterType encounter);
	Animal GenerateAnimal (string username, AnimalSpecies species);
	//string GenerateColorFile (float health1, float health2, float health3);
	//string GenerateColorFile(float health1, float health2, float health3);
	string NotifyAnimalDiscovered(string username, AnimalSpecies species);
	void NotifyAnimalCaught(string username, Animal animal);
	void NotifyAnimalReleased(string username, Animal animal);
	List<JournalEntry> GetJournalEntryData(string username);

	void UpdateAvatar(string sessionKey, Avatar avatar);

	void SendRatings(string sessionKey, List<InterestValue> interests);

	List<Venue> GetVenueList (string sessionKey);

	Dictionary<string, MajorLocationData> GetRecommendations (string sessionKey);




	List<Vector2> RequestDirections (List<Vector2> placesToVisit);
	Dictionary<string, List<Major>> GetMajorsAtLocation (string sessionKey);
	Dictionary<Major, MajorData> AllMajors();
}