using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CheckForNearbyAnimals : MonoBehaviour {

	public GameObject GpsScriptHolder;
	public RawImage MapImage;
	private UpdateGPSLocation gpsScript;
	private bool animalOnScreen;
	private bool allowUpdate;
	void Awake()
	{
		EventManager.RegisterEvent <ScreenType> (GameEvent.SwitchScreen, SetUpdate);
		EventManager.RegisterEvent (GameEvent.GPSInitialized, Init);
		EventManager.RegisterEvent <Animal> (GameEvent.AnimalCaught, SetAnimalOnScreenToFalse);
		animalOnScreen = false;
		allowUpdate = false;
	}

	public void Init()
	{
		//gpsScript = MapImage.GetComponent<UpdateGPSLocation> ();
		gpsScript = GpsScriptHolder.GetComponent<UpdateGPSLocation> ();
	}

	public void SetAnimalOnScreenToFalse(Animal animal)
	{
		animalOnScreen = false;
	}

	public void SetUpdate(ScreenType screen)
	{
		if (screen == ScreenType.GoMapHome) 
		{
			allowUpdate = true;
		} 
		else 
		{
			allowUpdate = false;
		}
	}

	// Update is called once per frame
	void Update () 
	{
		if (!allowUpdate || animalOnScreen)
		{
			return;
		}
			
		//float allowedDistanceRadius = 0.0001349778f;

		/*
		//TODO: Bring back the TEMPORARY REMOVAL OF THE TUTORIAL HORSE
		if (!Service.Request.Player ().HasDiscoveredAnimal (AnimalSpecies.Horse))
		{
			EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.CatchAnimal);
			EventManager.TriggerEvent (GameEvent.AnimalEncounter, AnimalSpecies.Horse);
			animalOnScreen = true;
		}
		else
		{*/
			Vector2 currentLocation = gpsScript.GetCoordinate ();

			Vector2 limit = new Vector2 (34.41094f, -119.8639f);
			Vector2 livingRoomPoint = new Vector2 (34.41103f, -119.8638f);
			float allowedDistance = Vector2.Distance (limit, livingRoomPoint); 

			List<AnimalLocation> AnimalLocations = Service.Request.PlacesToVisit ();
			for (int i = 0; i < AnimalLocations.Count; i++)
			{
				Vector2 testp = AnimalLocations [i].Location.Coordinate;
				float currentDistance = Vector2.Distance (testp, currentLocation);
				if (currentDistance < allowedDistance && !Service.Request.Player ().GetAnimals ().ContainsKey (AnimalLocations [i].Animal))
				{
					EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.CatchAnimal);
					EventManager.TriggerEvent (GameEvent.AnimalEncounter, AnimalLocations [i].Animal);
					animalOnScreen = true;
				}
			}
		//}

		/*
		float currentDistance = Vector2.Distance (testp, currentLocation);

		currentDistance = 0.0f;
		//distanceDebug.text = "allowedDistance: " + allowedDistance.ToString () + " currentDistance: " + currentDistance.ToString ();

		// Default/Debug Animal
		if (currentDistance < allowedDistance && !Service.Request.Player().GetAnimals().ContainsKey(AnimalSpecies.Horse))
		{
			EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.CatchAnimal);
			EventManager.TriggerEvent (GameEvent.AnimalEncounter, AnimalSpecies.Horse);
			Service.Request.Player ().AddDiscoveredAnimal (AnimalSpecies.Horse);
			animalOnScreen = true;
		}*/
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <ScreenType> (GameEvent.SwitchScreen, SetUpdate);
		EventManager.UnregisterEvent (GameEvent.GPSInitialized, Init);
		EventManager.UnregisterEvent <Animal> (GameEvent.AnimalCaught, SetAnimalOnScreenToFalse);
	}
}


//Vector2 currentLocation = new Vector2 (myGPSLocation.latitude, myGPSLocation.longitude);

//Check if close to a predefined location
//El Centro: 34.414207, -119.844517
//Girvetz Courtyard: 34.413632, -119.846974

//Test: 		34.41103, -119.8638
//Test Limit: 	34.41094, -119.8639
/*Vector2 limit = new Vector2 (34.41094f, -119.8639f);
		Vector2 testp = new Vector2 (34.41103f, -119.8638f);
		float allowedDistance = Vector2.Distance (limit, testp);

		currentDistance = Vector2.Distance (testp, currentLocation);

		currentDistance = 0.0f;
		distanceDebug.text = "allowedDistance: " + allowedDistance.ToString () + " currentDistance: " + currentDistance.ToString ();

		// Default/Debug Animal
		if (currentDistance < allowedDistance && !StartGame.CurrentPlayer.GetAnimals().ContainsKey(AnimalSpecies.Tiger))
		{
			AssetManager.ShowAnimal (AnimalSpecies.Horse);
			EventManager.TriggerEvent (GameEvent.CatchAnimal, ScreenType.CatchAnimal);
			//spawnHint.text = AnimalSpecies.Tiger.ToString ();
			EventManager.TriggerEvent (GameEvent.AnimalEncounter, AnimalSpecies.Horse);
		}
		*/

// Actual Places
/*Vector2 GirvetzCourtyard = new Vector2 (34.413632f, -119.846974f);
		Vector2 ElCentro = new Vector2 (34.414207f, -119.844517f);
		float allowedDistanceRadius = 0.0001349778f;

		if (Vector2.Distance (currentLocation, GirvetzCourtyard) < allowedDistance && !StartGame.CurrentPlayer.GetAnimals().ContainsKey(AnimalSpecies.Butterfly))
		{
			
			AssetManager.ShowAnimal (AnimalSpecies.Butterfly);
			EventManager.TriggerEvent (GameEvent.CatchAnimal, ScreenType.CatchAnimal);
			spawnHint.text = AnimalSpecies.Butterfly.ToString ();
		}

		if (Vector2.Distance (currentLocation, ElCentro) < allowedDistance && !StartGame.CurrentPlayer.GetAnimals().ContainsKey(AnimalSpecies.Tiger))
		{
			AssetManager.ShowAnimal (AnimalSpecies.Tiger);
			EventManager.TriggerEvent (GameEvent.CatchAnimal, ScreenType.CatchAnimal);
			spawnHint.text = AnimalSpecies.Tiger.ToString ();
		}*/