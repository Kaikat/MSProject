using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsUnderObs : MonoBehaviour {	
	
    // Update is called once per frame
	void Update () {
		
	}

    public void Click()
    {
        EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.AnimalUnderObs);
    }

}
