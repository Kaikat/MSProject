﻿using System.Collections;
using UnityEngine;

public class AnimalsUnderObsBackButton : MonoBehaviour {

    public void Click()
    {
        EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.IDCard);
    }
}