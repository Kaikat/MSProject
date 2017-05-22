using System.Collections;
using UnityEngine;

public class IDButton : MonoBehaviour {

    public void Click()
    {
        EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.IDCard);
    }
}
