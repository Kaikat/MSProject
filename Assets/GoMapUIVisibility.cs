using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoMapUIVisibility : MonoBehaviour {
	public GameObject GoMapCanvas;
	public Camera GoMapCamera;

	void Awake () 
	{
		EventManager.RegisterEvent <ScreenType> (GameEvent.SwitchScreen, ShowHideCanvas);
	}
	
	public void ShowHideCanvas(ScreenType screen)
	{
		if (screen == ScreenType.GoMapHome) 
		{
			GoMapCanvas.SetActive (true);
			GoMapCamera.enabled = true;
		} 
		else 
		{
			GoMapCanvas.SetActive (false);
			GoMapCamera.enabled = false;
		}
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <ScreenType> (GameEvent.SwitchScreen, ShowHideCanvas);
	}
}
