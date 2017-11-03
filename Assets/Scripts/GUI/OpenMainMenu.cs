using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenMainMenu : MonoBehaviour 
{
	public Image ButtonImageAvatar;

	void Start()
	{
		ButtonImageAvatar.sprite = Resources.Load<Sprite> (Service.Request.Player ().Avatar.ToString ());
	}

	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Menu);
	}
}
