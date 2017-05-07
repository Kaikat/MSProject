using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetJournalEntry : MonoBehaviour {

	public RawImage AnimalImage;
	public Text Species;
	public Text EncounterDate;
	public Text ReleaseDate;
	public Text Health1;
	public Text Health2;
	public Text Health3;
	public Text Health1Released;
	public Text Health2Released;
	public Text Health3Released;

	public Text HealthFactor1;
	public Text HealthFactor2;
	public Text HealthFactor3;

	private const string HEALTH_FACTOR_1 = "Health Factor 1";
	private const string HEALTH_FACTOR_2 = "Health Factor 2";
	private const string HEALTH_FACTOR_3 = "Health Factor 3";

	public void SetJournalEntryElements(JournalEntry entry)
	{
		string animal_species = Service.Request.AnimalName (entry.Species);
		Species.text = animal_species;
		AnimalImage.texture = Resources.Load<Texture> (entry.Species.ToString());

		EncounterDate.text = entry.EncounterType != AnimalEncounterType.Discovered ? 
			AnimalEncounterType.Caught.ToString () + " " + ConvertDate (entry.LatestEncounterDate.ToString ()) :
			AnimalEncounterType.Discovered.ToString () + " " + ConvertDate (entry.LatestEncounterDate.ToString ());

		ReleaseDate.text = entry.EncounterType == AnimalEncounterType.Released ? 
			entry.EncounterType.ToString() + " " + ConvertDate(entry.LatestEncounterDate.ToString()) : "";
		
		Health1Released.text = entry.EncounterType == AnimalEncounterType.Released ? entry.LatestHealth1.ToString() : "";
		Health2Released.text = entry.EncounterType == AnimalEncounterType.Released ? entry.LatestHealth2.ToString() : "";
		Health3Released.text = entry.EncounterType == AnimalEncounterType.Released ? entry.LatestHealth3.ToString() : "";

		HealthFactor1.text = entry.EncounterType == AnimalEncounterType.Discovered ? "" : HEALTH_FACTOR_1;
		HealthFactor2.text = entry.EncounterType == AnimalEncounterType.Discovered ? "" : HEALTH_FACTOR_2;
		HealthFactor3.text = entry.EncounterType == AnimalEncounterType.Discovered ? "" : HEALTH_FACTOR_3;

		if (entry.EncounterType == AnimalEncounterType.Released)
		{
			Health1.text = entry.OldHealth1.ToString ();
			Health2.text = entry.OldHealth2.ToString ();
			Health3.text = entry.OldHealth3.ToString ();
		}
		else if (entry.EncounterType == AnimalEncounterType.Caught)
		{
			Health1.text = entry.LatestHealth1.ToString ();
			Health2.text = entry.LatestHealth2.ToString ();
			Health3.text = entry.LatestHealth3.ToString ();
		}
		else
		{
			Health1.text = "";
			Health2.text = "";
			Health3.text = "";
		}
	}

	string ConvertDate(string dateString)
	{
		//2017-05-01 15:09:25
		string [] tokens = dateString.Split(' ');
		//string[] removeAfterSpace = tokens [2].Split (' ');
		return tokens [0];// + "/" + removeAfterSpace [0] + "/" + tokens [0];
	}

	/*
	public void SetJournalEntryElements(AnimalSpecies species, string encounter_date)
	{
		string animal_species = Service.Request.AnimalName (species);
		Species.text = animal_species;
		AnimalImage.texture = Resources.Load<Texture> (species.ToString());
		EncounterDate.text = encounter_date;
		ReleaseDate.text = "";
		Health1.text = "";
		Health2.text = "";
		Health3.text = "";
		Health1Released.text = "";
		Health2Released.text = "";
		Health3Released.text = "";
	}

	public void SetJournalEntryElements(AnimalSpecies species, string encounter_date, float health1, float health2, float health3)
	{
		string animal_species = Service.Request.AnimalName (species);
		Species.text = animal_species;
		AnimalImage.texture = Resources.Load<Texture> (species.ToString());
		EncounterDate.text = encounter_date;
		ReleaseDate.text = "";
		Health1.text = health1.ToString ();
		Health2.text = health2.ToString ();
		Health3.text = health3.ToString ();
		Health1Released.text = "";
		Health2Released.text = "";
		Health3Released.text = "";
	}

	public void SetJournalEntryElements(AnimalSpecies species, string encounter_date, float health1, float health2, float health3,
		string release_date, float health1_r, float health2_r, float health3_r)
	{
		string animal_species = Service.Request.AnimalName (species);
		Species.text = animal_species;
		AnimalImage.texture = Resources.Load<Texture> (species.ToString());
		EncounterDate.text = encounter_date;
		ReleaseDate.text = release_date;
		Health1.text = health1.ToString ();
		Health2.text = health2.ToString ();
		Health3.text = health3.ToString ();
		Health1Released.text = health1_r.ToString ();
		Health2Released.text = health2_r.ToString ();
		Health3Released.text = health3_r.ToString ();
	}*/
}
