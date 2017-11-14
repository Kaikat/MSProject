using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VenueLoader : MonoBehaviour, IShowHideListener
{
	public GameObject VenueGrid;
	public TaggedShowHide VenueScreenTag;
	public Text NumberDiscoveredVenues;

	private const string PREFAB_FOLDER = "UIPrefabs";
	private const string VENUE_FAB = "VenueEntry";
	private List<Venue> venues;
	private List<GameObject> entries = new List<GameObject> ();

	void Awake()
	{
		VenueScreenTag.listener = this;
	}

	public void OnShow()
	{
		if (venues == null) 
		{
			venues = Service.Request.AllVenues ();
		}

		List<Venue> playerVenues = new List<Venue> ();

		int numberVisited = 0;
		foreach (Venue venue in venues)
		{
			if (Service.Request.Player ().HasDiscoveredAnimal (venue.Animal)) 
			{
				numberVisited++;
			}

			int recommendationIndex = Service.Request.Player ().GetRecommendationIndex (venue.Location);
			playerVenues.Add (new Venue (venue, recommendationIndex));
		}
		playerVenues.Sort ((x, y) => (x.Index).CompareTo (y.Index));

		NumberDiscoveredVenues.text = numberVisited.ToString () + "/" + playerVenues.Count;
		GameObject parentlessPrefab = AssetManager.LoadPrefab(PREFAB_FOLDER, VENUE_FAB) as GameObject;
		foreach (Venue venue in playerVenues)
		{
			GameObject entry = Instantiate(parentlessPrefab);
			entry.GetComponentInChildren<SetVenueEntry> ().SetVenueEntryElements (venue);
			entry.transform.SetParent (VenueGrid.transform);
			entry.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
			entry.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
			entries.Add (entry);
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
