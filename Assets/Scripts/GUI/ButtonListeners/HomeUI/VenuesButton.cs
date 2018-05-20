using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VenuesButton : MonoBehaviour 
{
	public void Click()
	{
		Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.PlaceJournal);
	}
}
