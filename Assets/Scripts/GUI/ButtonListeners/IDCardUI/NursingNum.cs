using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class NursingNum : MonoBehaviour
{
	public Text Nursing;

	//This number can change during gameplay
	void Update ()
	{
		Nursing.text = "1";	
	}
}

