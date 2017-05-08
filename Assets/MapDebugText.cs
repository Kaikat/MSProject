using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapDebugText : MonoBehaviour {

	public Text DebugText;
	public Text DebugMapUrlText;

	public Text ButtonText;

	private const string show = "Show\nDebug\nText";
	private const string hide = "Hide\nDebug\nText";

	public void Click()
	{
		DebugText.enabled = !DebugText.enabled;
		DebugMapUrlText.enabled = !DebugMapUrlText.enabled;

		if (DebugText.enabled)
		{
			ButtonText.text = hide;
		}
		else
		{
			ButtonText.text = show;
		}
	}
}
