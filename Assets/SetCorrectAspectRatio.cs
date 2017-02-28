using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SetCorrectAspectRatio : MonoBehaviour {

	public Camera cam;

	// Use this for initialization
	void Start () 
	{
		cam.orthographicSize = (float) Screen.height / Screen.width / 2.0f;
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
}
