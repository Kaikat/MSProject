using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsButton : MonoBehaviour 
{
	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.AnimalsJournal);
	}
}
