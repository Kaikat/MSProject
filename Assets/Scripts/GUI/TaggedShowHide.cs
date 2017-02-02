using UnityEngine;
using System.Collections;

public class TaggedShowHide : MonoBehaviour {

	public ScreenType Tag;

	public void Show()
	{
		gameObject.SetActive (true);
	}

	public void Hide()
	{
		gameObject.SetActive (false);
	}
}
