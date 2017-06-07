using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalEntryManager : MonoBehaviour, IShowHideListener
{
	public GameObject JournalScreen;
	public List<GameObject> JournalEntries;
    private Dictionary<int, Animal> journalAnimalObjects;

	void Awake () 
	{
		JournalScreen.GetComponent<TaggedShowHide> ().listener = this;
	}

	public void OnShow()
	{
        List<JournalEntry> journalEntryValues = Service.Request.PlayerJournal();

        // Get Animal objects if in Journal
        Player player = Service.Request.Player();
        journalAnimalObjects = player.GetAnimals().Values.SelectMany(x => x)
                                     .Concat(player.GetReleasedAnimals().Values.SelectMany(x => x))
                                     .Where(an => journalEntryValues.Any(entry => entry.AnimalID == an.AnimalID))
                                     .ToDictionary(x => x.AnimalID);

		for (int i = 0; i < JournalEntries.Count; i++)
		{
			if (i < journalEntryValues.Count)
			{
				JournalEntries[i].SetActive(true);
				JournalEntry entry = journalEntryValues [i];

				JournalEntries[i].GetComponent<SetJournalEntry> ().SetJournalEntryElements (entry);
				JournalEntries[i].SetActive(true);

                // Create local copy of index to pass into closure
                int _i = i;
                JournalEntries[i].GetComponent<Button>().onClick.AddListener(() =>
                    {
                        Animal animal = journalAnimalObjects[journalEntryValues[_i].AnimalID];
                        Dictionary<string, object> eventDict = new Dictionary<string, object>()
                        {
                            { AnimalInformationController.ANIMAL, animal },
                            { AnimalInformationController.CALLING_SCREEN, ScreenType.Journal }
                        };
                        EventManager.TriggerEvent(GameEvent.ViewingAnimalInformation, eventDict);
                        EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Caught);
                    });
			}
			else
			{
				JournalEntries[i].SetActive (false);
			}
		}
	}

    public void OnHide()
	{
	}

	void Destroy ()
	{
	}
}
