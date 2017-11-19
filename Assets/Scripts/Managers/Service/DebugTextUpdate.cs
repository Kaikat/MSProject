using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugTextUpdate : MonoBehaviour 
{
	public Text DebugText;
	void Update () 
	{
		DebugText.text = DebugLog.message;
	}
}
