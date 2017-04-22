using UnityEngine;
using System.Collections;

public class CancelCreateAccount : MonoBehaviour {

	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Login);
	}
}
