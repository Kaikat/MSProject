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
			if (!Service.Request.Player ().Survey)
			{
				return;
			}

			LoadProperGameVersion ();
			stationsLoaded = true;
		}
	}

	void Destroy()
	{
		EventManager.UnregisterEvent<ScreenType> (GameEvent.SwitchScreen, LoadStations);
	}

	private void LoadProperGameVersion()
	{
		GameVersion version = Service.Request.Player ().Username.GetGameVersion ();
		switch (version)
		{
			case GameVersion.TrackVisits:
				LoadUncoloredVersion ();
				break;
			case GameVersion.ColorCodedMajors:
				LoadMajorColorCodesVersion ();
				break;
			default:
				LoadFullVersion ();
				break;
		}
	}

	private void LoadFullVersion()
	{
		Dictionary<string, MajorLocationData>  playersRecommendations = Service.Request.Player().GetRecommendations();

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
			if (Service.Request.Player ().isAnimalOwned (location.Animal) ||
				Service.Request.Player ().hasReleasedAnimal (location.Animal)) 
			{
				go.GetComponentInChildren<BannerColor> ().SetBannerColor (Resources.Load<Texture> ("gray"));
			} 
			else if(playersRecommendations.ContainsKey(location.Location.LocationName))
			{
				if (playersRecommendations [location.Location.LocationName].Index < UIConstants.Recommended) 
				{
					go.GetComponentInChildren<BannerColor> ().SetBannerColor (Resources.Load<Texture> ("yellow"));
				}
				else if (playersRecommendations[location.Location.LocationName].Index >= UIConstants.Recommended && 
					playersRecommendations[location.Location.LocationName].Index < UIConstants.SomewhatRecommended)
				{
					go.GetComponentInChildren<BannerColor> ().SetBannerColor (Resources.Load<Texture> ("blue"));
				}
				else 
				{
					go.GetComponentInChildren<BannerColor> ().SetBannerColor (Resources.Load<Texture> ("green"));
				}
			}
			else
			{
				Debug.LogWarning ("ERROR KEY *" + location.Location.LocationName + "* was not in dictionary");
			}
		}
	}

	private void LoadMajorColorCodesVersion()
	{
		//STEM = 10, Social Science = 4, Humanities = 11
		List<Major> STEM = new List<Major> { Major.Architecture, Major.Biology, Major.Chemistry, 
			Major.ComputerScience, Major.EarthSciences, Major.EnvironmentalScience,
			Major.MarineBiology, Major.MarineScience, Major.MediaArtsAndTechnology,
			Major.Physics };
		List<Major> socialSciences = new List<Major> { Major.Anthropology, Major.Counseling, 
			Major.Psychology, Major.Sociology };
		List<Major> humanities = new List<Major> { Major.Art, Major.ArtHistory, Major.Athletics, Major.Communications, 
			Major.Dance, Major.Education, Major.EthnicStudies, Major.History, Major.Literature, 
			Major.Music, Major.Theater };

		Dictionary<string, List<Major>> majorLocations = Service.Request.GetMajorsAtLocation ();

		foreach (AnimalLocation location in locations)
		{
			Coordinates coordinates = new Coordinates (location.Location.Coordinate.x, location.Location.Coordinate.y, 0.0f);
			GameObject go = GameObject.Instantiate (prefab);
			Vector3 coordinate = coordinates.convertCoordinateToVector();
			go.transform.localPosition = new Vector3 (coordinate.x, coordinate.y + 5.0f, coordinate.z);
			go.transform.parent = transform;
			go.name = location.Location.LocationName;

			//NOTE: This assumes that every location only as majors of the same type, for example,
			//		2+ STEM majors at one spot, not a combination of major types
			if (Service.Request.Player ().isAnimalOwned (location.Animal) ||
			    Service.Request.Player ().hasReleasedAnimal (location.Animal))
			{
				go.GetComponentInChildren<BannerColor> ().SetBannerColor (Resources.Load<Texture> ("gray"));
			} 
			else if (STEM.Contains(majorLocations[location.Location.LocationName][0]))
			{
				go.GetComponentInChildren<BannerColor> ().SetBannerColor (Resources.Load<Texture> ("yellow"));
			}
			else if (socialSciences.Contains(majorLocations[location.Location.LocationName][0]))
			{
				go.GetComponentInChildren<BannerColor>().SetBannerColor (Resources.Load<Texture>("blue"));
			}
			else
			{
				go.GetComponentInChildren<BannerColor>().SetBannerColor(Resources.Load<Texture>("green"));
			}
		}
	}

	private void LoadUncoloredVersion()
	{
		foreach (AnimalLocation location in locations)
		{
			Coordinates coordinates = new Coordinates (location.Location.Coordinate.x, location.Location.Coordinate.y, 0.0f);
			GameObject go = GameObject.Instantiate (prefab);
			Vector3 coordinate = coordinates.convertCoordinateToVector();
			go.transform.localPosition = new Vector3 (coordinate.x, coordinate.y + 5.0f, coordinate.z);
			go.transform.parent = transform;
			go.name = location.Location.LocationName;
			if (Service.Request.Player ().isAnimalOwned (location.Animal) ||
				Service.Request.Player ().hasReleasedAnimal (location.Animal))
			{
				go.GetComponentInChildren<BannerColor> ().SetBannerColor (Resources.Load<Texture> ("gray"));
			} 
			else
			{
				go.GetComponentInChildren<BannerColor>().SetBannerColor(Resources.Load<Texture>("yellow"));
			}
		}
	}
}
