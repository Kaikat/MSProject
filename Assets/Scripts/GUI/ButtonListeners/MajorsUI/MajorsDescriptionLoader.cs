using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorsDescriptionLoader : MonoBehaviour, IShowHideListener
{
	public GameObject MajorsGrid;
	public TaggedShowHide MajorsScreenTag;

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
		}
		if (allMajorData == null)
		{
			allMajorData = Service.Request.AllMajors ();
		}

		List<Venue> playerVenues = new List<Venue> ();
		foreach (Venue venue in venues)
		{
			if (Service.Request.Player ().HasDiscoveredAnimal (venue.Animal)) 
			{
				int recommendationIndex = Service.Request.Player ().GetRecommendationIndex (venue.Location);
				playerVenues.Add (new Venue (venue, recommendationIndex));
			}
		}
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
