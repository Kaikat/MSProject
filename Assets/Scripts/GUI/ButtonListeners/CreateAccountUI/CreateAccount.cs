using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateAccount : MonoBehaviour {

	public Text ErrorLabel;

	public void Click()
	{
		ErrorLabel.text = "";
		EventManager.TriggerEvent (GameEvent.CreateNewAccount, ScreenType.CreateAccount);
	}
}
