using System.Collections;
using UnityEngine;

public class AnimalsUnderObsBackButton : MonoBehaviour {

    public void Click()
    {
		Event.Request.TriggerEvent(GameEvent.SwitchScreen, ScreenType.IDCard);
    }
}
