using UnityEngine;
using System.Collections.Generic;

public interface IServices
{
	List<BasicAnimal> GetAllAnimals ();
	string AnimalDescription (AnimalSpecies species);

	List<Animal> GetPlayerAnimals();
	void CatchAnimal (AnimalSpecies species);

	bool VerifyLogin (string username, string password);
	string CreateAccount (string username, string name, string password, string email);

}
