using UnityEngine;
using System.Collections;


using UnityEngine.UI;
public class CatchAnimalUIObject : MonoBehaviour, IShowHideListener 
{
	public GameObject cameraFeedBackground;
	public RawImage image;

	// Use this for initialization
	void Start () 
	{
		gameObject.GetComponent<TaggedShowHide> ().listener = this;
		//image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height); //this is probably height, width
		//image.GetComponent<CameraController> ().OnShow ();
		cameraFeedBackground.SetActive (true);
	}
	
	public void OnShow()
	{
		//image.rectTransform.sizeDelta = new Vector2(Screen.width, Screen.height);
		//image.GetComponent<CameraController> ().OnShow ();
		cameraFeedBackground.SetActive (true);
		cameraFeedBackground.GetComponent<CameraController> ().OnShow ();
	}

	public void OnHide()
	{
		//image.rectTransform.sizeDelta = new Vector2(0.0f, 0.0f);
		//image.GetComponent<CameraController> ().OnHide ();

		cameraFeedBackground.GetComponent<CameraController> ().OnHide ();
		cameraFeedBackground.SetActive (false);
	}
}
