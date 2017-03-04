using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Description : MonoBehaviour
{
	public Text SpawnHint;
	public Text AnimalDescription;

	// Use this for initialization
	void Start ()
	{
		/*t = GetComponent<Text> ();
		if (SpawnAnimal.animal.name.ToString().Equals("Tiger(Clone)")) {
			t.text = "The tiger (Panthera tigris) is the largest cat species, most recognisable for their pattern of dark vertical stripes on reddish-orange fur with a lighter underside.";
		} else {
			t.text = "Butterflies are insects in the macrolepidopteran clade Rhopalocera from the order Lepidoptera, which also includes moths. ";
		}*/

		if (SpawnHint.text == AnimalSpecies.Tiger.ToString())
		{
			AnimalDescription.text = "The tiger (Panthera tigris) is the largest cat species, most recognisable for their pattern of dark vertical stripes on reddish-orange fur with a lighter underside.";
		}
		else if (SpawnHint.text == AnimalSpecies.Butterfly.ToString())
		{
			AnimalDescription.text = "Butterflies are insects in the macrolepidopteran clade Rhopalocera from the order Lepidoptera, which also includes moths. ";
		}

	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}

