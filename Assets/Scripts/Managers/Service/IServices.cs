using UnityEngine;
using System;
using System.Collections.Generic;

public interface IServices
{
	//Basic Data
	Dictionary<AnimalSpecies, AnimalData> AllAnimals();
	string AnimalDescription (AnimalSpecies species);
	string AnimalEnglishName (AnimalSpecies species);
	string AnimalSpanishName (AnimalSpecies species);
	string AnimalNahuatlName (AnimalSpecies species);
	List<AnimalLocation> PlacesToVisit();

	Player Player();

	Animal AnimalToCatch(AnimalSpecies species);
	List<JournalEntry> PlayerJournal();

	//Send Data
	void CatchAnimal (Animal animal);
	void ReleaseAnimal (Animal animal);

	bool VerifyLogin (string username, string password);
	string CreateAccount (string username, string name, string password, string email, string gender, string birthdate);
	void UpdateAvatar (Avatar avatar);

	void SendPlayerRatings (List<InterestValue> playerInterests);
	//Dictionary<string, MajorLocationData> GetRecommendations ();
	List<Venue> AllVenues();
	Dictionary<string, List<Major>> GetMajorsAtLocation ();
	Dictionary<Major, MajorData> AllMajors();
}
