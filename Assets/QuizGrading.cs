﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizGrading : MonoBehaviour {

    private List<int> answers;
    private const int NumQuestions = 2;

	// Use this for initialization
	void Start ()
    {
        // Initialize List of correct answers
        answers = new List<int>(NumQuestions);
        answers[0] = 0;
        answers[1] = 1;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void CheckAnswers(List<int> player_answers)
    {
        if (/*array contents equal*/ true)
        {
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Celebration);
        } else
        {
            EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Failure);
        }
    }
}