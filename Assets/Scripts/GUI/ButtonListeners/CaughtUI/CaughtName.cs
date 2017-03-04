using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CaughtName : MonoBehaviour
{
	Text t;
	public Text SpawnHint;
	public Text AnimalNameTitle;
	// Use this for initialization
	void Start ()
	{
		//t = GetComponent<Text> ();
		//t.text = SpawnAnimal.animal.ToString ();
		AnimalNameTitle.text = SpawnHint.text;
	}
	
	// Update is called once per frame
	void Update ()
	{
	}
}

