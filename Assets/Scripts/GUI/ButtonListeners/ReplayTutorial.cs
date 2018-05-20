using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReplayTutorial : MonoBehaviour 
{
	public void Click()
	{
		Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Tutorial);
	}
}
