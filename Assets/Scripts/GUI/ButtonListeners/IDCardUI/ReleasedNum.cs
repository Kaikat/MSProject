using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReleasedNum : MonoBehaviour
{
	public Text Released;

	//This number can change during gameplay
	void Update () 
	{
		Released.text = Service.Request.Player ().AnimalsReleased.ToString ();
	}
}

