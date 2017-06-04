using UnityEngine;
using System.Collections;
using GoMap;
using GoShared;
public class GOObject : MonoBehaviour {
	
	public GOMap map;
	public Coordinates coordinatesGPS;

	// Use this for initialization
	void Awake () {

		if (map == null) {
			Debug.LogWarning ("GOObject - Map property not set");
			return;
		}

		//register this class for location notifications
		map.locationManager.onOriginSet += LoadData;

	}

	void LoadData (Coordinates currentLocation) {//This is called when the origin is set

		map.dropPin (coordinatesGPS.latitude, coordinatesGPS.longitude, gameObject);

	}

}
