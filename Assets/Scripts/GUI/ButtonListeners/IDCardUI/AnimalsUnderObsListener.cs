using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalsUnderObsListener : MonoBehaviour {	

    public void Click()
    {
        EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.AnimalUnderObs);
    }

}
