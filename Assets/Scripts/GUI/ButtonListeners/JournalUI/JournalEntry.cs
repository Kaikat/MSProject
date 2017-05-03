using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalEntry 
{
	public int AnimalID { private set; get; }
	public AnimalSpecies Species { private set; get; }
	public AnimalEncounterType EncounterType { private set; get; }
	public float Health1 { private set; get; }
	public float Health2 { private set; get; }
	public float Health3 { private set; get; }
	public string EncounterDate { private set; get; }

	public string ReleaseDate { private set; get; }
	public float ReleaseHealth1 { private set; get; }
	public float ReleaseHealth2 { private set; get; }
	public float ReleaseHealth3 { private set; get; }

	public JournalEntry(int animal_id, AnimalSpecies species, AnimalEncounterType encounter, float health1, float health2, float health3, string encounterDate,
		string releaseDate, float releaseHealth1, float releaseHealth2, float releaseHealth3)
	{
		AnimalID = animal_id;
		Species = species;
		EncounterType = encounter;
		Health1 = health1;
		Health2 = health2;
		Health3 = health3;
		EncounterDate = encounterDate;

		ReleaseDate = releaseDate;
		ReleaseHealth1 = releaseHealth1;
		ReleaseHealth2 = releaseHealth2;
		ReleaseHealth3 = releaseHealth3;
	}

	public JournalEntry(int animal_id, AnimalSpecies species, AnimalEncounterType encounter, float health1, float health2, float health3, string encounterDate)
	{
		AnimalID = animal_id;
		Species = species;
		EncounterType = encounter;
		Health1 = health1;
		Health2 = health2;
		Health3 = health3;
		EncounterDate = encounterDate;
	}
}
