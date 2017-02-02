using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class AssetManager
{
	private static Dictionary<AnimalSpecies, Object> AnimalPrefabs;

	static AssetManager()
	{
		Init ();
	}

	public static void Init()
	{
		string folder = "AnimalPrefabs";
		AnimalPrefabs = new Dictionary<AnimalSpecies, Object> ();

		List<BasicAnimal> animals = Service.Request.GetAllAnimals ();

		for(int i = 0; i < animals.Count; i++)
		{
			AnimalPrefabs.Add(animals[i].Species, LoadPrefab(folder, animals[i].Species.ToString()));
		}
	}

	public static Object LoadPrefab(string folder, string prefabName)
	{
		Object animalPrefabObj = new Object ();
		animalPrefabObj = folder == string.Empty ? Resources.Load (prefabName) : Resources.Load (folder + "/" + prefabName);
		return animalPrefabObj;
	}

	public static Object GetAnimalPrefab(AnimalSpecies species)
	{
		return AnimalPrefabs [species];
	}

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

