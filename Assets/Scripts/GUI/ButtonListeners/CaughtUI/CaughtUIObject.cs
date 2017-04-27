using UnityEngine;
using UnityEngine.UI;

public class CaughtUIObject : MonoBehaviour {

	public Text AnimalNameTitle;
	public Text AnimalDescription;
	public RawImage AnimalImage;

	void Awake()
	{
		EventManager.RegisterEvent <AnimalSpecies> (GameEvent.Caught, SetTextFields);
	}

	void SetTextFields(AnimalSpecies animal)
	{
		string animalName = animal.ToString ();
		AnimalNameTitle.text = animalName;
		AnimalDescription.text = Service.Request.AnimalDescription (animal);
		AnimalImage.texture = Resources.Load<Texture> (animalName);
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <AnimalSpecies> (GameEvent.Caught, SetTextFields);
	}
}
