using UnityEngine;
using System;
using System.Collections.Generic;

public interface IServices
{
	void InitAgain();
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
	bool CatchAnimal (Animal animal);
	bool ReleaseAnimal (Animal animal);

	string VerifyLogin (string username, string password);
	string CreateAccount (string username, string name, string password, string email, string gender, string birthdate);
	void UpdateAvatar (Avatar avatar);

	bool SendPlayerRatings (List<InterestValue> playerInterests);
	//Dictionary<string, MajorLocationData> GetRecommendations ();
	List<Venue> AllVenues();
	Dictionary<string, List<Major>> GetMajorsAtLocation ();
	Dictionary<Major, MajorData> AllMajors();
}
