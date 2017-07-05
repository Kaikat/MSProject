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
			List<MajorLocation> playersRecommendations = Service.Request.GetRecommendations ();

			foreach(AnimalLocation location in locations)
			{
				Coordinates coordinates = new Coordinates (location.Location.Coordinate.x, location.Location.Coordinate.y, 0.0f);
				GameObject go = GameObject.Instantiate (prefab);
				Vector3 coordinate = coordinates.convertCoordinateToVector();
				go.transform.localPosition = new Vector3 (coordinate.x, coordinate.y + 5.0f, coordinate.z);
				go.transform.parent = transform;
				go.name = location.Location.LocationName;

				foreach (MajorLocation loc in playersRecommendations) 
				{
					if (loc.Location == location.Location.LocationName) 
					{
						var texture = new Texture2D(2, 2, TextureFormat.ARGB32, false);

						texture.SetPixel(0, 0, Color.yellow);
						texture.SetPixel(1, 0, Color.yellow);
						texture.SetPixel(0, 1, Color.yellow);
						texture.SetPixel(1, 1, Color.yellow);
						texture.Apply();
						go.GetComponent<Renderer>().material.mainTexture = texture;
					}
				}
			}

			stationsLoaded = true;
		}
	}

	void Destroy()
	{
		EventManager.UnregisterEvent<ScreenType> (GameEvent.SwitchScreen, LoadStations);
	}
}
