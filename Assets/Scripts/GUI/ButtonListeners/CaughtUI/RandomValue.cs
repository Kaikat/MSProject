using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class RandomValue : MonoBehaviour
{
	Text t;

	// Use this for initialization
	void Start ()
	{
		t = GetComponent<Text> ();
		t.text = Random.Range (0, 10).ToString ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}

