using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetCorrectAspectRatio : MonoBehaviour {

	public Camera cam;
	public Text debugText;

	// Use this for initialization
	void Start () 
	{
		//debugText.text = "Screen width: " + Screen.width.ToString () + ", Screen height: " + Screen.height.ToString() + "\n";
		cam.orthographicSize = (float) Screen.height / Screen.width / 2.0f;
		//debugText.text += "\nCam Ortho: " + cam.orthographicSize.ToString();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
