using UnityEngine;
using System.Collections;

public class CheckForNearbyAnimals : MonoBehaviour {

	public GameObject MapImage;

	UpdateGPSLocation gpsScript;
	bool animalOnScreen;

	void Awake()
	{
		EventManager.RegisterEvent (GameEvent.GPSInitialized, Init);
		EventManager.RegisterEvent (GameEvent.Caught, AnimalCaught);
		animalOnScreen = false;
	}

	public void Init()
	{
		gpsScript = MapImage.GetComponent<UpdateGPSLocation> ();
	}

	public void AnimalCaught()
	{
		animalOnScreen = false;
	}

	// Update is called once per frame
	void Update () 
	{
		if (gpsScript == null || animalOnScreen)
		{
			return;
		}

		Vector2 currentLocation = gpsScript.GetCoordinate ();
		Vector2 limit = new Vector2 (34.41094f, -119.8639f);
		Vector2 testp = new Vector2 (34.41103f, -119.8638f);
		float allowedDistance = Vector2.Distance (limit, testp);

		float currentDistance = Vector2.Distance (testp, currentLocation);

		currentDistance = 0.0f;
		//distanceDebug.text = "allowedDistance: " + allowedDistance.ToString () + " currentDistance: " + currentDistance.ToString ();

		// Default/Debug Animal
		if (currentDistance < allowedDistance && !StartGame.CurrentPlayer.GetAnimals().ContainsKey(AnimalSpecies.Horse))
		{
			//AssetManager.ShowAnimal (AnimalSpecies.Horse);
			//AnimalToCatch = AnimalSpecies.Horse;
			//EventManager.TriggerEvent (GameEvent.CatchAnimal, ScreenType.CatchAnimal);
			EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.CatchAnimal);
			EventManager.TriggerEvent (GameEvent.AnimalEncounter, AnimalSpecies.Horse);

			//spawnHint.text = AnimalSpecies.Tiger.ToString ();
			//EventManager.TriggerEvent (GameEvent.AnimalEncounter, AnimalSpecies.Horse);

			animalOnScreen = true;
		}
	}

	void Destroy()
	{
		EventManager.UnregisterEvent (GameEvent.GPSInitialized, Init);
		EventManager.UnregisterEvent (GameEvent.Caught, AnimalCaught);
	}
}
