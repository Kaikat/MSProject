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

	public Image Background;
	public Image Panel1;
	public Image Panel2;

	private Animal entryAnimal;

	private const string HEALTH_FACTOR_1 = "Health Factor 1";
	private const string HEALTH_FACTOR_2 = "Health Factor 2";
	private const string HEALTH_FACTOR_3 = "Health Factor 3";

	public void SetJournalEntryElements(JournalEntry entry)
	{
		string animal_species = Service.Request.AnimalEnglishName (entry.Species);
		entryAnimal = Service.Request.Player ().GetAnimalBySpeciesAndID (entry.Species, entry.AnimalID);
		Species.text = animal_species;
		AnimalImage.texture = Resources.Load<Texture> (UIConstants.ANIMAL_IMAGE_PATH + entry.Species.ToString());
		Panel1.color = UIConstants.Beige;
		Panel2.color = UIConstants.Beige;
		//Panel1.sprite = Resources.Load<Sprite> ("Limestone");
		//Panel2.sprite = Resources.Load<Sprite> ("Limestone");

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

	public void Click()
	{
		if (entryAnimal == null)
		{
			return;
		}

		EventManager.TriggerEvent (GameEvent.ObservedAnimalsPreviousScreen, new PreviousScreenData(ScreenType.Journal, entryAnimal));
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Caught);
	}

	private string ConvertDate(string dateString)
	{
		//FORMAT: 2017/05/01 15:09:25
		string [] tokens = dateString.Split(' ');
		return tokens [0];
	}
		
	private void SetDiscoveredEntry(JournalEntry entry)
	{
		Background.color = UIConstants.Blue;
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
		Background.color = UIConstants.Yellow;
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
		Background.color = UIConstants.Green;
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
