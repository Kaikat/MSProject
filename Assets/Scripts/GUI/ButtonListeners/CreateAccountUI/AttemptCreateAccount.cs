using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text.RegularExpressions;

public class AttemptCreateAccount : MonoBehaviour {

	public InputField Username;
	public InputField Name;
	public InputField Email;
	public InputField Password;

	public Text ErrorLabel;

	private int USERNAME_MIN_LENGTH = 8;
	private int NAME_MIN_LENGTH = 3;
	private int PASSWORD_MIN_LENGTH = 8;

	public void Click()
	{
		if (Username.text.Length < USERNAME_MIN_LENGTH) 
		{
			ErrorLabel.text = "Username must be at least " + USERNAME_MIN_LENGTH.ToString() +  " characters long.";
			return;
		}
		if (Name.text.Length < NAME_MIN_LENGTH) 
		{
			ErrorLabel.text = "Name must be at least " + NAME_MIN_LENGTH.ToString () + " characters long.";
			return;
		}
		if (Password.text.Length < PASSWORD_MIN_LENGTH) 
		{
			ErrorLabel.text = "Password must be at least " + PASSWORD_MIN_LENGTH.ToString () + " characters long.";
			return;
		}
		if (!ValidEmailAddress (Email.text))
		{
			ErrorLabel.text = "Invalid email address.";
			return;
		}

		string message = "";
		message = Service.Request.CreateAccount (Username.text, Name.text, Password.text, Email.text);
		ErrorLabel.text = message;

		if (message == "Account Created")
		{
			Username.text = "";
			Name.text = "";
			Email.text = "";
			Password.text = "";

			EventManager.TriggerEvent (GameEvent.Login, ScreenType.Login);
		}
	}

	public bool ValidEmailAddress(string email)
	{
		// https://forum.unity3d.com/threads/check-if-its-an-e-mail.73132/
		const string MatchEmailPattern =
			@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
            + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
              + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
            + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

		if (email != null)
		{
			return Regex.IsMatch (email, MatchEmailPattern);
		}

		return false;
	}
}
