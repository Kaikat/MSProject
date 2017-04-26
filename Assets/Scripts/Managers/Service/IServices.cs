using UnityEngine;
using System.Collections.Generic;

public interface IServices
{
	//Basic Data
	Dictionary<AnimalSpecies, AnimalData> AllAnimals();
	string AnimalDescription (AnimalSpecies species);

	Player Player();

	//Send Data
	void CatchAnimal (AnimalSpecies species);


	bool VerifyLogin (string username, string password);
	string CreateAccount (string username, string name, string password, string email);
}
