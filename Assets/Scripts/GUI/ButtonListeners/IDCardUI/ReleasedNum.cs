using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReleasedNum : MonoBehaviour
{
	Text t;

	void Start () 
	{
		t = GetComponent<Text> ();
	}

	void Update () 
	{
		t.text = Service.Request.Player ().AnimalsReleased.ToString ();
	}
}

