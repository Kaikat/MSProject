using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

public class GoogleMaps : MonoBehaviour
{
	public enum MapType
	{
		RoadMap,
		Satellite,
		Terrain,
		Hybrid
	}

	public RawImage MapImage;

	public bool loadOnStart = true;
	public bool autoLocateCenter = true;
	public GoogleMapLocation centerLocation;
	public int zoom = 13;
	public MapType mapType;
	public int size = 512;
	public bool doubleResolution = false;
	public GoogleMapMarker[] markers;
	public GoogleMapPath[] paths;

	public Text DebugText;

	void Start() {
		SetMarkers ();
		if(loadOnStart) Refresh();	
	}

	public void Refresh() {
		if(autoLocateCenter && (markers.Length == 0 && paths.Length == 0)) {
			Debug.LogError("Auto Center will only work if paths or markers are used.");	
		}
		StartCoroutine(_Refresh());
	}

	IEnumerator _Refresh ()
	{
		var url = "https://maps.googleapis.com/maps/api/staticmap";
		//var url = "https://maps.googleapis.com/maps/api/js?key=AIzaSyDY0kkJiTPVd2U7aTOAwhc9ySH6oHxOIYM&sensor=false";
		 
		var qs = "";
		if (!autoLocateCenter) {
			if (centerLocation.address != "")
				qs += "center=" + WWW.UnEscapeURL (centerLocation.address);
			else {
				qs += "center=" + WWW.UnEscapeURL (string.Format ("{0},{1}", centerLocation.latitude, centerLocation.longitude));
			}

			qs += "&zoom=" + zoom.ToString ();
		}
		qs += "&size=" + WWW.UnEscapeURL (string.Format ("{0}x{0}", size));
		qs += "&scale=" + (doubleResolution ? "2" : "1");
		qs += "&maptype=" + mapType.ToString ().ToLower ();
		var usingSensor = false;
		#if UNITY_IPHONE
		usingSensor = Input.location.isEnabledByUser && Input.location.status == LocationServiceStatus.Running;
		#endif
		qs += "&sensor=" + (usingSensor ? "true" : "false");

		foreach (var i in markers) {
			qs += "&markers=" + string.Format ("size:{0}|color:{1}|label:{2}", i.size.ToString ().ToLower (), i.color, i.label);
			foreach (var loc in i.locations) {
				if (loc.address != "")
					qs += "|" + WWW.UnEscapeURL (loc.address);
				else
					qs += "|" + WWW.UnEscapeURL (string.Format ("{0},{1}", loc.latitude, loc.longitude));
			}
		}

		foreach (var i in paths) {
			qs += "&path=" + string.Format ("weight:{0}|color:{1}", i.weight, i.color);
			if(i.fill) qs += "|fillcolor:" + i.fillColor;
			foreach (var loc in i.locations) {
				if (loc.address != "")
					qs += "|" + WWW.UnEscapeURL (loc.address);
				else
					qs += "|" + WWW.UnEscapeURL (string.Format ("{0},{1}", loc.latitude, loc.longitude));
			}
		}
			
		UnityWebRequest web_request = UnityWebRequest.GetTexture(url + "?" + qs);
		yield return web_request.Send();


		DebugText.text = web_request.url;

		//interpolated strings?
		//Debug.LogWarning ("~~~~~~~ " + web_request.error + " ~~~~~~");

		MapImage.texture = ((DownloadHandlerTexture)web_request.downloadHandler).texture;
	}

