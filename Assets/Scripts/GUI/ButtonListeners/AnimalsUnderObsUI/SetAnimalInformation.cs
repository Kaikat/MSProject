using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAnimalInformation : MonoBehaviour 
{
	public RawImage AnimalImage;
	public Text AnimalSpecies;
	public Text AnimalDescription;

	public Text HealthFactor1;
	public Text HealthFactor2;
	public Text Biomagnification;

	public Text ButtonText;

	public GameObject ConditionGraph;
	private Vector3[] NewVertices;
	private Vector2[] NewUV;

	void Awake()
	{
		EventManager.RegisterEvent<PreviousScreenData> (GameEvent.ObservedAnimalsPreviousScreen, SetScreenContent);
	}

	void Destroy()
	{
		EventManager.UnregisterEvent<PreviousScreenData> (GameEvent.ObservedAnimalsPreviousScreen, SetScreenContent);
	}

	public void SetScreenContent(PreviousScreenData screenData)
	{
		Animal animal = screenData.Data as Animal;

		// Populate fields
		AnimalImage.texture = Resources.Load<Texture>(UIConstants.ANIMAL_IMAGE_PATH + animal.Species.ToString());
		AnimalSpecies.text = Service.Request.AnimalEnglishName(animal.Species);
		AnimalSpecies.text += "\n" + Service.Request.AnimalSpanishName(animal.Species);
		AnimalSpecies.text += "\n" + Service.Request.AnimalNahuatlName(animal.Species);
		AnimalDescription.text = Service.Request.AnimalDescription(animal.Species);

		// Populate health factors
		HealthFactor1.text = animal.Stats.Health1.ToString();
		HealthFactor2.text = animal.Stats.Health2.ToString();
		Biomagnification.text = animal.Stats.Health3.ToString();

		SetButtonText (screenData.Screen);
		CreateTriangleGraph ();
	}

	private void SetButtonText(ScreenType screen)
	{
		switch (screen)
		{
			case ScreenType.AnimalUnderObs:
			case ScreenType.Journal:
				ButtonText.text = "Back";
			break;

			case ScreenType.CatchAnimal:
				ButtonText.text = "Next";
			break;
		}
	}

	private void CreateTriangleGraph()
	{
		Mesh mesh = ConditionGraph.GetComponent<MeshFilter>().mesh;
		mesh.Clear();
		mesh.vertices = new Vector3[] { new Vector3(0, 0, 0), new Vector3(-50, 50, 0), new Vector3(50, 50, 0) };
		mesh.uv = new Vector2[] { new Vector2(0, 0), new Vector2(0, 1), new Vector2(1, 1) };
		mesh.triangles = new int[] { 0, 1, 2 };
	}
}