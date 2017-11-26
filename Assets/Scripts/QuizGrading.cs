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

	public RawImage FaceBubble;
	public Text BubbleMessage;
	public Text ButtonText;

	private readonly string GIRL = "GirlBubble";
	private readonly string BOY = "BoyBubble";
	private readonly string BUNNY = "RabbitScribeBubble";
	private readonly string HOME = "Home";
	private readonly string NEXT = "Next";
	private readonly string ENCOUNTER_MESSAGE = "I may be able to help this animal depending on its health values.";
	private readonly string CELEBRATORY_MESSAGE = "Congratulations! You found the animal in time to help. Keep up the good work.";
	private readonly string TOO_LATE_MESSAGE = "The biomagnification value is too high. Sorry, this animal can no longer be helped.";

	void Awake()
	{
		Event.Request.RegisterEvent<Animal> (GameEvent.QuizTime, SetAnimal);
	}
		
	void SetAnimal(Animal quizAnimal)
	{
		animal = quizAnimal;
		AssetManager.ShowAnimal (animal.Species);
		FaceBubble.texture = Service.Request.Player ().Avatar == Avatar.Girl ? Resources.Load<Texture> (GIRL) : Resources.Load<Texture> (BOY);
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
			BubbleMessage.text = ENCOUNTER_MESSAGE;
			Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
			AssetManager.HideAnimals ();
			ButtonText.text = NEXT;

			//Service will release the animal as soon as it has a chance
			//this allows the player to return to the map screen and continue on their journey
			bool error = Service.Request.ReleaseAnimal (animal);
			return;
		}

		if (!encounterFinished && (Random.Range (0, 20) >= 10 || animal.Species == AnimalSpecies.Horse))
		{
			FaceBubble.texture = Resources.Load<Texture> (BUNNY);
			BubbleMessage.text = CELEBRATORY_MESSAGE;
			encounterFinished = true;
			ButtonText.text = HOME;
			return;
		} 
		else if (!encounterFinished && (Random.Range(0, 20) < 10 || animal.Species != AnimalSpecies.Horse))
		{
			FaceBubble.texture = Resources.Load<Texture> (BUNNY);
			BubbleMessage.text = TOO_LATE_MESSAGE;
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
		Event.Request.UnregisterEvent<Animal> (GameEvent.QuizTime, SetAnimal);
	}
}
