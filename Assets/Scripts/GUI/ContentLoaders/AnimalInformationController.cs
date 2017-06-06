using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalInformationController : MonoBehaviour
{
    public RawImage AnimalImage;
    public Text AnimalNameTitle;
    public Text AnimalDescription;

    public Text HealthFactor1;
    public Text HealthFactor2;
    public Text HealthFactor3;

    public Button button;
    
    // For passing information between screens, and handling state
    public const string ANIMAL = "ANIMAL";
    public const string CALLING_SCREEN = "CALLING_SCREEN";
    private Animal animal;
    private ScreenType callingScreen;

    void Awake()
    {
        EventManager.RegisterEvent<Dictionary<string, object>>(GameEvent.ViewingAnimalInformation,
                                                               SetAnimalAndCallingScreen);
    }

    void Destroy()
    {
        EventManager.UnregisterEvent<Dictionary<string,object>>(GameEvent.ViewingAnimalInformation,
                                                                SetAnimalAndCallingScreen);
    }

    private void SetAnimalAndCallingScreen(Dictionary<string, object> eventDict)
    {
        animal = eventDict[ANIMAL] as Animal;
        callingScreen = (ScreenType)eventDict[CALLING_SCREEN];
    }

    /// <summary>
    /// Fills in fields for animal information.
    /// </summary>
    void SetTextFields()
    {
        // Populate fields
        AnimalImage.texture = Resources.Load<Texture>(animal.Species.ToString());
        AnimalNameTitle.text = Service.Request.AnimalName(animal.Species);
        AnimalDescription.text = Service.Request.AnimalDescription(animal.Species);
        // Populate health factors
        HealthFactor1.text = animal.Stats.Health1.ToString();
        HealthFactor2.text = animal.Stats.Health2.ToString();
        HealthFactor3.text = animal.Stats.Health3.ToString();
    }

    /// <summary>
    /// Sets the button object on Animal Information to route to the correct place, display correct text.
    /// </summary>
    public void Click()
    {
        switch (callingScreen)
        {
            case ScreenType.AnimalUnderObs:
                ClickForAnimalUnderObs();
                break;
            case ScreenType.CatchAnimal:
                ClickForCatchAnimal();
                break;
            case ScreenType.Journal:
                ClickForJournal();
                break;
        }
    }

    void ClickForAnimalUnderObs()
    {
        EventManager.TriggerEvent(GameEvent.ViewingAnimalsUnderObservation, animal.Species);
        EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.AnimalUnderObs);
    }

    void ClickForCatchAnimal()
    {
        if (Random.Range(0, 20) >= 15)
        {
            Service.Request.ReleaseAnimal(animal);
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Celebration);
        }
        else
        {
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Quiz);
            EventManager.TriggerEvent(GameEvent.QuizTime, animal);
        }
    }

    void ClickForJournal()
    {
        EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Journal);
    }
}
