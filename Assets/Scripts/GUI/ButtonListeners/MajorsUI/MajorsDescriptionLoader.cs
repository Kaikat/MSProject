using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MajorsDescriptionLoader : MonoBehaviour, IShowHideListener
{
	public GameObject MajorsGrid;
	public TaggedShowHide MajorsScreenTag;

	public Text NumberOfDiscoveredMajors;
	private readonly string DISCOVERED_MAJORS = "Discovered ";

	private const string PREFAB_FOLDER = "UIPrefabs";
	private const string MAJOR_FAB = "MajorEntry";
	private List<Venue> venues;
	private List<GameObject> entries = new List<GameObject> ();
	private Dictionary<Major, MajorData> allMajorData;

	void Awake()
	{
		MajorsScreenTag.listener = this;
	}

	public void OnShow()
	{
		if (venues == null) 
		{
			venues = Service.Request.AllVenues ();
			if (venues == null)
			{
				Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Menu);
				return;
			}
		}
		if (allMajorData == null)
		{
			allMajorData = Service.Request.AllMajors ();
			if (allMajorData == null)
			{
				Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Menu);
				return;
			}
		}

		List<Venue> playerVenues = new List<Venue> ();
		List<Major> discoveredMajors = new List<Major> ();
		foreach (Venue venue in venues)
		{
			if (Service.Request.Player ().HasDiscoveredAnimal (venue.Animal)) 
			{
				int recommendationIndex = Service.Request.Player ().GetRecommendationIndex (venue.Location);
				playerVenues.Add (new Venue (venue, recommendationIndex));

				foreach (Major major in venue.Majors)
				{
					if (!discoveredMajors.Contains (major))
					{
						discoveredMajors.Add (major);
					}
				}
			}
		}

		NumberOfDiscoveredMajors.text = DISCOVERED_MAJORS + discoveredMajors.Count.ToString() + "/" + Major.GetNames(typeof(Major)).Length;	
		playerVenues.Sort ((x, y) => (x.Index).CompareTo (y.Index));

		GameObject parentlessPrefab = AssetManager.LoadPrefab(PREFAB_FOLDER, MAJOR_FAB) as GameObject;
		foreach (Venue venue in playerVenues) 
		{
			List<Major> majorsAtVenue = venue.Majors;

			foreach (Major major in majorsAtVenue)
			{
				if (!allMajorData.ContainsKey (major))
				{
					Debug.LogError ("Major Key: " + major.ToString () + " NOT FOUND!");
					continue;
				}

				GameObject entry = Instantiate(parentlessPrefab);
				entry.GetComponentInChildren<SetMajorEntry> ().SetMajorEntryElements (allMajorData[major], venue);
				entry.transform.SetParent (MajorsGrid.transform);
				entry.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
				entry.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
				entries.Add (entry);
			}
		}
	}

	public void OnHide()
	{
		foreach (GameObject entry in entries)
		{
			GameObject.Destroy(entry);
		}

		entries.Clear();
	}
}
