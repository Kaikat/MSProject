using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CancelCreateAccount : MonoBehaviour {

	public Text ErrorLabel;

	public void Click()
	{
		ErrorLabel.text = "";
		EventManager.TriggerEvent (GameEvent.Login, ScreenType.Login);
	}
}
