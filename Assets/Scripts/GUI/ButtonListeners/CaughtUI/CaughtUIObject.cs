using UnityEngine;
using UnityEngine.UI;

public class CaughtUIObject : MonoBehaviour {
	
	public RawImage AnimalImage;
	public Text AnimalNameTitle;
	public Text AnimalDescription;

	public Text HealthFactor1;
	public Text HealthFactor2;
	public Text HealthFactor3;

	void Awake()
	{
		EventManager.RegisterEvent <Animal> (GameEvent.AnimalCaught, SetTextFields);
	}

	void SetTextFields(Animal animal)
	{
		string animalName = animal.Species.ToString ();
		AnimalImage.texture = Resources.Load<Texture> (animalName);
		AnimalNameTitle.text = animalName;
		AnimalDescription.text = Service.Request.AnimalDescription (animal.Species);

		HealthFactor1.text = animal.Stats.Health1.ToString ();
		HealthFactor2.text = animal.Stats.Health2.ToString ();
		HealthFactor3.text = animal.Stats.Health3.ToString ();
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <Animal> (GameEvent.AnimalCaught, SetTextFields);
	}
}
