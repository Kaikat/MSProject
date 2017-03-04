using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProgressButtons : MonoBehaviour
{
	public GameObject prefab;
	public GameObject[] buttons;
	//Dictionary<AnimalSpecies, List<Animal>> Animals;


	// Use this for initialization
	void Start ()
	{
		//prefab = Resources.Load ("Assets/Prefabs/AnimalButton") as GameObject;
		Animals = StartGame.CurrentPlayer.GetAnimals ();
		//int numOfAnimals = Animals.Count;
		int numOfAnimals = 3;

		buttons = new GameObject[numOfAnimals];
		for (int i = 0; i < numOfAnimals; i++) {
			GameObject button = Instantiate (prefab) as GameObject;
			button.transform.parent = this.transform;
			button.transform.position = new Vector3 (285 + 100 * i, 320, 0);
			buttons [i] = button;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

