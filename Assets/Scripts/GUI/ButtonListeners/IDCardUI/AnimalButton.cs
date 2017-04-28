using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AnimalButton : MonoBehaviour {

	public AnimalSpecies Species;
	string species;
	const string unknown = "Unknown";

	void Awake()
	{
		species = Species.ToString ();
		EventManager.RegisterEvent<Animal> (GameEvent.AnimalCaught, UpdateButton);
	}

	void Start()
	{
		Dictionary<AnimalSpecies, List<Animal>> ownedAnimals = Service.Request.Player().GetAnimals();

		if (ownedAnimals.ContainsKey (Species))
		{
			SetButtonComponents (species);
		}
		else
		{
			SetButtonComponents (unknown);
		}	
	}

	void SetButtonComponents(string animalName)
	{
		GetComponentInChildren<Text> ().text = animalName;
		GetComponent<Button> ().image.overrideSprite = Resources.Load<Sprite> (animalName);
	}

	void UpdateButton(Animal animal)
	{
		if (Species == animal.Species)
		{
			SetButtonComponents (species);
			Unregister ();
		}
	}

	void Destroy()
	{
		Unregister ();
	}
		
	void Unregister()
	{
		EventManager.UnregisterEvent<Animal> (GameEvent.AnimalCaught, UpdateButton);
	}
}
