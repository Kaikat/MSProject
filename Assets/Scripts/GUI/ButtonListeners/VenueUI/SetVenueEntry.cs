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

	public void SetVenueEntryElements(Venue venue)
	{
		VenueName.text = venue.Location;
		Description.text = venue.Description;

		for (int i = 0; i < venue.Majors.Count; i++) 
		{
			Majors.text += venue.Majors [i].ToString();
			if (i != venue.Majors.Count - 1) 
			{
				Majors.text += ", ";
			}
		}

		ImagePanel.color = UIConstants.Beige;
		DataPanel.color = UIConstants.Beige;
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
}
