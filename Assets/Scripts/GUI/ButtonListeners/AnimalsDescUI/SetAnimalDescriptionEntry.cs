using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAnimalDescriptionEntry : MonoBehaviour 
{
	public Text AnimalSpecies;
	public Text Description;
	public Text Venue;

	public Image Background;
	public Image DataPanel;

	public void SetAnimalDescription(AnimalData animalData)
	{
		AnimalSpecies.text = animalData.Name;
		Description.text = animalData.Description;

		DataPanel.color = UIConstants.Beige;
		Background.color = UIConstants.Yellow;
	}
}
