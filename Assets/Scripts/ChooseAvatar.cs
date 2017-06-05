using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseAvatar : MonoBehaviour 
{
	public Avatar Avatar;
	
	public void Click ()
	{
		Service.Request.UpdateAvatar (Avatar);
		//EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Home);
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
	}
}
