﻿using UnityEngine;
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

		if (entry.EncounterType == AnimalEncounterType.Discovered)
		{
			SetDiscoveredEntry (entry);
		}
		else if (entry.EncounterType == AnimalEncounterType.Caught)
		{
			SetCaughtEntry (entry);
		}
		else
		{
			SetReleasedEntry (entry);
		}
	}

	private string ConvertDate(string dateString)
	{
		//FORMAT: 2017/05/01 15:09:25
		string [] tokens = dateString.Split(' ');
		return tokens [0];
	}
		
	private void SetDiscoveredEntry(JournalEntry entry)
	{
		EncounterDate.text = entry.EncounterType.ToString() + " " + ConvertDate (entry.LatestEncounterDate.ToString ());
		ReleaseDate.text = "";
		Health1.text = "";
		Health2.text = "";
		Health3.text = "";
		Health1Released.text = "";
		Health2Released.text = "";
		Health3Released.text = "";
		HealthFactor1.text = "";
		HealthFactor2.text = "";
		HealthFactor3.text = "";
	}

	private void SetCaughtEntry(JournalEntry entry)
	{
		EncounterDate.text = entry.EncounterType.ToString() + " " + ConvertDate(entry.LatestEncounterDate.ToString());
		ReleaseDate.text = "";
		Health1.text = entry.LatestHealth1.ToString ();
		Health2.text = entry.LatestHealth2.ToString ();
		Health3.text = entry.LatestHealth3.ToString ();
		Health1Released.text = "";
		Health2Released.text = "";
		Health3Released.text = "";
		HealthFactor1.text = HEALTH_FACTOR_1;
		HealthFactor2.text = HEALTH_FACTOR_2;
		HealthFactor3.text = HEALTH_FACTOR_3;
	}

	private void SetReleasedEntry(JournalEntry entry)
	{
		EncounterDate.text = entry.EncounterType.ToString() + " " + ConvertDate(entry.LatestEncounterDate.ToString());
		ReleaseDate.text = AnimalEncounterType.Caught.ToString() + " " + ConvertDate(entry.OldEncounterDate.ToString());
		Health1.text = entry.OldHealth1.ToString ();
		Health2.text = entry.OldHealth2.ToString ();
		Health3.text = entry.OldHealth3.ToString ();
		Health1Released.text = entry.LatestHealth1.ToString ();
		Health2Released.text = entry.LatestHealth2.ToString ();
		Health3Released.text = entry.LatestHealth3.ToString ();
		HealthFactor1.text = HEALTH_FACTOR_1;
		HealthFactor2.text = HEALTH_FACTOR_2;
		HealthFactor3.text = HEALTH_FACTOR_3;
	}
}
