using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JournalEntryManager : MonoBehaviour, IShowHideListener
{
	public GameObject journalScreen;
	public List<GameObject> JournalEntries;

	void Awake () 
	{
		journalScreen.GetComponent<TaggedShowHide> ().listener = this;
	}

	public void OnShow()
	{
		List<JournalEntry> journalEntries = Service.Request.PlayerJournal ();

		for (int i = 0; i < JournalEntries.Count; i++)
		{
			if (i < journalEntries.Count)
			{
				JournalEntries [i].SetActive (true);
				JournalEntry entry = journalEntries [i];

				JournalEntries [i].GetComponent<SetJournalEntry> ().SetJournalEntryElements (entry);
				JournalEntries [i].SetActive (true);
				/*if (entry.EncounterType == AnimalEncounterType.Released)
				{
					string releasedString = "Released: " + ConvertDate (entry.LatestEncounterDate.ToString ());
					string caughtString = "Caught: " + ConvertDate (entry.OldEncounterDate.ToString ());

					JournalEntries [i].GetComponent<SetJournalEntry> ().SetJournalEntryElements (entry.Species, caughtString, 
						entry.OldHealth1, entry.OldHealth2, entry.OldHealth3, releasedString, 
						entry.LatestHealth1, entry.LatestHealth2, entry.LatestHealth3);
				}
				else
				{
					string encounterString = entry.EncounterType.ToString () + " " + ConvertDate (entry.LatestEncounterDate.ToString ());
					if (entry.EncounterType == AnimalEncounterType.Caught)
					{
						JournalEntries [i].GetComponent<SetJournalEntry> ().SetJournalEntryElements (entry.Species, encounterString,
							entry.LatestHealth1, entry.LatestHealth2, entry.LatestHealth3);
					}
					else
					{
						JournalEntries [i].GetComponent<SetJournalEntry> ().SetJournalEntryElements (entry.Species, entry.LatestEncounterDate);
					}
				}*/


			}
			else
			{
				JournalEntries [i].SetActive (false);
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
