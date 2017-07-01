using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaughtAnimalDataButton : MonoBehaviour 
{
	private PreviousScreenData screenData;

	void Awake()
	{
		EventManager.RegisterEvent<PreviousScreenData> (GameEvent.ObservedAnimalsPreviousScreen, SavePreviousScreenData);
	}

	void Destroy()
	{
		EventManager.UnregisterEvent<PreviousScreenData> (GameEvent.ObservedAnimalsPreviousScreen, SavePreviousScreenData);
	}

	public void SavePreviousScreenData(PreviousScreenData data)
	{
		screenData = data;
	}

	public void Click()
	{
		Animal animal = screenData.Data as Animal;

		switch (screenData.Screen) 
		{
			case ScreenType.AnimalUnderObs:
				EventManager.TriggerEvent (GameEvent.ViewingAnimalsUnderObservation, animal.Species);
				EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.AnimalUnderObs);
				break;

			case ScreenType.CatchAnimal:
				if (Random.Range(0, 20) >= 15)
				{
					Service.Request.ReleaseAnimal(animal);
					EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Celebration);
				}
				else
				{
					EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Quiz);
					EventManager.TriggerEvent(GameEvent.QuizTime, animal);
				}
				break;

			case ScreenType.Journal:
				EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Journal);
				break;

			default:
				Debug.LogError ("Invalid ScreenType in CaughtAnimalDataButton.cs");
				break;
		}
	}
}
