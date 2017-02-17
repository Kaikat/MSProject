using UnityEngine;
using System.Collections;

public class JournalButton : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.Journal, ScreenType.Journal);
	}
}

