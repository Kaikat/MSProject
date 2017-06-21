using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToGenderScreen : MonoBehaviour {
	
	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Gender);
	}
}
