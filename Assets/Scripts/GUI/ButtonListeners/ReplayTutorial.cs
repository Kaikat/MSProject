using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayTutorial : MonoBehaviour 
{
	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Tutorial);
	}
}
