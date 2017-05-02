using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchOnHealth : MonoBehaviour {

	//TODO: Find use for this animal argument
    private Animal animal;

    void Awake() 
	{
		EventManager.RegisterEvent<Animal>(GameEvent.AnimalCaught, SetAnimal);
    }

    private void SetAnimal(Animal animal_arg)
    {
        animal = animal_arg;
    }

    void Destroy()
    {
		EventManager.UnregisterEvent<Animal>(GameEvent.AnimalCaught, SetAnimal);
    }

    public void Click()
    {
		if (Random.Range(0, 20) >= 15)
        {
			Service.Request.ReleaseAnimal (animal);
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Celebration);
        }
        else
        {
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Quiz);
			EventManager.TriggerEvent(GameEvent.QuizTime, animal);
        }
    }
}
