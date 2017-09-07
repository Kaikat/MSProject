using UnityEngine;
using UnityEngine.UI;
using System;
using System.Text.RegularExpressions;

public class AttemptCreateAccount : MonoBehaviour {

	public InputField Username;
	public InputField Name;
	public InputField Email;
	public InputField Password;
    public Text Gender;
    public Text Day;
    public Text Month;
    public Text Year;

	public Text ErrorLabel;

	private int USERNAME_MIN_LENGTH = 2;
	private int USERNAME_MAX_LENGTH = 20;
	private int NAME_MIN_LENGTH = 3;
	private int NAME_MAX_LENGTH = 20;
	private int PASSWORD_MIN_LENGTH = 2;
	private int PASSWORD_MAX_LENGTH = 20;
	private int EMAIL_MAX_LENGTH = 40;

	public void Click()
	{
		if (Username.text.Length < USERNAME_MIN_LENGTH) 
		{
			ErrorLabel.text = "Username must be at least " + USERNAME_MIN_LENGTH.ToString() +  " characters long.";
			return;
		}
		if (Username.text.Length > USERNAME_MAX_LENGTH) 
		{
			ErrorLabel.text = "Username must be less than " + USERNAME_MAX_LENGTH.ToString () + " characters long.";
			return;
		}
		if (Name.text.Length < NAME_MIN_LENGTH) 
		{
			ErrorLabel.text = "Name must be at least " + NAME_MIN_LENGTH.ToString () + " characters long.";
			return;
		}
		if (Name.text.Length > NAME_MAX_LENGTH)
		{
			ErrorLabel.text = "Name must be less than " + NAME_MAX_LENGTH.ToString () + " characters long.";
			return;
		}
		if (Password.text.Length < PASSWORD_MIN_LENGTH) 
		{
			ErrorLabel.text = "Password must be at least " + PASSWORD_MIN_LENGTH.ToString () + " characters long.";
			return;
		}
		if (Password.text.Length > PASSWORD_MAX_LENGTH)
		{
			ErrorLabel.text = "Password must be less than " + PASSWORD_MAX_LENGTH.ToString () + " characters long.";
			return;
		}
		if (Email.text.Length > EMAIL_MAX_LENGTH)
		{
			ErrorLabel.text = "Email must be less than " + EMAIL_MAX_LENGTH.ToString () + " characters long.";
			return;
		}
		if (!ValidEmailAddress (Email.text))
		{
			ErrorLabel.text = "Invalid email address.";
			return;
		}
		if (Month.text == "Month")
		{
			ErrorLabel.text = "Invalid Month";
			return;
		}
		if (Day.text == "Day")
		{
			ErrorLabel.text = "Invalid Day";
			return;
		}
		if (Year.text == "Year")
		{
			ErrorLabel.text = "Invalid Year";
			return;
		}

		string message = "";

		//Remove later
		string debugMessage = "Username: " + Username.text + "\n";
		debugMessage += "Name: " + Name.text + "\n";
		debugMessage += "Password: " + Password.text + "\n";
		debugMessage += "Email: " + Email.text + "\n";
		debugMessage += "Gender: " + Gender.text + "\n";
		debugMessage += "Month: " + Month.text + "\n";
		debugMessage += "Day: " + Day.text + "\n";
		debugMessage += "Year: " + Year.text + "\n";
		Debug.LogWarning (debugMessage);
		//end remove

		message = Service.Request.CreateAccount(Username.text,
                                                Name.text,
                                                Password.text,
                                                Email.text,
                                                Gender.text,
                                                new DateTime(Int32.Parse(Year.text), Int32.Parse(Month.text), Int32.Parse(Day.text)));
		ErrorLabel.text = message;

		if (message == "Account Created")
		{
			TextFile.Write (UIConstants.USERNAME_FILE, Username.text);

			Username.text = "";
			Name.text = "";
			Email.text = "";
			Password.text = "";

			EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Login);
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
