using UnityEngine;
using System.Collections.Generic;

public interface IServices
{
	//Basic Data
	List<BasicAnimal> AllAnimals ();
	string AnimalDescription (AnimalSpecies species);

	//Player Specific Data
	string[] PlayerData (string username);
	List<Animal> PlayerAnimals(string username);

	//Send Data
	void CatchAnimal (AnimalSpecies species);


	bool VerifyLogin (string username, string password);
	string CreateAccount (string username, string name, string password, string email);
}
