using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CreateAccount : MonoBehaviour {

	public Text ErrorLabel;

	public void Click()
	{
		ErrorLabel.text = "";
		Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.CreateAccount);
	}
}
