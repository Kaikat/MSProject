using UnityEngine;
using System.Collections.Generic;

public static class AssetManager
{
	private static Dictionary<AnimalSpecies, Object> AnimalPrefabs;
	private static List<GameObject> AnimalModels;
	private static List<AnimalSpecies> Species;

	private static Dictionary<AnimalSpecies, Vector3> AnimalPositions;
	private static Dictionary<AnimalSpecies, Vector3> AnimalRotations;
	private static Dictionary<AnimalSpecies, Vector3> AnimalScales;

	static AssetManager()
	{
		SetAnimalPositions ();
		SetAnimalRotations ();
		SetAnimalScales ();
	}

	public static void Init()
	{
		string folder = "AnimalPrefabs";
		AnimalPrefabs = new Dictionary<AnimalSpecies, Object> ();
		AnimalModels = new List<GameObject> ();
		Species = new List<AnimalSpecies> ();

		Dictionary<AnimalSpecies, AnimalData> animals = Service.Request.AllAnimals();

		var species = System.Enum.GetValues(typeof(AnimalSpecies));
		foreach (AnimalSpecies i in species)
		{
			AnimalPrefabs.Add(animals[i].Species, LoadPrefab(folder, animals[i].Species.ToString()));

			// TEMPORARY: Not the final assets
			//Keep these 2
			GameObject animal = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (animals [i].Species));
			animal.layer = LayerMask.NameToLayer ("3D");

			// TEMPORARY: Not the final assets
			/*if (animals[i].Species == AnimalSpecies.Butterfly)
			{
				GameObject temp = new GameObject ();
				temp.layer = LayerMask.NameToLayer ("3D");
				Transform t = temp.transform;
				t.localScale = t.transform.localScale * 10.0f;
				t.localPosition = new Vector3 (1.0f, -3.5f, 0.0f);
				animal.transform.SetParent (t);
			}*/

			AnimalSpecies a = animals[i].Species;

			animal.transform.localScale = AnimalScales [a];
			animal.transform.localPosition = AnimalPositions [a];
			animal.transform.localRotation = Quaternion.Euler (AnimalRotations [a]);

			animal.SetActive (false);
			AnimalModels.Add (animal);
			Species.Add (animals [i].Species);
		}
	}

	public static Object LoadPrefab(string folder, string prefabName)
	{
		Object prefabObj = new Object ();
		prefabObj = folder == string.Empty ? Resources.Load (prefabName) : Resources.Load (folder + "/" + prefabName);
		return prefabObj;
	}

	public static Object GetAnimalPrefab(AnimalSpecies species)
	{
		return AnimalPrefabs [species];
	}

	public static GameObject GetAnimalClone(AnimalSpecies species)
	{
		GameObject animal = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (species));
		animal.layer = LayerMask.NameToLayer ("3D");
		return animal;
	}

	public static void ShowAnimal(AnimalSpecies speciesArg)
	{
        int index = Species.FindIndex(sp => sp == speciesArg);
        if (index != -1)
        {
            AnimalModels[index].SetActive(true);
        }
	}

	public static void HideAnimals()
	{
        foreach (GameObject animalModel in AnimalModels)
        {
            animalModel.SetActive(false);
        }
	}

	private static void SetAnimalPositions()
	{
		AnimalPositions = new Dictionary<AnimalSpecies, Vector3> ();
		AnimalPositions.Add (AnimalSpecies.Acorn, new Vector3 (-0.5f, 0.71f, 0.0f));		//needs animation
		AnimalPositions.Add (AnimalSpecies.Bat, new Vector3 (1.0f, -3.5f, 0.0f));			//need asset - butterfly - GOOD
		AnimalPositions.Add (AnimalSpecies.Butterfly, new Vector3 (0.0f, 0.0f, 0.0f));		//DO LAST
		AnimalPositions.Add (AnimalSpecies.Coyote, new Vector3 (0.0f, -1.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Datura, new Vector3 (0.0f, 0.0f, 0.0f));			//needs animation
		AnimalPositions.Add (AnimalSpecies.Death, new Vector3 (0.0f, -1.0f, 0.0f));			//need asset - tiger - OK
		AnimalPositions.Add (AnimalSpecies.Deer, new Vector3 (-0.1f, -1.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Dolphin, new Vector3 (0.0f, 0.0f, 0.0f));		//needs animation
		AnimalPositions.Add (AnimalSpecies.Dragonfly, new Vector3 (0.0f, 0.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Earth, new Vector3 (0.0f, -1.0f, 0.0f));			//need asset - tiger - OK
		AnimalPositions.Add (AnimalSpecies.Heron, new Vector3 (0.0f, -2.2f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Horse, new Vector3 (-0.5f, -1.5f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Lizard, new Vector3 (0.0f, 0.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Mountainlion, new Vector3 (0.0f, -1.0f, 0.0f));	//Recolor tiger asset to light brown - OK
		AnimalPositions.Add (AnimalSpecies.Rabbit, new Vector3 (0.0f, -0.2f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Rain, new Vector3 (0.0f, -1.0f, 0.0f));			//need asset - tiger - GOOD
		AnimalPositions.Add (AnimalSpecies.Rattlesnake, new Vector3 (0.0f, 0.0f, 0.0f));	//needs animation
		AnimalPositions.Add (AnimalSpecies.Redtailedhawk, new Vector3 (0.0f, -0.5f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Shark, new Vector3 (0.2f, 0.5f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Squirrel, new Vector3 (0.0f, 0.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Tiger, new Vector3 (0.0f, -1.0f, 0.0f));
		AnimalPositions.Add (AnimalSpecies.Water, new Vector3 (0.0f, -1.0f, 0.0f));			//need asset - tiger - GOOD
		AnimalPositions.Add (AnimalSpecies.Wind, new Vector3 (0.0f, -1.0f, 0.0f));			//need asset - tiger - OK
	}

	private static void SetAnimalScales()
	{
		AnimalScales = new Dictionary<AnimalSpecies, Vector3> ();
		AnimalScales.Add (AnimalSpecies.Acorn, new Vector3 (100.0f, 100.0f, 100.0f));			//horse
		AnimalScales.Add (AnimalSpecies.Bat, new Vector3 (10.0f, 10.0f, 10.0f));		//butterfly
		AnimalScales.Add (AnimalSpecies.Butterfly, new Vector3 (10.0f, 10.0f, 10.0f));
		AnimalScales.Add (AnimalSpecies.Coyote, new Vector3 (7.0f, 7.0f, 7.0f));
		AnimalScales.Add (AnimalSpecies.Datura, new Vector3 (1.5f, 1.5f, 1.5f));		//horse
		AnimalScales.Add (AnimalSpecies.Death, new Vector3 (4.0f, 4.0f, 4.0f));			//tiger
		AnimalScales.Add (AnimalSpecies.Deer, new Vector3 (7.0f, 7.0f, 7.0f));
		AnimalScales.Add (AnimalSpecies.Dolphin, new Vector3 (3.0f, 3.0f, 3.0f));		//tiger
		AnimalScales.Add (AnimalSpecies.Dragonfly, new Vector3 (20.0f, 20.0f, 20.0f));
		AnimalScales.Add (AnimalSpecies.Earth, new Vector3 (4.0f, 4.0f, 4.0f));			//tiger
		AnimalScales.Add (AnimalSpecies.Heron, new Vector3 (9.0f, 9.0f, 9.0f));
		AnimalScales.Add (AnimalSpecies.Horse, new Vector3 (1.7f, 1.7f, 1.7f));
		AnimalScales.Add (AnimalSpecies.Lizard, new Vector3 (10.0f, 10.0f, 10.0f));
		AnimalScales.Add (AnimalSpecies.Mountainlion, new Vector3 (4.0f, 4.0f, 4.0f));	//tiger
		AnimalScales.Add (AnimalSpecies.Rabbit, new Vector3 (5.0f, 5.0f, 5.0f));
		AnimalScales.Add (AnimalSpecies.Rain, new Vector3 (4.0f, 4.0f, 4.0f));			//horse
		AnimalScales.Add (AnimalSpecies.Rattlesnake, new Vector3 (115.0f, 115.0f, 115.0f));	//tiger
		AnimalScales.Add (AnimalSpecies.Redtailedhawk, new Vector3 (7.0f, 7.0f, 7.0f));
		AnimalScales.Add (AnimalSpecies.Shark, new Vector3 (7.0f, 7.0f, 7.0f));
		AnimalScales.Add (AnimalSpecies.Squirrel, new Vector3 (5.0f, 5.0f, 5.0f));
		AnimalScales.Add (AnimalSpecies.Tiger, new Vector3 (4.0f, 4.0f, 4.0f));
		AnimalScales.Add (AnimalSpecies.Water, new Vector3 (4.0f, 4.0f, 4.0f));			//horse
		AnimalScales.Add (AnimalSpecies.Wind, new Vector3 (4.0f, 4.0f, 4.0f));			//tiger
	}

	private static void SetAnimalRotations()
	{
		AnimalRotations = new Dictionary<AnimalSpecies, Vector3> ();
		AnimalRotations.Add (AnimalSpecies.Acorn, new Vector3 (-141.0f, -265.3f, 0.0f));		//horse
		AnimalRotations.Add (AnimalSpecies.Bat, new Vector3 (0.0f, 150.0f, 0.0f));			//horse
		AnimalRotations.Add (AnimalSpecies.Butterfly, new Vector3 (270.0f, 30.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Coyote, new Vector3 (0.0f, 190.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Datura, new Vector3 (0.0f, 0.0f, 0.0f));		//horse
		AnimalRotations.Add (AnimalSpecies.Death, new Vector3 (0.0f, 180.0f, 0.0f));		//tiger
		AnimalRotations.Add (AnimalSpecies.Deer, new Vector3 (0.0f, 160.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Dolphin, new Vector3 (-14.0f, 150.0f, 0.0f));		//tiger
		AnimalRotations.Add (AnimalSpecies.Dragonfly, new Vector3 (0.0f, 150.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Earth, new Vector3 (0.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Heron, new Vector3 (0.0f, 150.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Horse, new Vector3 (0.0f, 150.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Lizard, new Vector3 (0.0f, 200.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Mountainlion, new Vector3 (0.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Rabbit, new Vector3 (0.0f, 155.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Rain, new Vector3 (0.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Rattlesnake, new Vector3 (0.0f, 170.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Redtailedhawk, new Vector3 (15.0f, 200.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Shark, new Vector3 (0.0f, 190.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Squirrel, new Vector3 (0.0f, 275.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Tiger, new Vector3 (0.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Water, new Vector3 (0.0f, 180.0f, 0.0f));
		AnimalRotations.Add (AnimalSpecies.Wind, new Vector3 (0.0f, 180.0f, 0.0f));
	}




	/*private static void LoadAllAnimalModels()
	{
		List<BasicAnimal> animals = Service.Request.GetAllAnimals ();

		for (int i = 0; i < animals.Count; i++)
		{
			GameObject animal = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (animals[i].Species));
			animal.layer = LayerMask.NameToLayer("Default");
			AnimalModels.Add (animal);
		}
	}  */

	//INSTANTIATE
	//			animalPrefabObj = (GameObject)GameObject.Instantiate (Resources.Load (folder + "/" + prefabName));

}










































/*
public class AssetManager : MonoBehaviour {

	private static AssetManager s_Instance = null;

	// This defines a static instance property that attempts to find the manager object in the scene and
	// returns it to the caller.
	public static AssetManager instance 
	{
		get 
		{
			if (s_Instance == null) 
			{
				// This is where the magic happens.
				//  FindObjectOfType(...) returns the first AManager object in the scene.
				s_Instance =  FindObjectOfType(typeof (AssetManager)) as AssetManager;
			}

			// If it is still null, create a new instance
			if (s_Instance == null) 
			{
				GameObject obj = new GameObject("AssetManager");
				s_Instance = obj.AddComponent(typeof (AssetManager)) as AssetManager;
				Debug.Log ("Could not locate an AssetManager object. AssetManager was Generated Automaticly.");
			}

			return s_Instance;
		}
	}
		
	private static Dictionary<string, GameObject> ModelDictionary;


}
*/
	//private static AssetManager assetManager;
	/*
	public static AssetManager manager
	{
		get 
		{
			if (!assetManager) 
			{
				assetManager = FindObjectOfType (typeof(AssetManager)) as AssetManager;

				if (!assetManager) 
				{
					Debug.LogError ("There needs to be one active AssetManager script on a GameObject in your scene.");
				} 
				else 
				{
					assetManager.Init ();
				}
			}

			return assetManager;
		}
	}
*/
/*	void Init ()
	{
		ModelDictionary = new Dictionary<string, GameObject> ();
		for (int i = 0; i < name.Length; i++) 
		{
			ModelDictionary.Add (Names [i], Models [i]);
		}
	}

	private static void InitializeModelDictionary()
	{
		ModelDictionary = new Dictionary<string, GameObject> ();
		for (int i = 0; i < Names.Length; i++) 
		{
			ModelDictionary.Add (Names [i], Models [i]);
		}
	}*/

	/*public static GameObject GetAsset(string assetName)
	{


		if (ModelDictionary.ContainsKey (assetName)) 
		{
			return ModelDictionary [assetName];
		}
		else
		{
			Debug.Log ("assetName: " + assetName + " NOT FOUND!");
			return null;
		}
	}*/

