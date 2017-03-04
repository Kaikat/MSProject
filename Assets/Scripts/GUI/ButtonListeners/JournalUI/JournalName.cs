using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JournalName : MonoBehaviour
{
	Text t;
	Dictionary<AnimalSpecies,List<Animal>> Animals;

	// Use this for initialization
	void Start ()
	{
		t = GetComponent<Text> ();
		//Animals = StartGame.CurrentPlayer.GetAnimals ();

		//t.text = "Tiger";
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

