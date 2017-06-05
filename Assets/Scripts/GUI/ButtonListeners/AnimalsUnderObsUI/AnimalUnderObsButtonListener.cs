using UnityEngine;

public class AnimalUnderObsButtonListener : MonoBehaviour
{
    public readonly Animal CurrentAnimal;

    public AnimalUnderObsButtonListener(Animal animal)
    {
        CurrentAnimal = animal;
    }

    public void Click()
    {
        GameStateManager.CurrentAnimal = CurrentAnimal;
        GameStateManager.CurrentAnimalSpecies = CurrentAnimal.Species;
        EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.AnimalUnderObsDetail);
    }
}
