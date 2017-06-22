using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SurveyButton : MonoBehaviour {

	public Text SubmitText;
	public GameObject SurveyPart1;
	public GameObject SurveyPart2;
	public Text SurveyErrorText;

	//Part 1
	public GetPlayerSelection WorkingWithChildren;
	public GetPlayerSelection NatureAndOutdoors;
	public GetPlayerSelection HowPeopleThink;
	public GetPlayerSelection DesignAndStyle;
	public GetPlayerSelection HelpingOthers;
	public GetPlayerSelection GadgetsAndExperiments;
	public GetPlayerSelection Animals;

	//Part 2
	public GetPlayerSelection PuzzlesAndProblemSolving;
	public GetPlayerSelection MediaAndCurrentEvents;
	public GetPlayerSelection WorkingWithHands;
	public GetPlayerSelection Entertainment;
	public GetPlayerSelection SocialIssues;
	public GetPlayerSelection Writing;

	private readonly string SUBMIT = "Submit";
	private readonly string NEXT = "Next";
	private List<InterestValue> surveyResults;

	void Awake()
	{
		surveyResults = new List<InterestValue> ();
		SurveyPart1.SetActive (true);
	}

	public void Click()
	{
		SurveyErrorText.text = "";
		if (SubmitText.text == NEXT) 
		{
			if (isSurveyPart1Complete ()) 
			{
				SurveyPart1.SetActive (false);
				SurveyPart2.SetActive (true);
				SubmitText.text = SUBMIT;
			}
		} 
		else if (SubmitText.text == SUBMIT) 
		{
			if (isSurveyPart2Complete())
			{
				Service.Request.SendPlayerRatings (surveyResults);
				SurveyPart2.SetActive (false);
				PrintSurveyResults ();
				if (Service.Request.Player ().Avatar == Avatar.Default) 
				{
					EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Tutorial);
				} 
				else 
				{
					EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
				}
			}
		}
	}

	private bool isSurveyPart1Complete()
	{
		int[] results = new int[7];
		results[0] = WorkingWithChildren.LikertSelection();
		results[1] = NatureAndOutdoors.LikertSelection();
		results[2] = HowPeopleThink.LikertSelection();
		results[3] = DesignAndStyle.LikertSelection();
		results[4] = HelpingOthers.LikertSelection();
		results[5] = GadgetsAndExperiments.LikertSelection();
		results[6] = Animals.LikertSelection();

		Interest[] interests = { Interest.Working_With_Children, Interest.Nature_And_Outdoors,
			Interest.How_People_Think, Interest.Design_And_Style, Interest.Helping_Others,
			Interest.Gadgets_And_Experiments, Interest.Animals
		};
		if (isSurveyComplete (results)) 
		{
			for (int i = 0; i < results.Length; i++)
			{
				surveyResults.Add (new InterestValue(interests[i], results[i]));
			}
			return true;
		}

		return false;
	}

	private bool isSurveyPart2Complete()
	{
		int[] results = new int[6];
		results[0] = PuzzlesAndProblemSolving.LikertSelection();
		results[1] = MediaAndCurrentEvents.LikertSelection();
		results[2] = WorkingWithHands.LikertSelection();
		results[3] = Entertainment.LikertSelection();
		results[4] = SocialIssues.LikertSelection();
		results[5] = Writing.LikertSelection();

		Interest[] interests = { Interest.Puzzles_And_Problem_Solving, Interest.Media_And_Current_Events,
			Interest.Working_With_Hands, Interest.Entertainment, Interest.Social_Issues, Interest.Writing
		};
		if (isSurveyComplete (results)) 
		{
			for (int i = 0; i < results.Length; i++)
			{
				surveyResults.Add (new InterestValue(interests[i], results[i]));
			}
			return true;
		}

		return false;
	}

	private bool isSurveyComplete(int[] answers)
	{
		bool complete = true;
		foreach (int preference in answers) 
		{
			if (preference == -1) 
			{
				complete = false;
				SurveyErrorText.text = "Please rate everything.";
			}
		}
		return complete;
	}

	private void PrintSurveyResults()
	{
		string toPrint = "Survey Answers\n";
		foreach (InterestValue interestValue in surveyResults) 
		{
			toPrint += interestValue.Interest.ToString () + ": " + interestValue.Value.ToString () + "\n";
		}

		Debug.LogWarning (toPrint);
	}
}
