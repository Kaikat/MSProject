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

	public void SetJournalEntryElements(AnimalSpecies species, string encounter_date, float health1, float health2, float health3)
	{
		string animal_species = species.ToString ();
		Species.text = animal_species;
		AnimalImage.texture = Resources.Load<Texture> (animal_species);
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
		string animal_species = species.ToString ();
		Species.text = animal_species;
		AnimalImage.texture = Resources.Load<Texture> (animal_species);
		EncounterDate.text = encounter_date;
		ReleaseDate.text = release_date;
		Health1.text = health1.ToString ();
		Health2.text = health2.ToString ();
		Health3.text = health3.ToString ();
		Health1Released.text = health1_r.ToString ();
		Health2Released.text = health2_r.ToString ();
		Health3Released.text = health3_r.ToString ();
	}
}
