using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GoShared;

//TODO: Work in progress: hard coded example for testing
public class ShowPath : MonoBehaviour 
{
	public Text PathButtonText;
	public GameObject PlayerAvatar;
	public Material Material;
	public UpdateGPSLocation PlayerGPSLocation;

	private List<GameObject> LinePath;
	private const string SHOW_PATH = "Show\nPath";
	private const string HIDE_PATH = "Hide\nPath";

	List<Vector2> pathPoints;
	public void Awake()
	{
		LinePath = new List<GameObject> ();
		AddPathPoints ();
	}

	public void Click()
	{
		if (PathButtonText.text == SHOW_PATH)
		{
			DrawPath ();
		} 
		else
		{
			HidePath ();
		}
		PathButtonText.text = PathButtonText.text == SHOW_PATH ? HIDE_PATH : SHOW_PATH;
	}

	public void AddPathPoints()
	{
		pathPoints = new List<Vector2> ();
		pathPoints.Add(new Vector2(34.41406f, -119.8474f));
		pathPoints.Add(new Vector2(34.41385f, -119.8473f));
		pathPoints.Add(new Vector2(34.41355f, -119.8473f)); 
		pathPoints.Add(new Vector2(34.41323f, -119.8474f)); 
		pathPoints.Add(new Vector2(34.41309f, -119.8476f)); 
		pathPoints.Add(new Vector2(34.41309f, -119.8478f)); 
		pathPoints.Add(new Vector2(34.41309f, -119.8479f)); 
		pathPoints.Add(new Vector2(34.41309f, -119.848f)); 
		pathPoints.Add(new Vector2(34.41295f, -119.848f)); 
		pathPoints.Add(new Vector2(34.41293f, -119.848f));
		pathPoints.Add(new Vector2(34.41277f, -119.848f));
		pathPoints.Add(new Vector2(34.41277f, -119.848f)); 
		pathPoints.Add(new Vector2(34.41277f, -119.8484f));
	}

	public List<Vector2> PotentialPlacesToVisit()
	{
		List<Vector2> potentialVisits = new List<Vector2> ();
		potentialVisits.Add(PlayerGPSLocation.GetCoordinate ());

		Dictionary<string, MajorLocationData> recommendations = Service.Request.Player ().GetRecommendations ();
		List<AnimalLocation> placesNotVisited = Service.Request.Player ().GetUndiscoveredPlaces ();

		List<MajorLocationData> notVisitedData = new List<MajorLocationData> ();
		foreach (AnimalLocation location in placesNotVisited)
		{
			notVisitedData.Add (recommendations [location.Location.LocationName]);
		}

		notVisitedData.Sort ((x, y) => (x.Index).CompareTo (y.Index));

		foreach (MajorLocationData location in notVisitedData)
		{
			if (potentialVisits.Count < UIConstants.TOP_LOCATIONS_TO_CHOOSE_FROM + 1)
			{
				potentialVisits.Add (GetCoordinateFor(location.Location, placesNotVisited));
			}
		}

		return potentialVisits;
	}

	private Vector2 GetCoordinateFor(string locationName, List<AnimalLocation> places)
	{
		foreach (AnimalLocation place in places)
		{
			if (place.Location.LocationName == locationName)
			{
				return place.Location.Coordinate;
			}
		}

		return new Vector2 ();
	}

	public void DrawPath()
	{
		List<Vector2> places = PotentialPlacesToVisit ();
		string result = "TO VISIT: ";
		foreach (Vector2 place in places)
		{
			result += place.x + ", " + place.y + "\n";
		}
		Debug.LogWarning (result);

		List<Vector2> pathToTake = DataManager.Data.RequestDirections (places);

		/*GameObject path = new GameObject();
		path.transform.position = PlayerAvatar.transform.position;
		path.AddComponent<LineRenderer>();
		LineRenderer lr = path.GetComponent<LineRenderer>();
		lr.material = Material;
		lr.SetColors(UIConstants.Red, UIConstants.Red);
		lr.SetWidth(2.0f, 2.0f);*/

		LineRenderer lineRenderer;
		GameObject line = MakeNewLineObject (out lineRenderer);




		lineRenderer.SetPosition(0, PlayerAvatar.transform.position);
		//y = 5.2
		//lr.SetPosition(1, new Vector3(PlayerAvatar.transform.position.x, PlayerAvatar.transform.position.y + 100.0f, PlayerAvatar.transform.position.z));


	
		//lr.SetPosition(1, new Vector3(-176.7539f, 5.2f, -209.0429f));
		//GameObject.Destroy(path, duration);


		int i = 1;
		foreach (Vector2 p in pathToTake)
		{
			Coordinates coordinates = new Coordinates (p.x, p.y, 0.0f);
			Vector3 coordinate = coordinates.convertCoordinateToVector ();
			lineRenderer.SetPosition (i, new Vector3 (coordinate.x, coordinate.y, coordinate.z));
			LinePath.Add (line);

			i = 0;
			line = MakeNewLineObject (out lineRenderer);
			lineRenderer.SetPosition (i, new Vector3 (coordinate.x, coordinate.y, coordinate.z));
			i++;
		}
		Destroy (line);
	}

	public GameObject MakeNewLineObject(out LineRenderer lr)
	{
		GameObject lineObject = new GameObject();
		lineObject.transform.position = PlayerAvatar.transform.position;
		lineObject.AddComponent<LineRenderer>();
		lr = lineObject.GetComponent<LineRenderer>();
		lr.material = Material;
		lr.SetColors(UIConstants.Red, UIConstants.Red);
		lr.SetWidth(2.0f, 2.0f);
		return lineObject;
	}

	public void HidePath()
	{
		foreach (GameObject line in LinePath)
		{
			line.SetActive (false);
		}

		for (int i = LinePath.Count - 1; i >= 0; i--)
		{
			Destroy (LinePath [i]);
		}
		LinePath.Clear ();
	}
}