using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CatchAnimal : MonoBehaviour
{
	Animal wildAnimal;

	void Awake ()
	{
		Event.Request.RegisterEvent <AnimalSpecies> (GameEvent.AnimalEncounter, ShowEncounteredAnimal);
	}

	void ShowEncounteredAnimal(AnimalSpecies species)
	{
		wildAnimal = Service.Request.AnimalToCatch (species);
		if (wildAnimal == null)
		{
			Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
			return;
		}

		AssetManager.ShowAnimal (species);
	}

	bool CatchEncounteredAnimal()
	{
		bool error = Service.Request.CatchAnimal (wildAnimal);
		if (error)
		{
			return false;
		}

		AssetManager.HideAnimals ();
		return true;
	}

	public void Click()
	{
		if (CatchEncounteredAnimal ())
		{
			Event.Request.TriggerEvent (GameEvent.ObservedAnimalsPreviousScreen, new PreviousScreenData (ScreenType.CatchAnimal, wildAnimal));
			Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Caught);
			Event.Request.TriggerEvent (GameEvent.AnimalCaught, wildAnimal);
		}
	}

	void Destroy()
	{
		Event.Request.UnregisterEvent <AnimalSpecies> (GameEvent.AnimalEncounter, ShowEncounteredAnimal);
	}
}





























/*EventManager.TriggerEvent(GameEvent.ViewingAnimalInformation, new Dictionary<string, object>()
        {
            { AnimalInformationController.ANIMAL, wildAnimal },
            { AnimalInformationController.CALLING_SCREEN, ScreenType.CatchAnimal}
        });*/