using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SeenNum : MonoBehaviour
{
	Text t;

	void Start ()
	{
		t = GetComponent<Text> ();
	}
	
	void Update ()
	{	
		t.text = Service.Request.Player ().AnimalsDiscovered.ToString ();
	}
}

