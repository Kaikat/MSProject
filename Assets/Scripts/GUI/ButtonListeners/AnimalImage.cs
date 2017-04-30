using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnimalImage : MonoBehaviour {

	public RawImage image;

	void Awake()
	{
		EventManager.RegisterEvent <ScreenType> (GameEvent.SwitchScreen, SetSize);
		EventManager.RegisterEvent <AnimalSpecies> (GameEvent.AnimalEncounter, SetAnimalImage);

		image.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
	}

	void SetSize(ScreenType screen)
	{
		if (screen == ScreenType.Celebration || screen == ScreenType.Failure)
		{
			image.GetComponent<RectTransform> ().sizeDelta = new Vector2 (190, 190);
		}
		else
		{
			image.GetComponent<RectTransform> ().sizeDelta = new Vector2 (0, 0);
		}
	}

	void SetAnimalImage(AnimalSpecies animal)
	{
		image.texture = Resources.Load<Texture> (animal.ToString());
	}

	void Update ()
	{
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <ScreenType> (GameEvent.SwitchScreen, SetSize);
		EventManager.UnregisterEvent <AnimalSpecies> (GameEvent.AnimalEncounter, SetAnimalImage);
	}
}
