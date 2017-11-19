using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WifiBannerManager : MonoBehaviour 
{
	public GameObject UIBanner;
	public GameObject GOBanner;
	public GameObject BackgroundUIBanner;

	void Awake()
	{
		Event.Request.RegisterEvent (GameEvent.WifiAvailable, HideBanners);
		Event.Request.RegisterEvent (GameEvent.WifiUnavailable, ShowBanners);
	}

	public void ShowBanners()
	{
		UIBanner.SetActive (true);
		GOBanner.SetActive (true);
		BackgroundUIBanner.SetActive (true);
	}

	public void HideBanners()
	{
		UIBanner.SetActive (false);
		GOBanner.SetActive (false);
		BackgroundUIBanner.SetActive (false);
	}

	void Destroy()
	{
		Event.Request.UnregisterEvent (GameEvent.WifiAvailable, ShowBanners);
		Event.Request.UnregisterEvent (GameEvent.WifiUnavailable, HideBanners);
	}
}
