using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetMajorEntry : MonoBehaviour {

	public Text MajorName;
	public Text Description;
	public Text Venue;

	public Image Background;
	public Image DataPanel;

	public void SetMajorEntryElements(MajorData majorData, Venue venue)
	{
		MajorName.text = majorData.Name;
		Description.text = majorData.Description;
		Venue.text = venue.Location;

		DataPanel.color = UIConstants.Beige;

		GameVersion version = Service.Request.Player ().Username.GetGameVersion();
		switch (version)
		{
			case GameVersion.TrackVisits:
				ColorForTrackedVisits(venue);
				break;
			case GameVersion.ColorCodedMajors:
				ColorCodeByMajors(venue);
				break;
			default:
				ColorForFullVersionVenues (venue);
				break;
		}
	}

	private void ColorForFullVersionVenues(Venue venue)
	{
		int recommendationIndex = Service.Request.Player ().GetRecommendationIndex (venue.Location);
		if (recommendationIndex < UIConstants.Recommended) 
		{
			Background.color = UIConstants.Yellow;
		} 
		else if (recommendationIndex < UIConstants.SomewhatRecommended && recommendationIndex >= UIConstants.Recommended) 
		{
			Background.color = UIConstants.Blue;
		} 
		else
		{
			Background.color = UIConstants.Green;
		}
	}

	private void ColorForTrackedVisits(Venue venue)
	{
		List<AnimalLocation> animalLocations = Service.Request.Player ().GetDiscoveredPlaces ();

		if (animalLocations.FindIndex (f => f.Location.LocationName == venue.Location) != -1)
		{
			Background.color = UIConstants.Yellow;
		}
	}

	private void ColorCodeByMajors(Venue venue)
	{
		//NOTE: This assumes that locations will only have the same type of major, for example,
		//		2+ STEM majors at one location - not a combination of different types.
		if (GameConstants.STEM.Contains (venue.Majors [0]))
		{
			Background.color = UIConstants.Yellow;
		} 
		else if (GameConstants.SocialSciences.Contains (venue.Majors [0]))
		{
			Background.color = UIConstants.Blue;	
		}
		else
		{
			Background.color = UIConstants.Green;
		}
	}
}
