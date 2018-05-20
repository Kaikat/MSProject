using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour {

	public InputField Username;
	public InputField Password;
	public Text ErrorLabel;

	public void Click()
	{
		string result = Service.Request.VerifyLogin (Username.text, Password.text);
		if (result == "true")
		{
			ErrorLabel.text = "Login Successful";
			TextFile.Write (UIConstants.USERNAME_FILE, Username.text);

			Event.Request.TriggerEvent (GameEvent.LoginSuccessful);

			if (!Service.Request.Player ().Survey)
			{
				Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Survey);
			} 
			else if (Service.Request.Player ().Avatar == Avatar.Default)
			{
				Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Tutorial);
			} 
			else
			{
				Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
			}
		} 
		else if (result == "false")
		{
			ErrorLabel.text = "Wrong username or password!";
		} 
		else
		{
			ErrorLabel.text = "Please find internet access and try again.";
		}
	}
}
