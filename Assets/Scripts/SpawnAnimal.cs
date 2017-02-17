using UnityEngine;
using System.Collections;

public class SpawnAnimal : MonoBehaviour {

	GameObject animal;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Click()
	{
		Debug.Log ("Spawning");
		animal = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (AnimalSpecies.Tiger));
		animal.layer = LayerMask.NameToLayer("Default");

		//tigerAsset = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (AnimalSpecies.Tiger), new Vector3(0.0f, 0.0f, -5.0f), Quaternion.identity);

		//Animal animal = new Animal ("tiger1", AnimalSpecies.Tiger, "Tigecito", AnimalEncounterType.Caught, HabitatLevelType.Middle);

		//tigerAsset = (GameObject)GameObject.Instantiate(Resources.Load("AnimalPrefabs/Tiger"));//, new Vector3 (0f, 0f, 0f), Quaternion.identity);
	}
}
