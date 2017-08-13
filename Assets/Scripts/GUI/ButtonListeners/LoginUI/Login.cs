﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour {

	public InputField Username;
	public InputField Password;
	public Text ErrorLabel;

	public void Click()
	{
		if (Service.Request.VerifyLogin (Username.text, Password.text)) 
		{
			ErrorLabel.text = "Login Successful";
			TextFile.Write (UIConstants.USERNAME_FILE, Username.text);

			EventManager.TriggerEvent (GameEvent.LoginSuccessful);

			if (!Service.Request.Player ().Survey) 
			{
				EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Survey);
			}
			else if (Service.Request.Player ().Avatar == Avatar.Default)
			{
				EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Tutorial);
			}
			else
			{
				EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
			}
		} 
		else 
		{
			ErrorLabel.text = "Wrong username or password!";
		}
	}
}
