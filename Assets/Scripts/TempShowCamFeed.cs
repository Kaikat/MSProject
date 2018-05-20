using UnityEngine;
using System.Collections;

public class TempShowCamFeed : MonoBehaviour 
{
	public void Click()
	{
		Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.CatchAnimal);
	}
}
