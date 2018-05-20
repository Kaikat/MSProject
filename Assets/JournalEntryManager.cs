using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JournalEntryManager : MonoBehaviour, IShowHideListener
{
	public GameObject JournalScreen;
	public List<GameObject> JournalEntries;

	void Awake () 
	{
		JournalScreen.GetComponent<TaggedShowHide> ().listener = this;
	}

	public void OnShow()
	{
        List<JournalEntry> journalEntryValues = Service.Request.PlayerJournal();
		if (journalEntryValues == null)
		{
			Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Menu);
			return;
		}

		for (int i = 0; i < JournalEntries.Count; i++)
		{
			if (i < journalEntryValues.Count)
			{
				JournalEntry entry = journalEntryValues [i];
				JournalEntries[i].GetComponent<SetJournalEntry> ().SetJournalEntryElements (entry);
				JournalEntries[i].SetActive(true);
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












































// Get Animal objects if in Journal
//    private Dictionary<int, Animal> journalAnimalObjects;

/*Player player = Service.Request.Player();
        journalAnimalObjects = player.GetAnimals().Values.SelectMany(x => x)
                                     .Concat(player.GetReleasedAnimals().Values.SelectMany(x => x))
                                     .Where(an => journalEntryValues.Any(entry => entry.AnimalID == an.AnimalID))
                                     .ToDictionary(x => x.AnimalID);*/

// Create local copy of index to pass into closure
/*int _i = i;
                JournalEntries[i].GetComponent<Button>().onClick.AddListener(() =>
                {
                    Animal animal = journalAnimalObjects[journalEntryValues[_i].AnimalID];
                    Dictionary<string, object> eventDict = new Dictionary<string, object>()
                    {
                        { AnimalInformationController.ANIMAL, animal },
                        { AnimalInformationController.CALLING_SCREEN, ScreenType.Journal }
                    };
                    EventManager.TriggerEvent(GameEvent.SwitchScreen, ScreenType.Caught);
                    EventManager.TriggerEvent(GameEvent.ViewingAnimalInformation, eventDict);
                });*/