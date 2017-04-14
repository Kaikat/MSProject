using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Login : MonoBehaviour {

	public InputField Username;
	public InputField Password;
	public Text ErrorLabel;

	// Update is called once per frame
	void Update () {
	
	}

	public void Click()
	{
		if (Service.Request.VerifyLogin (Username.text, Password.text)) 
		{
			ErrorLabel.text = "Login Successful";
			EventManager.TriggerEvent (GameEvent.LoginSuccessful);
			EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Home);
			StartGame.CurrentPlayer.LoadPlayer (Username.text);
		} 
		else 
		{
			ErrorLabel.text = "Wrong username or password!";
		}
	}
}
