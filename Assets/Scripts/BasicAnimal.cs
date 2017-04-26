using UnityEngine;
using System.Collections;

public class BasicAnimal 
{
	public AnimalSpecies Species { private set; get; }
	public HabitatLevelType HabitatLevel { private set; get; }

	public BasicAnimal (AnimalSpecies species, HabitatLevelType habitat)
	{
		Species = species;
		HabitatLevel = habitat;
	}
}
