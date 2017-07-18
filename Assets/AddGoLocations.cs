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

				//Top 7 recommendations will be color coded
				//top 4 = gold, next 3 = blue, rest = green
				//Already visited locations should be what color? gray? Or should it be a different asset?
				//Making the asset not appear would make it hard if someone wanted to catch the same animal again.
				go.GetComponentInChildren<BannerColor> ().SetBannerColor (Resources.Load<Texture> ("green"));
				for (int i = 0; i < playersRecommendations.Count; i++) 
				{
					if (playersRecommendations [i].Location == location.Location.LocationName) 
					{
						if (i < 3) 
						{
							go.GetComponentInChildren<BannerColor> ().SetBannerColor (Resources.Load<Texture> ("yellow"));
						} 
						else if (i <= 3 && i < 7) 
						{
							go.GetComponentInChildren<BannerColor> ().SetBannerColor (Resources.Load<Texture> ("blue"));
						} 
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
