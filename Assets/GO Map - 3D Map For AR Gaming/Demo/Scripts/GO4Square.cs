using UnityEngine;
using System.Collections;

//This class uses Foursquare webservice API. 
//It's made for demo purpose only, and needs your personal foursquare API Key. 
//(No credit card is required, visit https://developer.foursquare.com/docs/venues/search)

//Full list of venues categories is given by this API https://api.foursquare.com/v2/venues/categories&oauth_token=

using GoShared;

namespace GoMap {
	
	public class GO4Square : MonoBehaviour {

		public GOMap goMap;
		public string baseUrl = "https://api.foursquare.com/v2/venues/search?v=20160914";
		public string categoryID;
		public string oauth_token;
		public GameObject prefab;
		public float queryRadius = 1000;

		Coordinates lastQueryCenter = null;

		//https://api.foursquare.com/v2/venues/search?ll=40.7,-74&radius=1000&v=20160914

		// Use this for initialization
		void Awake () {

			if (oauth_token.Length == 0) {
				Debug.LogWarning ("GO4Square - FORSQUARE OAUTH TOKEN IS REQUIRED, GET IT HERE: https://developer.foursquare.com/docs/venues/search");
				return;
			}

			//register this class for location notifications
			goMap.locationManager.onOriginSet += LoadData;
			goMap.locationManager.onLocationChanged += LoadData;

		}
			
		void LoadData (Coordinates currentLocation) {//This is called when the location changes

			if (lastQueryCenter == null || lastQueryCenter.DistanceFromPoint (currentLocation) >= queryRadius/1.5f) {
				lastQueryCenter = currentLocation;
				string url = baseUrl + "&ll=" + currentLocation.latitude + "," + currentLocation.longitude + "&radius=" + queryRadius+"&categoryId="+categoryID+"&oauth_token="+oauth_token;
				StartCoroutine (LoadPlaces(url));
			}
		}

		public IEnumerator LoadPlaces (string url) { //Request the API

			Debug.Log ("GO4Square URL: " + url);

			var www = new WWW(url);
			yield return www;

			ParseJob job = new ParseJob();
			job.InData = www.text;
			job.Start();

			yield return StartCoroutine(job.WaitFor());
		
			IDictionary response = (IDictionary)((IDictionary)job.OutData)["response"];
			IList results = (IList)response ["venues"];

			foreach (Transform child in transform) {
				GameObject.Destroy (child.gameObject);
			}

			foreach (IDictionary result in results) {//This example only takes GPS location and the name of the object. There's lot more, take a look at the Foursquare API documentation

				IDictionary location = ((IDictionary)result ["location"]);
				double lat = (double)location ["lat"];
				double lng = (double)location ["lng"];

	//			GameObject go = GameObject.Instantiate (prefab);
	//			go.name = (string)result["name"];
	//			goMap.dropPin (lat, lng, go);

				Coordinates coordinates = new Coordinates (lat, lng,0);
				GameObject go = GameObject.Instantiate (prefab);
				go.transform.localPosition = coordinates.convertCoordinateToVector(0);
				go.transform.parent = transform;
				go.name = (string)result["name"];

			}
		}
	}
}
