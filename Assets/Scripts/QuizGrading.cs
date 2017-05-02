using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizGrading : MonoBehaviour {

    private List<int> answers;
    private const int NumQuestions = 2;
	private Animal animal;

	void Awake()
	{
		EventManager.RegisterEvent<Animal> (GameEvent.QuizTime, SetAnimal);
	}

	void SetAnimal(Animal quizAnimal)
	{
		animal = quizAnimal;
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
        if (Random.Range(0,20)>=10)
        {
			Service.Request.ReleaseAnimal (animal);
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Celebration);
        } 
		else
        {
			Service.Request.ReleaseAnimal (animal);
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Failure);
        }
    }

	void Destroy()
	{
		EventManager.UnregisterEvent<Animal> (GameEvent.QuizTime, SetAnimal);
	}
}
