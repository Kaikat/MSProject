using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class CatchAnimal : MonoBehaviour
{
	Animal wildAnimal;

	void Awake ()
	{
		EventManager.RegisterEvent <AnimalSpecies> (GameEvent.AnimalEncounter, ShowEncounteredAnimal);
	}

	void ShowEncounteredAnimal(AnimalSpecies species)
	{
		wildAnimal = Service.Request.AnimalToCatch (species);
		AssetManager.ShowAnimal (species);
	}

	void CatchEncounteredAnimal()
	{
		Service.Request.CatchAnimal (wildAnimal);
		AssetManager.HideAnimals ();
	}

	public void Click()
	{
		CatchEncounteredAnimal ();
		EventManager.TriggerEvent (GameEvent.ObservedAnimalsPreviousScreen, new PreviousScreenData (ScreenType.CatchAnimal, wildAnimal));
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Caught);
		EventManager.TriggerEvent(GameEvent.AnimalCaught, wildAnimal);
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <AnimalSpecies> (GameEvent.AnimalEncounter, ShowEncounteredAnimal);
	}
}





























/*EventManager.TriggerEvent(GameEvent.ViewingAnimalInformation, new Dictionary<string, object>()
        {
            { AnimalInformationController.ANIMAL, wildAnimal },
            { AnimalInformationController.CALLING_SCREEN, ScreenType.CatchAnimal}
        });*/