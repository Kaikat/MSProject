using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ScreenManager : MonoBehaviour {

	public ScreenType ActiveScreen;
	public List<TaggedShowHide> Screens;

	private Dictionary<ScreenType, TaggedShowHide> screenMap;

	void Start ()
	{
		Screen.orientation = ScreenOrientation.Portrait;

		screenMap = new Dictionary<ScreenType, TaggedShowHide> ();

		foreach (TaggedShowHide screen in Screens)
		{
			screenMap [screen.Tag] = screen;
		}

		InitializeScreensWithRaceConditions ();
		screenMap [ActiveScreen].Show ();

		EventManager.RegisterEvent <ScreenType> (GameEvent.SwitchScreen, ShowScreen);
	}

	private void InitializeScreensWithRaceConditions()
	{
		List<ScreenType> screens = new List<ScreenType> ();
		screens.Add (ScreenType.CatchAnimal);
		screens.Add (ScreenType.Celebration);
		screens.Add (ScreenType.Failure);
		screens.Add (ScreenType.AnimalUnderObs);

		foreach (ScreenType screenType in screens) 
		{
			screenMap [screenType].Show ();
			screenMap [screenType].Hide ();
		}
	}

	public void ShowScreen(ScreenType screen)
	{
		Debug.LogWarning (screen.ToString ());
		//Hide previous screen and show active screen
		screenMap [ActiveScreen].Hide ();
		ActiveScreen = screen;
		screenMap [ActiveScreen].Show ();
	}

	public void Destroy()
	{
		EventManager.UnregisterEvent <ScreenType> (GameEvent.SwitchScreen, ShowScreen);
	}
}












































/*public void ChangeScreen(GameEvent gameEvent)
	{
		switch (gameEvent)
		{
			case GameEvent.Home:
			break;

			case GameEvent.CreateNewAccount:
			break;

			default:
				break;
		}
	}*/

//EventManager.RegisterEvent(GameEvent.Home, ()=>ShowScreen("Home"));
//EventManager.RegisterEvent(GameEvent.Home, ShowScreen);
/*EventManager.RegisterEvent(GameEvent.Junk, Junk);
		EventManager.RegisterEvent(GameEvent.LoginSuccessful, Destroy);
		EventManager.RegisterEvent(GameEvent.Home, Destroy);*/