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
			Event.Request.TriggerEvent (GameEvent.ObservedAnimalsPreviousScreen, screenData);
			Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Caught);
		}
	}
}