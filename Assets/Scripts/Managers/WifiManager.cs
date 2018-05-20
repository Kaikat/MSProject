using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WifiManager
{
	private static bool WifiAvailable;

	static WifiManager()
	{
		WifiAvailable = true;
	}

	public static void Update()
	{
		if (!WifiAvailable)
		{
			Dictionary<Major, MajorData> majors = Service.Request.AllMajors ();
			if (majors != null)
			{
				SetWifiAvailability (true);
				Event.Request.TriggerEvent (GameEvent.WifiAvailable);
			}
		}
	}

	public static void SetWifiAvailability(bool available)
	{
		//Only send the event when availability changes
		if (WifiAvailable && !available)
		{
			WifiAvailable = available;
			Event.Request.TriggerEvent (GameEvent.WifiUnavailable);
		}
		else if (!WifiAvailable && available)
		{
			WifiAvailable = available;
			Event.Request.TriggerEvent (GameEvent.WifiAvailable);
		}
	}
}
