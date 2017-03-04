using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CatchAnimal : MonoBehaviour
{
	public Text SpawnHint;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	public void Click()
	{
		EventManager.TriggerEvent (GameEvent.Caught, ScreenType.Caught);
		Service.Request.CatchAnimal (SpawnHint.text.ToEnum<AnimalSpecies>());
		SpawnHint.text = "";
		AssetManager.HideAnimals ();
	}
}

