using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenStreetMaps : MonoBehaviour
{
	public GameObject mapImage;

	public bool loadOnStart = true;
	public float latitude  = 34.4127f;
	public float longitude = -119.845f;
	public int zoom = 15;
	public int size = 600;

	void Start() {
		if(loadOnStart) Refresh();	
	}

	public void Refresh() {
		StartCoroutine(_Refresh());
	}

	IEnumerator _Refresh ()
	{
		//with markers
		//http://staticmap.openstreetmap.de/staticmap.php?center=48.1351253,11.5819806&zoom=14&size=1024x768&markers=48.1351253,11.5819806,ol-marker

		var url = "http://staticmap.openstreetmap.de/staticmap.php";
		//?center=34.4107,-119.8463&zoom=15&size=350x350
		var qs = "";
		qs += "center=" + WWW.UnEscapeURL (string.Format ("{0},{1}", latitude, longitude));
		qs += "&zoom=" + zoom.ToString ();
		qs += "&size=" + WWW.UnEscapeURL (string.Format ("{0}x{0}", size));

		var req = new WWW (url + "?" + qs);
		yield return req;

		Rect rec = new Rect(0, 0, size, size);
		mapImage.GetComponent<Image> ().sprite = Sprite.Create (req.texture, rec, new Vector2 (0.5f, 0.5f));
	}


}