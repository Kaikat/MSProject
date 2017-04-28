using UnityEngine;
using System.Collections;

//TODO: DEAD CLASS ?

public class SpawnAnimal : MonoBehaviour {

	//temp static for get animal type
	public static GameObject animal;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Click()
	{
//		Debug.Log ("Spawning");
//		animal = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (AnimalSpecies.Tiger));
//		animal.layer = LayerMask.NameToLayer("Default");



		Debug.Log ("Caught Animal");
		//animal = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (AnimalSpecies.Tiger));
		//animal.layer = LayerMask.NameToLayer("Default");

		//Service.Request.CatchAnimal (AnimalSpecies.Butterfly);




		//tigerAsset = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (AnimalSpecies.Tiger), new Vector3(0.0f, 0.0f, -5.0f), Quaternion.identity);
		//Animal animal = new Animal ("tiger1", AnimalSpecies.Tiger, "Tigecito", AnimalEncounterType.Caught, HabitatLevelType.Middle);
		//tigerAsset = (GameObject)GameObject.Instantiate(Resources.Load("AnimalPrefabs/Tiger"));//, new Vector3 (0f, 0f, 0f), Quaternion.identity);
	}
}
