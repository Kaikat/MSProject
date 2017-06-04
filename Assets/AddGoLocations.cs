using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoShared;

public class AddGoLocations : MonoBehaviour 
{
	public GoMap.GOMap goMap;
	public GameObject prefab;

	void Start () 
	{
		List<AnimalLocation> locations = Service.Request.PlacesToVisit ();	

		foreach(AnimalLocation location in locations)
		{
			Coordinates coordinates = new Coordinates (location.Location.Coordinate.x, location.Location.Coordinate.y, 0);
			GameObject go = GameObject.Instantiate (prefab);
			go.transform.localPosition = coordinates.convertCoordinateToVector(0);
			go.transform.localScale = new Vector3 (2.0f, 2.0f, 2.0f);
			go.transform.parent = transform;
			go.name = location.Location.LocationName;
		}
	}
}
