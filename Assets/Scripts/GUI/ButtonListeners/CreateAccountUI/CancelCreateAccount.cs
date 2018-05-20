using UnityEngine;
using System.Collections;

public class CancelCreateAccount : MonoBehaviour {

	public void Click()
	{
		Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Login);
	}
}
