using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CaughtUIObject : MonoBehaviour {

	public Text AnimalNameTitle;
	public Text AnimalDescription;

	void Awake()
	{
		EventManager.RegisterEvent <AnimalSpecies> (GameEvent.Caught, SetTextFields);
	}

	void SetTextFields(AnimalSpecies animal)
	{
		AnimalNameTitle.text = animal.ToString ();
		AnimalDescription.text = Service.Request.AnimalDescription (animal);
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <AnimalSpecies> (GameEvent.Caught, SetTextFields);
	}
}
