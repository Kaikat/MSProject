using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToMainMenuButton : MonoBehaviour 
{
	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Menu);
	}
}
