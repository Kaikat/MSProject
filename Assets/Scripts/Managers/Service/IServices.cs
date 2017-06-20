using UnityEngine;
using System;
using System.Collections.Generic;

public interface IServices
{
	//Basic Data
	Dictionary<AnimalSpecies, AnimalData> AllAnimals();
	string AnimalDescription (AnimalSpecies species);
	string AnimalName (AnimalSpecies species);
	List<AnimalLocation> PlacesToVisit();

	Player Player();

	Animal AnimalToCatch(AnimalSpecies species);
	List<JournalEntry> PlayerJournal();

	//Send Data
	void CatchAnimal (Animal animal);
	void ReleaseAnimal (Animal animal);

	bool VerifyLogin (string username, string password);
	string CreateAccount (string username, string name, string password, string email, string gender, DateTime birthdate);
	void UpdateAvatar (Avatar avatar);
}
