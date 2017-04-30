using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalAnimal 
{
	public int EncounterID { private set; get; }
	public AnimalSpecies Species { private set; get; }
	public float Health1 { private set; get; }
	public float Health2 { private set; get; }
	public float Health3 { private set; get; }
	public string EncounterDate { private set; get; }

	public JournalAnimal(int animal_id, AnimalSpecies species, float health1, float health2, float health3, string encounter_date)
	{
		EncounterID = animal_id;
		Species = species;
		Health1 = health1;
		Health2 = health2;
		Health3 = health3;
		EncounterDate = encounter_date;
	}
}
