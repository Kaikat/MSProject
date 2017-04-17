using UnityEngine;
using System.Collections;

public class BasicAnimal 
{
	public string AnimalID { private set; get; }
	public AnimalSpecies Species { private set; get; }
	public HabitatLevelType HabitatLevel { private set; get; }

	public BasicAnimal (string ID, AnimalSpecies species, HabitatLevelType habitat)
	{
		AnimalID = ID;
		Species = species;
		HabitatLevel = habitat;
	}
}
