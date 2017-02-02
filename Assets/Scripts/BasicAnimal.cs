using UnityEngine;
using System.Collections;

public class BasicAnimal 
{
	public string AnimalID;
	public AnimalSpecies Species;
	public HabitatLevelType HabitatLevel;

	public BasicAnimal (string ID, AnimalSpecies species, HabitatLevelType habitat)
	{
		AnimalID = ID;
		Species = species;
		HabitatLevel = habitat;
	}
}
