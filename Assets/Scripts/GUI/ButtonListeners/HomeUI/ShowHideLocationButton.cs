using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShowHideLocationButton : MonoBehaviour {

	public Text ShowHideButtonLabel;

	public void Click()
	{
		ShowHideButtonLabel.text = ShowHideButtonLabel.text == "Hide\nLocation" ? "Show\nLocation" : "Hide\nLocation";
			
	}
}
