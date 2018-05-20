using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAnimalDescriptionEntry : MonoBehaviour 
{
	public RawImage AnimalImage;
	public Text AnimalSpecies;
	public Text SpanishName;
	public Text NahuatlName;
	public Text Description;
	public Text Venue;

	public Image Background;
	public Image DataPanel;

	private readonly string SPANISH = "Spanish: ";
	private readonly string NAHUATL = "Nahuatl: ";

	public void SetAnimalDescription(AnimalData animalData)
	{
		AnimalImage.texture = Resources.Load<Texture> (UIConstants.ANIMAL_IMAGE_PATH + animalData.Species.ToString ());
		AnimalSpecies.text = animalData.Name;
		SpanishName.text = SPANISH + animalData.SpanishName;
		NahuatlName.text = NAHUATL + animalData.NahuatlName;
		Description.text = animalData.Description;

		DataPanel.color = UIConstants.Beige;
		Background.color = UIConstants.Yellow;
	}
}
