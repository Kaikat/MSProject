using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class AssetManager
{
	private static Dictionary<AnimalSpecies, Object> AnimalPrefabs;
	private static List<GameObject> AnimalModels;
	private static List<AnimalSpecies> Species;

	static AssetManager()
	{
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
			animal.layer = LayerMask.NameToLayer ("Default");

			// TEMPORARY: Not the final assets
			if (animals[i].Species == AnimalSpecies.Butterfly)
			{
				GameObject temp = new GameObject ();
				temp.layer = LayerMask.NameToLayer ("Default");
				Transform t = temp.transform;
				t.localScale = t.transform.localScale * 10.0f;
				t.localPosition = new Vector3 (1.0f, -3.5f, 0.0f);
				animal.transform.SetParent (t);
			}
			/*else
			{
				// Keep these 2
				animal.transform.localScale = animal.transform.localScale * 2.0f;
			}*/

			AnimalSpecies a = animals[i].Species;
			if (animals [i].Species == AnimalSpecies.Horse || a == AnimalSpecies.Bat || 
				a == AnimalSpecies.Raingod || a == AnimalSpecies.Datura ||
				a == AnimalSpecies.Watergod)
			{
				animal.transform.localScale = animal.transform.localScale * 2.0f;
				animal.transform.localPosition = new Vector3 (animal.transform.localPosition.x, -1.9f, animal.transform.localPosition.z);
			}

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

	public static void ShowAnimal(AnimalSpecies species)
	{
		for (int i = 0; i < Species.Count; i++)
		{
			if (Species [i] == species)
			{
				AnimalModels [i].SetActive (true);
			}
		}
	}

	public static void HideAnimals()
	{
		for (int i = 0; i < AnimalModels.Count; i++)
		{
			AnimalModels [i].SetActive (false);
		}
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

