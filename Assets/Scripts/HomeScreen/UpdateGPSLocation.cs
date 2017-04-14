using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateGPSLocation : MonoBehaviour
{
	public Text debugText;
	public Text distanceDebug;

	LocationInfo myGPSLocation;
	float fiveSecondCounter = 0.0f;

	float currentDistance;

	IEnumerator Start()
	{
		debugText.text += "Starting the GPS Script\n";
		EventManager.TriggerEvent (GameEvent.GPSInitialized);

		#if UNITY_ANROID
		Input.compass.enabled = true;
		#endif

		return InitializeGPSServices ();
	}

	IEnumerator InitializeGPSServices()
	{
		// First, check if user has location service enabled
		if (!Input.location.isEnabledByUser) {
			debugText.text += "GPS disabled by user\n";
			yield break;
		}

		// Start service before querying location
		Input.location.Start(0.1f, 0.1f);

		// Wait until service initializes
		int maxWait = 20;
		while (Input.location.status == LocationServiceStatus.Initializing && maxWait > 0)
		{
			yield return new WaitForSeconds(1);
			maxWait--;
		}

		// Service didn't initialize in 20 seconds
		if (maxWait < 1)
		{
			debugText.text += "Timed out\n";
			yield break;
		}
	}

	void Update()
	{
		fiveSecondCounter += Time.deltaTime;
		if (fiveSecondCounter > 5.0)
		{
			UpdateGPS ();
			fiveSecondCounter = 0.0f;
		}
	}

	void UpdateGPS()
	{
		// Connection has failed
		if (Input.location.status == LocationServiceStatus.Failed)
		{
			debugText.text += "Unable to determine device location\n";
			Input.location.Stop();
			Start ();
		}
		else
		{
			debugText.text += getUpdatedGPSstring();
		}
	}

	string getUpdatedGPSstring()
	{
		myGPSLocation = Input.location.lastData;
		return "Location: " + myGPSLocation.latitude + " " + myGPSLocation.longitude + " " + myGPSLocation.altitude + " " + myGPSLocation.horizontalAccuracy + " " + myGPSLocation.timestamp + "\n"; 
	}

	public Vector2 GetCoordinate()
	{
		return new Vector2 (myGPSLocation.latitude, myGPSLocation.longitude);
	}

	// For the IShowHideListener from the HomeUIObject
	public void OnShow()
	{
		InitializeGPSServices ();
	}

	public void OnHide()
	{
		// Stop service if there is no need to query location updates continuously
		Input.location.Stop ();
		debugText.text = "";

		// Make sure it immediately updates when the screen shows again
		fiveSecondCounter = 5.1f;
	}
}
