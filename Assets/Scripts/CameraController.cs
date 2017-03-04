using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// http://answers.unity3d.com/questions/706142/openstart-device-camera-in-unity3d-app-using-c-scr.html
using System;


public class CameraController : MonoBehaviour
{
	public Camera backgroundCamera;
	//public Text debugText;

	WebCamTexture tex;

	void Start ()
	{
		WebCamDevice[] devices = WebCamTexture.devices;
		/*for(int i = 0; i < devices.Length; i++)
		{
			print("Webcam available: " + devices[i].name);
		}*/

		tex = new WebCamTexture(devices[0].name);
		tex.filterMode = FilterMode.Trilinear;

		Vector3 originalScale = transform.localScale;
		float aspectRatio = (float) Screen.width / (float) Screen.height;
		//transform.localScale = new Vector3 (originalScale.x, originalScale.y, originalScale.z / aspectRatio);

		//ios 		: -height
		//android	: none
		/*#if UNITY_IOS
		transform.localScale = new Vector3 (originalScale.x, originalScale.y, originalScale.z / aspectRatio);
		debugText.text += "I'm an iPhone!";
		#endif

		#if UNITY_ANDROID
		debugText.text += "I'm an android phone!";
		transform.localScale = new Vector3 (originalScale.x, originalScale.y, originalScale.z / aspectRatio);
		#endif*/

		#if UNITY_ANDROID
//		debugText.text += "I'm an android phone - CHANGE!";
		transform.localScale = new Vector3 (-originalScale.x, originalScale.y, originalScale.z);
		#endif

		GetComponent<Renderer> ().material.mainTexture = tex;
		tex.Play();
	}

	public void OnShow()
	{
		tex.Play ();
	}

	public void OnHide()
	{
		tex.Pause ();
	}
}