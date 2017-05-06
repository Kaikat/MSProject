using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalLocation 
{
	public AnimalSpecies Animal { private set; get; }
	public PointOfInterest Location { private set; get; }

	public AnimalLocation(AnimalSpecies animal, PointOfInterest pointOfInterest)
	{
		Animal = animal;
		Location = pointOfInterest;
	}
}
