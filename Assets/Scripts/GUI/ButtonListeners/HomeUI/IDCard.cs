using UnityEngine;
using System.Collections;

public class IDCard : MonoBehaviour {

	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.IDCard);
	}
}
