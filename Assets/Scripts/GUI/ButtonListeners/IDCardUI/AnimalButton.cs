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
		EventManager.RegisterEvent<AnimalSpecies> (GameEvent.Caught, UpdateButton);
	}

	void Start()
	{
		bool found = false;

		List<Animal> ownedAnimals = Service.Request.PlayerAnimals (StartGame.CurrentPlayer.Username);
		for (int i = 0; i < ownedAnimals.Count; i++)
		{
			if (ownedAnimals [i].Species == Species)
			{
				found = true;
			}
		}

		if (found)
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

	void UpdateButton(AnimalSpecies animal)
	{
		if (Species == animal)
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
		EventManager.UnregisterEvent<AnimalSpecies> (GameEvent.Caught, UpdateButton);
	}
}
