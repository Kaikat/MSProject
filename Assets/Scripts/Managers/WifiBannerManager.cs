using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WifiBannerManager : MonoBehaviour 
{
	public GameObject UIBanner;
	public GameObject GOBanner;

	void Awake()
	{
		Event.Request.RegisterEvent (GameEvent.WifiAvailable, HideBanners);
		Event.Request.RegisterEvent (GameEvent.WifiUnavailable, ShowBanners);
	}

	void Update()
	{
		WifiManager.Update ();
	}

	public void ShowBanners()
	{
		UIBanner.SetActive (true);
		GOBanner.SetActive (true);
	}

	public void HideBanners()
	{
		UIBanner.SetActive (false);
		GOBanner.SetActive (false);
	}

	void Destroy()
	{
		Event.Request.UnregisterEvent (GameEvent.WifiAvailable, ShowBanners);
		Event.Request.UnregisterEvent (GameEvent.WifiUnavailable, HideBanners);
	}
}
