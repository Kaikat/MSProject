using UnityEngine;
using UnityEngine.UI;
using System.Collections;

// http://answers.unity3d.com/questions/706142/openstart-device-camera-in-unity3d-app-using-c-scr.html
using System;


public class CameraController : MonoBehaviour
{
	//public WebCamTexture mCamera = null;
	//public GameObject cameraImage;
	public Camera backgroundCamera;
	public Text debugText;
	//private WebCamTexture mCamera = null;

	//WebCamTexture webcamTexture;
	// Use this for initialization
	void Start ()
	{
		
		//WORKS
		/*WebCamDevice[] devices = WebCamTexture.devices;
		for(int i = 0; i < devices.Length; i++)
		{
			print("Webcam available: " + devices[i].name);
		}

		RawImage test = GetComponent<RawImage> ();
		WebCamTexture tex = new WebCamTexture(devices[0].name);
		test.texture = tex;
		tex.Play();
*/
		WebCamDevice[] devices = WebCamTexture.devices;
		for(int i = 0; i < devices.Length; i++)
		{
			print("Webcam available: " + devices[i].name);
		}

		Renderer renderer = GetComponent<Renderer>();
		WebCamTexture tex = new WebCamTexture(devices[0].name);

		//float quadHeight = (float) (3.0/4.0 * backgroundCamera.orthographicSize);
		float quadHeight = (float) (2.0 * backgroundCamera.orthographicSize);
		//float quadWidth  = (float) (quadHeight * (float) Screen.width / (float) Screen.height);
		float quadWidth  = quadHeight * Screen.width / Screen.height;


		transform.localScale = new Vector3 (-quadWidth, quadHeight, 1.0f);
		debugText.text = "I'm a computer!\n";

		#if UNITY_IOS
		transform.localScale = new Vector3 (quadWidth, -quadHeight, 1.0f);
		debugText.text = "I'm an iPhone!";
		#endif

		#if UNITY_ANDROID
		debugText.text = "I'm an android phone!";
		#endif

	

		renderer.material.mainTexture = tex;
		tex.Play();





		/*	WebCamDevice[] devices = WebCamTexture.devices;

			// for debugging purposes, prints available devices to the console
			for(int i = 0; i < devices.Length; i++)
			{
				print("Webcam available: " + devices[i].name);
			}

			//Renderer rend = this.GetComponentInChildren<Renderer>();
			RawImage test = GetComponent<RawImage> ();

			// assuming the first available WebCam is desired
			WebCamTexture tex = new WebCamTexture(devices[0].name);
			//rend.material.mainTexture = tex;


			test.texture = tex;
			tex.Play();*/

		/*
		 * 
		test.
		float aspectRatio = (float) tex.width / tex.height;
		if (aspectRatio > 1.0f)
		{
			float uvHeight = 1.0f / aspectRatio;
			test.uvRect = new Rect(0.0f, 0.5f - uvHeight / 2.0f, 1.0f, uvHeight);
		} else
		{
			float uvWidth = aspectRatio;
			test.uvRect = new Rect(0.5f - uvWidth / 2.0f, 0.0f, uvWidth, 1.0f);
		}
		 * /


		/*Debug.Log ("Script has been started");
		//plane = GameObject.FindWithTag ("Player");

		WebCamTexture webcamTexture = new WebCamTexture();
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.mainTexture = webcamTexture;
		webcamTexture.Play();*/

		//mCamera = new WebCamTexture ();
		//plane.GetComponent<Renderer> ().material.mainTexture = mCamera;
		//mCamera.Play ();

		/*WebCamTexture webcamTexture = new WebCamTexture();
		Rect rec = new Rect(0, 0, webcamTexture.width, webcamTexture.height);
		IntPtr ptr = webcamTexture.GetNativeTexturePtr ();
		Texture2D texture = new Texture2D (webcamTexture.width, webcamTexture.height);
		texture.UpdateExternalTexture (ptr);
		cameraImage.GetComponent<Image> ().sprite = Sprite.Create (texture, rec, new Vector2 (0.5f, 0.5f));
		webcamTexture.Play ();*/

		//webcamTexture = new WebCamTexture();
		//Renderer renderer = GetComponent<Renderer>();
		//renderer.material.mainTexture = webcamTexture;
		//webcamTexture.Play();

	}

	// Update is called once per frame
	void Update ()
	{

	}
}