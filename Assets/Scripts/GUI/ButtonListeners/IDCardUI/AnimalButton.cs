using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AnimalButton : MonoBehaviour {

	public AnimalSpecies Species;
	string species;
	const string UNKNOWN = "Unknown";
    private bool clickable = false;

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
			SetButtonComponents (UNKNOWN);
		}
	}

	void SetButtonComponents(string animalName)
	{
		GetComponentInChildren<Text> ().text = animalName;
		if (animalName != UNKNOWN)
		{
            clickable = true;
            GetComponent<Button> ().image.overrideSprite = Resources.Load<Sprite> (Species.ToString ());
		}
		else
		{
			GetComponent<Button> ().image.overrideSprite = Resources.Load<Sprite> (UNKNOWN);
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

    public void Click()
    {
        if(clickable)
        {
            EventManager.TriggerEvent(GameEvent.ViewingAnimalsUnderObservation, Species);
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.AnimalUnderObs);
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