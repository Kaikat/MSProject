using UnityEngine;
using System.Collections;

public class HomeButton : MonoBehaviour 
{
	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
	}
}
