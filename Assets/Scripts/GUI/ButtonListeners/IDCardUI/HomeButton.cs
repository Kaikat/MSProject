using UnityEngine;
using System.Collections;

public class HomeButton : MonoBehaviour 
{
	public void Click()
	{
		Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
	}
}
