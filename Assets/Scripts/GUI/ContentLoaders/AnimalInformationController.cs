using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Moved to SetAnimalInformation.cs
public class AnimalInformationController : MonoBehaviour
{
    public RawImage AnimalImage;
    public Text AnimalNameTitle;
    public Text AnimalDescription;

    public Text HealthFactor1;
    public Text HealthFactor2;
    public Text HealthFactor3;

    public Button BottomButton;
    public Text ButtonText;

    public GameObject ConditionGraph;
    private Vector3[] NewVertices;
    private Vector2[] NewUV;
    
    // For passing information between screens, and handling state
    public const string ANIMAL = "ANIMAL";
    public const string CALLING_SCREEN = "CALLING_SCREEN";
    private Animal animal;
    private ScreenType callingScreen;

    void Awake()
    {
        EventManager.RegisterEvent<Dictionary<string, object>>(GameEvent.ViewingAnimalInformation,
                                                               SetScreenContent);
    }

    void Destroy()
    {
        EventManager.UnregisterEvent<Dictionary<string,object>>(GameEvent.ViewingAnimalInformation,
                                                                SetScreenContent);
    }

    private void SetScreenContent(Dictionary<string, object> eventDict)
    {
        animal = eventDict[ANIMAL] as Animal;
        callingScreen = (ScreenType)eventDict[CALLING_SCREEN];
        SetAnimalFieldsAndButton();
    }

    /// <summary>
    /// Fills in fields for animal information.
    /// </summary>
    private void SetAnimalFieldsAndButton()
    {
        // Populate fields
		AnimalImage.texture = Resources.Load<Texture>(UIConstants.ANIMAL_IMAGE_PATH + animal.Species.ToString());
        AnimalNameTitle.text = Service.Request.AnimalName(animal.Species);
        AnimalDescription.text = Service.Request.AnimalDescription(animal.Species);
        // Populate health factors
        HealthFactor1.text = animal.Stats.Health1.ToString();
        HealthFactor2.text = animal.Stats.Health2.ToString();
        HealthFactor3.text = animal.Stats.Health3.ToString();
        // Set behavior of button and text
        BottomButton.onClick.AddListener(Click);
        switch (callingScreen)
        {
            case ScreenType.AnimalUnderObs:
            case ScreenType.Journal:
                ButtonText.text = "Back";
                break;
            case ScreenType.CatchAnimal:
                ButtonText.text = "Next";
                break;
        }
        // Create triangles?
        Mesh mesh = ConditionGraph.GetComponent<MeshFilter>().mesh;
        mesh.Clear();
        mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(-50, 50, 0), new Vector3(50, 50, 0) };
        mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
        mesh.triangles = new int[] { 0, 1, 2 };
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

    private void ClickForAnimalUnderObs()
    {
        EventManager.TriggerEvent(GameEvent.ViewingAnimalsUnderObservation, animal.Species);
        EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.AnimalUnderObs);
    }

    private void ClickForCatchAnimal()
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

    private void ClickForJournal()
    {
        EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Journal);
    }
}
