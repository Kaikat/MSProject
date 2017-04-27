using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnHealth : MonoBehaviour {

    private AnimalSpecies animal;

    void Awake() {
        EventManager.RegisterEvent<AnimalSpecies>(GameEvent.Caught, SetAnimal);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetAnimal(AnimalSpecies animal_arg)
    {
        animal = animal_arg;
    }

    void Destroy()
    {
        EventManager.UnregisterEvent<AnimalSpecies>(GameEvent.Caught, SetAnimal);
    }

    public void Click()
    {
        if (Random.Range(0, 20) >= 15)
        {
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Celebration);
        }
        else
        {
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Failure);
        }
    }
}
