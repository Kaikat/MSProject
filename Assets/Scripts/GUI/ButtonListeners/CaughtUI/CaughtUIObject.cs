using UnityEngine;
using UnityEngine.UI;

public class CaughtUIObject : MonoBehaviour {

	public Text AnimalNameTitle;
	public Text AnimalDescription;
	public RawImage AnimalImage;

	void Awake()
	{
		EventManager.RegisterEvent <Animal> (GameEvent.AnimalCaught, SetTextFields);
	}

	void SetTextFields(Animal animal)
	{
		string animalName = animal.Species.ToString ();
		AnimalNameTitle.text = animalName;
		AnimalDescription.text = Service.Request.AnimalDescription (animal.Species);
		AnimalImage.texture = Resources.Load<Texture> (animalName);
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <Animal> (GameEvent.AnimalCaught, SetTextFields);
	}
}
