using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SeenNum : MonoBehaviour
{
	public Text Discovered;

	//This number can change during gameplay
	void Update ()
	{	
		Discovered.text = Service.Request.Player ().AnimalsDiscovered.ToString ();
	}
}

