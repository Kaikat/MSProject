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
	
	// Update is called once per frame
	void Update ()
	{	
		
		t.text = "3";
		//t.text = StartGame.CurrentPlayer.GetSeen.ToString();
	}
}

