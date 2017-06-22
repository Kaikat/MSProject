using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoShared;

public class AddGoLocations : MonoBehaviour 
{
	public GoMap.GOMap goMap;
	public GameObject prefab;

	private List<AnimalLocation> locations;
	private bool stationsLoaded = false;

	void Awake()
	{
		EventManager.RegisterEvent <ScreenType> (GameEvent.SwitchScreen, LoadStations);
	}

	void Start () 
	{
		locations = Service.Request.PlacesToVisit ();	
	}

	public void LoadStations(ScreenType screen)
	{
		if (stationsLoaded) 
		{
			return;
		}

		if (screen == ScreenType.GoMapHome) 
		{
			foreach(AnimalLocation location in locations)
			{
				Coordinates coordinates = new Coordinates (location.Location.Coordinate.x, location.Location.Coordinate.y, 0.0f);
				GameObject go = GameObject.Instantiate (prefab);
				go.transform.localPosition = coordinates.convertCoordinateToVector();
				go.transform.localScale = new Vector3 (2.0f, 2.0f, 2.0f);
				go.transform.parent = transform;
				go.name = location.Location.LocationName;
			}

			stationsLoaded = true;
		}
	}

	void Destroy()
	{
		EventManager.UnregisterEvent<ScreenType> (GameEvent.SwitchScreen, LoadStations);
	}
}
