using UnityEngine;
using System.Collections;

public class TaggedShowHide : MonoBehaviour {

	public ScreenType Tag;
	public IShowHideListener listener;

	public void Show()
	{
		gameObject.SetActive (true);

		if (listener != null)
		{
			listener.OnShow ();
		}
	}

	public void Hide()
	{
		gameObject.SetActive (false);

		if (listener != null)
		{
			listener.OnHide ();
		}
	}
}
