using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ProgressButtons : MonoBehaviour
{
	public GameObject prefab;
	public GameObject[] buttons;
	public Sprite tiger;
	public Sprite butterfly;
	Dictionary<AnimalSpecies, List<Animal>> Animals;


	// Use this for initialization
	void Start ()
	{
		//prefab = Resources.Load ("Assets/Prefabs/AnimalButton") as GameObject;
		Animals = StartGame.CurrentPlayer.GetAnimals ();
		//int numOfAnimals = Animals.Count;
		int numOfAnimals = 2;

		buttons = new GameObject[numOfAnimals];
		for (int i = 0; i < numOfAnimals; i++) {
			GameObject button = Instantiate (prefab) as GameObject;
			button.transform.parent = this.transform;
			button.transform.position = new Vector3 (265 + 100 * i, 300, 0);
			if (i == 0) {
				button.GetComponentInChildren<Text> ().text = "Tiger";
				button.GetComponent<Image> ().sprite = tiger;
			} else {
				button.GetComponentInChildren<Text> ().text = "Butterfly";
				button.GetComponent<Image> ().sprite = butterfly;
			}
			buttons [i] = button;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

