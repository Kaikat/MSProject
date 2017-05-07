using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalEntry 
{
	public int AnimalID { private set; get; }
	public AnimalSpecies Species { private set; get; }
	public AnimalEncounterType EncounterType { private set; get; }
	public float LatestHealth1 { private set; get; }
	public float LatestHealth2 { private set; get; }
	public float LatestHealth3 { private set; get; }
	public System.DateTime LatestEncounterDate { private set; get; }

	public System.DateTime OldEncounterDate { private set; get; }
	public float OldHealth1 { private set; get; }
	public float OldHealth2 { private set; get; }
	public float OldHealth3 { private set; get; }

	public JournalEntry(int animal_id, AnimalSpecies species, AnimalEncounterType encounter, float latestHealth1, float latestHealth2, float latestHealth3, 
		System.DateTime latestEncounterDate, System.DateTime oldEncounterDate, float oldHealth1, float oldHealth2, float oldHealth3)
	{
		AnimalID = animal_id;
		Species = species;
		EncounterType = encounter;
		LatestHealth1 = latestHealth1;
		LatestHealth2 = latestHealth2;
		LatestHealth3 = latestHealth3;
		LatestEncounterDate = latestEncounterDate;

		OldEncounterDate = oldEncounterDate;
		OldHealth1 = oldHealth1;
		OldHealth2 = oldHealth2;
		OldHealth3 = oldHealth3;
	}

	public JournalEntry(int animal_id, AnimalSpecies species, AnimalEncounterType encounter, float health1, float health2, float health3, System.DateTime encounterDate)
	{
		AnimalID = animal_id;
		Species = species;
		EncounterType = encounter;
		LatestHealth1 = health1;
		LatestHealth2 = health2;
		LatestHealth3 = health3;
		LatestEncounterDate = encounterDate;
	}

	public JournalEntry(AnimalEncounterType encounter, AnimalSpecies species, System.DateTime encounterDate)
	{
		EncounterType = encounter;
		Species = species;
		LatestEncounterDate = encounterDate;
	}
}
