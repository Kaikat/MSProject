using UnityEngine;
using System.Collections;

public class JournalButton : MonoBehaviour {

	//TODO: Move me to HomeUI
	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Journal);
	}
}

