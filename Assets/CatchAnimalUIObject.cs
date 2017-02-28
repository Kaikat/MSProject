using UnityEngine;
using System.Collections;

public class CatchAnimalUIObject : MonoBehaviour, IShowHideListener 
{
	public GameObject cameraFeedBackground;

	// Use this for initialization
	void Start () 
	{
		gameObject.GetComponent<TaggedShowHide> ().listener = this;
		cameraFeedBackground.SetActive (true);
	}
	
	public void OnShow()
	{
		cameraFeedBackground.SetActive (true);
		cameraFeedBackground.GetComponent<CameraController> ().OnShow ();
	}

	public void OnHide()
	{
		cameraFeedBackground.GetComponent<CameraController> ().OnHide ();
		cameraFeedBackground.SetActive (false);
	}
}
