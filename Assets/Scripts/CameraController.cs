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

	WebCamTexture tex;
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
		tex = new WebCamTexture(devices[0].name);
		tex.filterMode = FilterMode.Trilinear;
		/*
		//float quadHeight = (float) (3.0/4.0 * backgroundCamera.orthographicSize);
		float quadHeight = (float) (2.0 * backgroundCamera.orthographicSize);
		//float quadWidth  = (float) (quadHeight * (float) Screen.width / (float) Screen.height);
		float quadWidth  = quadHeight * Screen.width / Screen.height;
		*/

		float quadWidth = 10.0f * ((float)Screen.width * backgroundCamera.orthographicSize) / (float)(Screen.width + Screen.height);
		float quadHeight = 10.0f * ((float)Screen.height * backgroundCamera.orthographicSize) / (float)(Screen.width + Screen.height);

		debugText.text = "Camera Pixel Dimension: " + backgroundCamera.pixelWidth.ToString () + ", " + backgroundCamera.pixelHeight.ToString () + "\n";
		debugText.text += "Texture Dimension: " + tex.width.ToString() + ", " + tex.height.ToString () + "\n";
		debugText.text += "Screen: " + Screen.width.ToString () + ", " + Screen.height.ToString () + "\n";

		Vector3 originalScale = transform.localScale;
		//float aspectRatio = (float)Screen.width / (float)Screen.height;
		//Vector3 quadPoint = new Vector3(1.0f, 0.0f, 0.0f);
		//Vector3 toScreenSpace = backgroundCamera.WorldToScreenPoint (quadPoint);
		//debugText.text += "XYZ: " + toScreenSpace [0].ToString() + ", " + toScreenSpace [1].ToString () + ", " + toScreenSpace [2].ToString () + "\n";
		float aspectRatio = (float) Screen.width / (float) Screen.height;
		//transform.localScale = new Vector3(originalScale.x, originalScale.y, originalScale.z / aspectRatio);

		/*
		Mesh mesh = GetComponent<Mesh> ();
		Vector3 posStart = backgroundCamera.WorldToScreenPoint(new Vector3(mesh.bounds.min.x, mesh.bounds.min.y, mesh.bounds.min.z));
		Vector3 posEnd = backgroundCamera.WorldToScreenPoint(new Vector3(mesh.bounds.max.x, mesh.bounds.max.y, mesh.bounds.min.z));

		int widthX = (int)(posEnd.x - posStart.x);
		int widthY = (int)(posEnd.y - posStart.y);
		*/

		//quadWidth = aspectRatio;
		//quadHeight = aspectRatio * 2.0f;
		//transform.localScale = new Vector3(cam.orthographicSize/2 * (Screen.width/Screen.height),cam.orthographicSize/2,0f);



		//debugText.text += "I'm a computer!\n";
		//transform.localScale = new Vector3 (-quadWidth, quadHeight, 1.0f);

		//ios 		: -height
		//android	: none
		#if UNITY_IOS
		transform.localScale = new Vector3 (originalScale.x, originalScale.y, originalScale.z / aspectRatio);
		debugText.text += "I'm an iPhone!";
		#endif

		#if UNITY_ANDROID
		debugText.text += "I'm an android phone!";
		transform.localScale = new Vector3 (originalScale.x, originalScale.y, originalScale.z / aspectRatio);
		#endif

	

		renderer.material.mainTexture = tex;
		tex.Play();

		debugText.text += "Texture Dimension22: " + tex.width.ToString() + ", " + tex.height.ToString () + "\n";




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

	public void OnShow()
	{
		tex.Play ();
	}

	public void OnHide()
	{
		tex.Pause ();
	}
}