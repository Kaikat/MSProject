using UnityEngine;
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
			EventManager.TriggerEvent (GameEvent.LoginSuccessful);

			if (Service.Request.Player ().Avatar == Avatar.Default)
			{
				EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Gender);
			}
			else
			{
				//EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Home);
				EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
			}
		} 
		else 
		{
			ErrorLabel.text = "Wrong username or password!";
		}
	}
}
