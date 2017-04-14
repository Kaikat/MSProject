using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatchAnimal : MonoBehaviour
{
	AnimalSpecies encounteredAnimal;

	void Awake ()
	{
		EventManager.RegisterEvent <AnimalSpecies> (GameEvent.AnimalEncounter, ShowEncounteredAnimal);
	}

	void ShowEncounteredAnimal(AnimalSpecies animal)
	{
		encounteredAnimal = animal;
		AssetManager.ShowAnimal (encounteredAnimal);
	}

	void CatchEncounteredAnimal()
	{
		Service.Request.CatchAnimal (encounteredAnimal);
		AssetManager.HideAnimals ();
	}

	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Click()
	{
		CatchEncounteredAnimal ();
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Caught);
		EventManager.TriggerEvent (GameEvent.Caught, encounteredAnimal);
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <AnimalSpecies> (GameEvent.AnimalEncounter, ShowEncounteredAnimal);
	}
}

