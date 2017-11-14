using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizGrading : MonoBehaviour
{
    private List<int> answers;
    private const int NumQuestions = 2;
	private Animal animal;
	private bool encounterFinished = false;

	public Text BunnyMessage;
	public Text ButtonText;

	private readonly string HOME = "Home";
	private readonly string NEXT = "Next";
	private readonly string ENCOUNTER_MESSAGE = "We may be able to help this animal depending on its health values.";
	private readonly string CELEBRATORY_MESSAGE = "Congratulations! You found the animal in time to help. Keep up the good work.";
	private readonly string TOO_LATE_MESSAGE = "The biomagnification value is too high. Sorry, this animal can no longer be helped.";

	void Awake()
	{
		EventManager.RegisterEvent<Animal> (GameEvent.QuizTime, SetAnimal);
	}
		
	void SetAnimal(Animal quizAnimal)
	{
		animal = quizAnimal;
		AssetManager.ShowAnimal (animal.Species);
	}

	void Start ()
    {
        // Initialize List of correct answers
        answers = new List<int>(NumQuestions);
		answers.Add (0);
		answers.Add (1);
	}

    public void Click()
    {
		if (encounterFinished)
		{
			encounterFinished = false;
			BunnyMessage.text = ENCOUNTER_MESSAGE;
			Service.Request.ReleaseAnimal (animal);
			EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
			AssetManager.HideAnimals ();
			ButtonText.text = NEXT;
			return;
		}

		if (!encounterFinished && (Random.Range (0, 20) >= 10 || animal.Species == AnimalSpecies.Horse))
		{
			BunnyMessage.text = CELEBRATORY_MESSAGE;
			encounterFinished = true;
			ButtonText.text = HOME;
			return;
		} 
		else if (!encounterFinished && (Random.Range(0, 20) < 10 || animal.Species != AnimalSpecies.Horse))
		{
			BunnyMessage.text = TOO_LATE_MESSAGE;
			encounterFinished = true;
			ButtonText.text = HOME;
			return;
		}

		/*if (Random.Range(0,20) >= 10 || animal.Species == AnimalSpecies.Horse)
        {
			Service.Request.ReleaseAnimal (animal);
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Celebration);
        } 
		else
        {
			Service.Request.ReleaseAnimal (animal);
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Failure);
        }

		AssetManager.HideAnimals ();*/
    }

	void Destroy()
	{
		EventManager.UnregisterEvent<Animal> (GameEvent.QuizTime, SetAnimal);
	}
}
