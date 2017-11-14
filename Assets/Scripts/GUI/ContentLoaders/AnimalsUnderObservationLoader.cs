using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalsUnderObservationLoader : MonoBehaviour, IShowHideListener 
{
	//public Camera BackgroundGUICamera;
	public GameObject AnimalGrid;
    public GameObject UIObjectScreen;

    private AnimalSpecies animalSpecies;
    private bool isAnimalSpeciesSet = false;

    private List<GameObject> entries = new List<GameObject> ();
	private List<GameObject> animalModels = new List<GameObject>();

	private readonly string ANIMAL_CARD = "AnimalCard";
	private const string PREFAB_FOLDER = "UIPrefabs";

	Dictionary<AnimalSpecies, Vector3> AnimalScales;
	Dictionary<AnimalSpecies, Vector3> AnimalRotations;
	Dictionary<AnimalSpecies, Vector3> AnimalPositions;

    void Awake()
    {
        EventManager.RegisterEvent<AnimalSpecies>(GameEvent.ViewingAnimalsUnderObservation, SetAnimalSpecies);
        UIObjectScreen.GetComponent<TaggedShowHide>().listener = this;
		SetPositions ();
		SetAnimalScales ();
		SetAnimalRotations ();
    }

    void Destroy()
    {
        EventManager.UnregisterEvent<AnimalSpecies>(GameEvent.ViewingAnimalsUnderObservation, SetAnimalSpecies);
    }

	public void OnHide()
	{
		foreach (GameObject entry in entries)
		{
			GameObject.Destroy(entry);
		}
		entries.Clear();

		foreach (GameObject animal in animalModels) 
		{
			GameObject.Destroy (animal);
		}
		animalModels.Clear ();
		isAnimalSpeciesSet = false;
	}

    public void OnShow()
    {
		if (!isAnimalSpeciesSet) 
		{
			return;
		}

        Player player = Service.Request.Player();
		List<Animal> ownedAnimals = player.isAnimalOwned (animalSpecies) ? player.GetAnimals () [animalSpecies] : new List<Animal> ();
		List<Animal> releasedAnimals = player.hasReleasedAnimal (animalSpecies) ? player.GetReleasedAnimals () [animalSpecies] : new List<Animal> ();

		GameObject parentlessPrefab = AssetManager.LoadPrefab(PREFAB_FOLDER, ANIMAL_CARD) as GameObject;
		foreach (Animal animal in ownedAnimals)
		{
			SetupAnimalCard (parentlessPrefab, animal, true);
		}

		foreach (Animal animal in releasedAnimals)
		{
			SetupAnimalCard (parentlessPrefab, animal, false);
		}
    }

	private void SetupAnimalCard(GameObject parentlessPrefab, Animal animal, bool released)
	{
		GameObject entry = Instantiate(parentlessPrefab);
		entry.GetComponent<RawImage> ().texture = released ? Resources.Load<Texture> ("nature") : Resources.Load<Texture> ("lab");
		entry.GetComponentInChildren<ObservedAnimalButton> ().animal = animal;

		GameObject animalObject = AssetManager.GetAnimalClone (animalSpecies);
		animalObject.transform.SetParent (entry.transform);
		animalObject.transform.position = new Vector3 (entry.transform.position.x + AnimalPositions[animalSpecies].x, 
			entry.transform.position.y + AnimalPositions[animalSpecies].y, 
			entry.transform.position.z + AnimalPositions[animalSpecies].z);
		animalObject.transform.localScale = AnimalScales [animalSpecies];
		animalObject.transform.localRotation = Quaternion.Euler (
			AnimalRotations [animalSpecies].x, AnimalRotations [animalSpecies].y, AnimalRotations [animalSpecies].z); 

		entry.transform.SetParent (AnimalGrid.transform);
		entry.transform.localScale = new Vector3 (1.0f, 1.0f, 1.0f);
		entry.transform.localPosition = new Vector3 (0.0f, 0.0f, 0.0f);
		animalModels.Add (animalObject);
		entries.Add (entry);
	}

	private void SetAnimalSpecies(AnimalSpecies species)
	{
		animalSpecies = species;
		isAnimalSpeciesSet = true;
	}

	private void SetUpButtonListener(Button button, Animal animal)
	{
		const string ANIMAL = "ANIMAL";
		const string CALLING_SCREEN = "CALLING_SCREEN";

		button.onClick.RemoveAllListeners ();
		button.onClick.AddListener(() =>
		{
				Dictionary<string, object> eventDict = new Dictionary<string, object>()
				{
					{ ANIMAL, animal },
					{ CALLING_SCREEN, ScreenType.AnimalUnderObs }
				};
				EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Caught);
				EventManager.TriggerEvent(GameEvent.ViewingAnimalInformation, eventDict);
		});
	}

	private void SetPositions()
	{
		AnimalPositions = new Dictionary<AnimalSpecies, Vector3> ();
		AnimalPositions.Add (AnimalSpecies.Acorn, new Vector3 (-5.0f, 5.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Bat, new Vector3 (0.0f, 0.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Butterfly, new Vector3 (0.0f, 0.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Coyote, new Vector3 (0.0f, -30.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Datura, new Vector3 (0.0f, 5.9f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Death, new Vector3 (0.0f, -28.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Deer, new Vector3 (0.0f, -28.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Dolphin, new Vector3 (0.0f, -7.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Dragonfly, new Vector3 (0.0f, -12.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Earth, new Vector3 (0.0f, -28.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Heron, new Vector3 (0.0f, -30.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Lizard, new Vector3 (0.0f, -16.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Mountainlion, new Vector3 (0.0f, -28.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Rabbit, new Vector3 (0.0f, -22.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Rain, new Vector3 (0.0f, -28.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Rattlesnake, new Vector3 (0.0f, -8.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Redtailedhawk, new Vector3 (0.0f, -24.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Shark, new Vector3 (0.0f, -8.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Squirrel, new Vector3 (0.0f, -28.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Water, new Vector3 (0.0f, -28.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Wind, new Vector3 (0.0f, -28.0f, 0.0f));
	}

	private void SetAnimalScales()
	{
		AnimalScales = new Dictionary<AnimalSpecies, Vector3> ();
		AnimalScales.Add (AnimalSpecies.Acorn, new Vector3 (2000.0f, 2000.0f, 2000.0f));
		AnimalScales.Add (AnimalSpecies.Bat, new Vector3 (3.0f, 3.0f, 3.0f));
		AnimalScales.Add (AnimalSpecies.Butterfly, new Vector3 (20.0f, 20.0f, 20.0f));
		AnimalScales.Add (AnimalSpecies.Coyote, new Vector3 (100.0f, 100.0f, 100.0f));
		AnimalScales.Add(AnimalSpecies.Datura, new Vector3 (15.0f, 15.0f, 15.0f));
		AnimalScales.Add (AnimalSpecies.Death, new Vector3 (60.0f, 60.0f, 60.0f));
		AnimalScales.Add (AnimalSpecies.Deer, new Vector3 (100.0f, 100.0f, 100.0f));
		AnimalScales.Add (AnimalSpecies.Dolphin, new Vector3 (30.0f, 30.0f, 30.0f));
		AnimalScales.Add (AnimalSpecies.Dragonfly, new Vector3 (500.0f, 500.0f, 500.0f));
		AnimalScales.Add (AnimalSpecies.Earth, new Vector3 (60.0f, 60.0f, 60.0f));
		AnimalScales.Add (AnimalSpecies.Heron, new Vector3 (75.0f, 75.0f, 75.0f));
		AnimalScales.Add (AnimalSpecies.Lizard, new Vector3 (200.0f, 200.0f, 200.0f));
		AnimalScales.Add (AnimalSpecies.Mountainlion, new Vector3 (60.0f, 60.0f, 60.0f));
		AnimalScales.Add (AnimalSpecies.Rabbit, new Vector3 (100.0f, 100.0f, 100.0f));
		AnimalScales.Add (AnimalSpecies.Rain, new Vector3 (60.0f, 60.0f, 60.0f));
		AnimalScales.Add (AnimalSpecies.Rattlesnake, new Vector3 (1500.0f, 1500.0f, 1500.0f));
		AnimalScales.Add (AnimalSpecies.Redtailedhawk, new Vector3 (100.0f, 100.0f, 100.0f));
		AnimalScales.Add (AnimalSpecies.Shark, new Vector3 (80.0f, 80.0f, 80.0f));
		AnimalScales.Add (AnimalSpecies.Squirrel, new Vector3 (100.0f, 100.0f, 100.0f));
		AnimalScales.Add (AnimalSpecies.Water, new Vector3 (60.0f, 60.0f, 60.0f));
		AnimalScales.Add (AnimalSpecies.Wind, new Vector3 (60.0f, 60.0f, 60.0f));
	}

	private void SetAnimalRotations()
	{
		AnimalRotations = new Dictionary<AnimalSpecies, Vector3> ();
		AnimalRotations.Add (AnimalSpecies.Acorn, new Vector3 (-141.0f, -265.3f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Bat, new Vector3 (0.0f, 150.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Butterfly, new Vector3 (0.0f, 0.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Coyote, new Vector3 (13.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Datura, new Vector3 (0.0f, 0.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Death, new Vector3 (12.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Deer, new Vector3 (12.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Dolphin, new Vector3 (-14.0f, 150.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Dragonfly, new Vector3 (12.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Earth, new Vector3 (12.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Heron, new Vector3 (0.0f, 200.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Lizard, new Vector3 (15.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Mountainlion, new Vector3 (12.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Rabbit, new Vector3 (12.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Rain, new Vector3 (12.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Rattlesnake, new Vector3 (0.0f, 170.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Redtailedhawk, new Vector3 (12.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Shark, new Vector3 (17.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Squirrel, new Vector3 (0.0f, 270.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Water, new Vector3 (12.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Wind, new Vector3 (12.0f, 180.0f, 0.0f));
	}
}