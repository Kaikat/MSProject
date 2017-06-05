using UnityEngine;
using System.Collections;

//This class uses Google Places webservice API. 
//It's made for demo purpose only, and needs your personal Google Developer API Key. 
//(No credit card is required, visit https://developers.google.com/places/web-service/intro)

using GoShared;
namespace GoMap
{

	public class GOPlaces : MonoBehaviour {

		public GOMap goMap;
		public string baseUrl = "https://maps.googleapis.com/maps/api/place/radarsearch/json?";
		public string type;
		public string googleAPIkey;
		public GameObject prefab;
		public float queryRadius = 3000;

		Coordinates lastQueryCenter = null;

		// Use this for initialization
		void Awake () {

			if (googleAPIkey.Length == 0) {
				Debug.LogWarning ("GOPlaces - GOOGLE API KEY IS REQUIRED, GET iT HERE: https://developers.google.com/places/web-service/intro");
				return;
			}

			//register this class for location notifications
			goMap.locationManager.onOriginSet += LoadData;
			goMap.locationManager.onLocationChanged += LoadData;

		}
			
		void LoadData (Coordinates currentLocation) {//This is called when the location changes

			if (lastQueryCenter == null || lastQueryCenter.DistanceFromPoint (currentLocation) >= queryRadius/1.5f) { //Do the request only if approaching the limit of the previous one
				lastQueryCenter = currentLocation;
				string url = baseUrl + "location="+currentLocation.latitude+","+currentLocation.longitude+"&radius="+queryRadius+"&type="+type+"&key="+googleAPIkey;
				StartCoroutine (LoadPlaces(url));
			}
		}

		public IEnumerator LoadPlaces (string url) { //Request the API

			Debug.Log ("GO PLACES URL: " + url);

			var www = new WWW(url);
			yield return www;

			ParseJob job = new ParseJob();
			job.InData = www.text;
			job.Start();

			yield return StartCoroutine(job.WaitFor());
		
			IDictionary response = (IDictionary)job.OutData;

			IList results = (IList)response ["results"];

			foreach (Transform child in transform) {
				GameObject.Destroy (child.gameObject);
			}


			foreach (IDictionary result in results) { //This example only takes GPS location and the id of the object. There's lot more, take a look at the places API documentation

				IDictionary location = (IDictionary)((IDictionary)result ["geometry"])["location"];
				double lat = (double)location ["lat"];
				double lng = (double)location ["lng"];

	//			GameObject go = GameObject.Instantiate (prefab);
	//			go.name = (string)result["place_id"];
	//			goMap.dropPin (lat, lng, go);

				Coordinates coordinates = new Coordinates (lat, lng,0);
				GameObject go = GameObject.Instantiate (prefab);
				go.transform.localPosition = coordinates.convertCoordinateToVector(0);
				go.transform.parent = transform;
				go.name = (string)result["place_id"];

			}

		}


	}
}
