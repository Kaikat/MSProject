using UnityEngine;
using System.Collections;

public class HomeButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.Home);
	}
}
