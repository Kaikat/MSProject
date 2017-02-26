using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HomeUIObject : MonoBehaviour, IShowHideListener
{
	public GameObject mapImage;

	public void Start()
	{
		gameObject.GetComponent<TaggedShowHide> ().listener = this;
	}

	public void OnShow()
	{
		mapImage.GetComponent<UpdateGPSLocation>().OnShow();
	}

	public void OnHide()
	{
		mapImage.GetComponent<UpdateGPSLocation>().OnHide();
	}
}
