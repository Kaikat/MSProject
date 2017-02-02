using UnityEngine;
using System.Collections.Generic;

public interface IServices
{
	List<BasicAnimal> GetAllAnimals ();
	List<Animal> GetPlayerAnimals();

	bool VerifyLogin (string username, string password);
	string CreateAccount (string username, string name, string password, string email);

}
