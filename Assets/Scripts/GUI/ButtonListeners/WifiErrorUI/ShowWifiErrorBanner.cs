using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowWifiErrorBanner : MonoBehaviour, IShowHideListener
{
	public TaggedShowHide WifiErrorScreenTag;
	public GameObject UIWifiErrorBanner;

	//This solves a race conditions that shows up on device
	//This is for the case when the player loads the game
	//without an internet connection
	void Awake () 
	{
		WifiErrorScreenTag.listener = this;
	}
	
	public void OnShow()
	{
		UIWifiErrorBanner.SetActive (true);
	}

	public void OnHide()
	{
		UIWifiErrorBanner.SetActive (false);
	}
}
