using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetVenueEntry : MonoBehaviour {

	public RawImage VenueImage;
	public Text VenueName;
	public Text Description;
	public Text Majors;

	public Image Background;
	public Image ImagePanel;
	public Image DataPanel;

	private readonly string VENUE_MESSAGE = "Discover more at this location!";

	public void SetVenueEntryElements(Venue venue)
	{
		VenueName.text = venue.Location;
		VenueImage.texture = Resources.Load<Texture> (UIConstants.VENUES_IMAGE_PATH + venue.Location);
		ImagePanel.color = UIConstants.Beige;
		DataPanel.color = UIConstants.Beige;

		if (!Service.Request.Player ().HasDiscoveredAnimal (venue.Animal))
		{
			Background.color = UIConstants.Gray;
			Description.text = VENUE_MESSAGE;
			return;
		}

		Description.text = venue.Description;
		for (int i = 0; i < venue.Majors.Count; i++) 
		{
			Majors.text += venue.Majors [i].ToString();
			if (i != venue.Majors.Count - 1) 
			{
				Majors.text += ", ";
			}
		}
			
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