	private void SetMarkers()
	{
		List<AnimalLocation> placesToVisit = Service.Request.PlacesToVisit ();
		markers = new GoogleMapMarker[placesToVisit.Count];

		Dictionary<AnimalSpecies, List<Animal>> releasedAnimals = Service.Request.Player ().GetReleasedAnimals ();
		Dictionary<AnimalSpecies, List<Animal>> ownedAnimals = Service.Request.Player ().GetAnimals ();

		for(int i = 0; i < placesToVisit.Count; i++)
		{
			markers [i] = new GoogleMapMarker ();
			markers [i].label = "";
			//markers [i].label = placesToVisit [i].Location.LocationName.Replace(' ', '+');
			markers [i].size = GoogleMapMarker.GoogleMapMarkerSize.Small;

			Vector2 coordinate = placesToVisit [i].Location.Coordinate;
			markers [i].locations = new GoogleMapLocation[1];
			markers [i].locations [0] = new GoogleMapLocation ();
			markers [i].locations [0].address = "";
			markers [i].locations [0].latitude = coordinate.x;
			markers [i].locations [0].longitude = coordinate.y;

			if (releasedAnimals.ContainsKey (placesToVisit [i].Animal))
			{
				markers [i].color = GoogleMapColor.green;
			}
			else if (ownedAnimals.ContainsKey (placesToVisit [i].Animal))
			{
				markers [i].color = GoogleMapColor.yellow;
			}
			else if (Service.Request.Player().HasDiscoveredAnimal(placesToVisit[i].Animal))
			{
				markers [i].color = GoogleMapColor.orange;
			}
			else
			{
				markers [i].color = GoogleMapColor.red;
			}
		}
	}

}

public enum GoogleMapColor
{
	black,
	brown,
	green,
	purple,
	yellow,
	blue,
	gray,
	orange,
	red,
	white
}

[System.Serializable]
public class GoogleMapLocation
{
	public string address;
	public float latitude;
	public float longitude;
}

[System.Serializable]
public class GoogleMapMarker
{
	public enum GoogleMapMarkerSize
	{
		Tiny,
		Small,
		Mid
	}
	public GoogleMapMarkerSize size;
	public GoogleMapColor color;
	public string label;
	public GoogleMapLocation[] locations;

}

[System.Serializable]
public class GoogleMapPath
{
	public int weight = 5;
	public GoogleMapColor color;
	public bool fill = false;
	public GoogleMapColor fillColor;
	public GoogleMapLocation[] locations;	
}


//GOOD: https://maps.googleapis.com/maps/api/staticmap?center=34.4127,-119.845&zoom=15&size=512x512&scale=2&maptype=roadmap&sensor=false&markers=size:small|color:red|label:|34.4122,-119.8484&markers=size:small|color:red|label:|34.41296,-119.8467&markers=size:small|color:red|label:|34.41496,-119.8499&markers=size:small|color:orange|label:|34.41366,-119.8489&markers=size:small|color:orange|label:|34.40979,-119.846&markers=size:small|color:red|label:|413453,-119.8503&markers=size:small|color:red|label:|34.41182,-119.849&markers=size:small|color:red|label:|34.41153,-119.8509&markers=size:small|color:red|label:|34.41117,-119.8481&markers=size:small|color:red|label:|34.41441,-119.8449&markers=size:small|color:red|label:|34.41351,-119.8412&markers=size:small|color:red|label:|413209,-119.842&markers=size:small|color:red|label:|34.41517,-119.8402&markers=size:small|color:green|label:|34.41711,-119.8505&markers=size:small|color:red|label:|34.41274,-119.8484&markers=size:small|color:orange|label:|34.40723,-119.8434&markers=size:small|color:red|label:|34.4136,-119.8436&markers=size:small|color:red|label:|34.40752,-119.8436&markers=size:small|color:red|label:|34.41406,-119.8474&markers=size:small|color:red|label:|34.41489,-119.8446&markers=size:small|color:orange|label:|34.4111,-119.8639
//http://maps.googleapis.com/maps/api/staticmap?center=34.4127,-119.845&zoom=15&size=512x512&scale=2&maptype=roadmap&sensor=false&markers=size:small|color:red|label:|34.41421,-119.8445|34.41362,-119.847&markers=size:small|color:orange|label:|34.41421,-119.8445|34.41362,-119.847&markers=size:small|color:yellow|label:|34.41421,-119.8445|34.41362,-119.847&markers=size:small|color:green|label:|34.41421,-119.8445|34.41362,-119.847
