using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoveredAnimal
{
	public AnimalSpecies Species { private set; get; }
	public string Date { private set; get; }

	public DiscoveredAnimal(AnimalSpecies species, string date)
	{
		Species = species;
		Date = date;
	}
}
