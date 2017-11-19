using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadUsername : MonoBehaviour 
{
	public InputField UsernameInputField;

	void Awake ()
	{
		Event.Request.RegisterEvent <ScreenType>(GameEvent.SwitchScreen, UpdateUsername);
	}

	void Start () 
	{
		UsernameInputField.text = TextFile.Read (UIConstants.USERNAME_FILE);
	}

	public void UpdateUsername(ScreenType screen)
	{
		if (screen == ScreenType.Login)
		{
			UsernameInputField.text = TextFile.Read (UIConstants.USERNAME_FILE);
		}
	}

	void Destroy()
	{
		Event.Request.UnregisterEvent<ScreenType> (GameEvent.SwitchScreen, UpdateUsername);
	}
}
