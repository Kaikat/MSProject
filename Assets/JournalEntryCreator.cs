using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class JournalEntryCreator : MonoBehaviour {

	string journalEntryPrefab = "JournalEntry";
	Object journalEntry;
	List<Object> journalEntries;

	void Awake()
	{
		journalEntries = new List<Object> ();
		Object journalEntry = AssetManager.LoadPrefab ("", journalEntryPrefab);
	}

	void Start () 
	{
		/*Dictionary<AnimalSpecies, List<Animal>> playerAnimals = Service.Request.Player ().GetAnimals ();
		foreach (KeyValuePair<AnimalSpecies, List<Animal>> keyPair in playerAnimals)
		{
			GameObject entry = (GameObject)GameObject.Instantiate (journalEntry);
			RawImage image = entry.GetComponent<RawImage> ();
			image.texture = Resources.Load<Sprite> (System.Enum.Parse(AnimalSpecies, keyPair.Key).ToString());
		}*/

	}
}
