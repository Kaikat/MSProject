using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalDescriptionsLoader : MonoBehaviour, IShowHideListener
{
	public GameObject MajorsGrid;
	public TaggedShowHide AnimalsScreenTag;

	public Text NumberOfDiscoveredMajors;
	private readonly string DISCOVERED_ANIMALS = "Discovered ";

	private const string PREFAB_FOLDER = "UIPrefabs";
	private const string MAJOR_FAB = "AnimalDescriptionEntry";
	private List<GameObject> entries = new List<GameObject> ();
	private Dictionary<AnimalSpecies, AnimalData> allAnimals;

	void Awake()
	{
		AnimalsScreenTag.listener = this;
	}

	public void OnShow()
	{
		if (allAnimals == null) 
		{
			allAnimals = Service.Request.AllAnimals ();
		}

		Player player = Service.Request.Player ();

		int animalCount = 0;
		var animalSpecies = Enum.GetValues (typeof(AnimalSpecies));
		GameObject parentlessPrefab = AssetManager.LoadPrefab(PREFAB_FOLDER, MAJOR_FAB) as GameObject;

		foreach (AnimalSpecies species in animalSpecies)
		{
			if (species == AnimalSpecies.Horse || species == AnimalSpecies.Butterfly || species == AnimalSpecies.Tiger)
			{
				continue;
			}

			if (player.isAnimalOwned(species) || player.hasReleasedAnimal(species)) 
			{
				GameObject entry = Instantiate(parentlessPrefab);
				entry.GetComponentInChildren<SetAnimalDescriptionEntry> ().SetAnimalDescription (allAnimals[species]);
				entry.transform.SetParent (MajorsGrid.transform);
				entry.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
				entry.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
				entries.Add (entry);
				animalCount++;
			}
		}

		NumberOfDiscoveredMajors.text = DISCOVERED_ANIMALS + animalCount.ToString() + "/" + (AnimalSpecies.GetNames(typeof(AnimalSpecies)).Length - 3).ToString ();	
	}

	public void OnHide()
	{
		foreach (GameObject entry in entries)
		{
			GameObject.Destroy(entry);
		}

		entries.Clear();
	}
}
