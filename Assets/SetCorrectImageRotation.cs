using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetCorrectImageRotation : MonoBehaviour {

	public Text debugText;

	void Start () 
	{
		#if UNITY_IOS
		//debugText.text = "I'm an iPhone!";
		#endif

		#if UNITY_ANDROID
		//debugText.text = "I'm an android phone!";
		transform.localRotation = Quaternion.Euler(90.0f, 0.0f, 180.0f);
		#endif
	}

	// Update is called once per frame
	void Update () {
	
	}
}
