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
		species = Service.Request.AnimalName (Species);
		EventManager.RegisterEvent<Animal> (GameEvent.AnimalCaught, UpdateButton);
	}

	void Start()
	{
		if (Service.Request.Player ().HasDiscoveredAnimal (Species))
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
		if (animalName != unknown)
		{
			GetComponent<Button> ().image.overrideSprite = Resources.Load<Sprite> (Species.ToString ());
		}
		else
		{
			GetComponent<Button> ().image.overrideSprite = Resources.Load<Sprite> (animalName);
		}
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
