using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VersionButtons : MonoBehaviour 
{
	public Button Legend;
	public Button ShowPath;
	private bool buttonSetupDone = false;
	private readonly string LEGEND_COLORED_MAJORS = "LegendColoredMajors";
	private readonly string LEGEND_2_OPTIONS = "Legend2Options";
	 
	void Awake()
	{
		EventManager.RegisterEvent <ScreenType> (GameEvent.SwitchScreen, ShowGameVersionButtons);
	}

	public void ShowGameVersionButtons(ScreenType screen)
	{
		if (screen != ScreenType.GoMapHome || buttonSetupDone)
		{
			return;
		}

		//TODO: Check that locations are color coded appropriately in the Venues button
		//TODO: Kittykat has a null problem with showing the path but empty accounts do not
		//  NOTE: Empty accounts now do as well
		GameVersion version = Service.Request.Player().Username.GetGameVersion();
		switch (version)
		{
			case GameVersion.TrackVisits:
				ShowPath.gameObject.SetActive (false);
				Legend.image.overrideSprite = Resources.Load<Sprite> (LEGEND_2_OPTIONS);
				break;
			case GameVersion.ColorCodedMajors:
				ShowPath.gameObject.SetActive (false);
				Legend.image.overrideSprite = Resources.Load<Sprite> (LEGEND_COLORED_MAJORS);
				break;
			default:
				//TODO: REVERSE CHANGE LATER - default should be empty
				ShowPath.gameObject.SetActive(false);
				break;
		}
			
		buttonSetupDone = true;
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <ScreenType> (GameEvent.SwitchScreen, ShowGameVersionButtons);
	}
}
