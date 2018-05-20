using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Venue 
{
	public string Location { private set; get; }
	public string Description { private set; get; }
	public List<Major> Majors { private set; get; }
	public AnimalSpecies Animal { private set; get; }
	public int Index { private set; get; }

	public Venue(string location, Major major)
	{
		Majors = new List<Major> ();
		Majors.Add (major);
		Location = location;
	}

	public Venue(Venue venue, int index)
	{
		Location = venue.Location;
		Description = venue.Description;
		Majors = venue.Majors;
		Animal = venue.Animal;
		Index = index;
	}

	public void AddMajor(Major major)
	{
		Majors.Add(major);
	}

	public void SetDescription(string description)
	{
		Description = description;
	}

	public void SetAnimal(AnimalSpecies animal)
	{
		Animal = animal;
	}
}
