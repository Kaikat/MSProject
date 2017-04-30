using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CaughtNum : MonoBehaviour 
{
	public Text Caught;

	void Update ()
	{	
		Caught.text = Service.Request.Player ().AnimalsCaught.ToString ();
	}
}
