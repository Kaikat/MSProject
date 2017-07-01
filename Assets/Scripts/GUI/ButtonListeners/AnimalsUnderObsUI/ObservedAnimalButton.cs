using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObservedAnimalButton : MonoBehaviour 
{
	public Animal animal;

	public void Click()
	{
		if (animal != null) 
		{
			PreviousScreenData screenData = new PreviousScreenData (ScreenType.AnimalUnderObs, animal);
			EventManager.TriggerEvent (GameEvent.ObservedAnimalsPreviousScreen, screenData);
			EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Caught);
		}
	}
}