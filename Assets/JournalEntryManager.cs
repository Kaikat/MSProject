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

	string ConvertDate(string dateString)
	{
		//2017-05-01 15:09:25
		string [] tokens = dateString.Split('-');
		string[] removeAfterSpace = tokens [2].Split (' ');
		return tokens [0] + "/" + tokens [1] + "/" + removeAfterSpace [0];
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

				if (entry.EncounterType == AnimalEncounterType.Released)
				{
					string releasedString = "Released: " + ConvertDate (entry.ReleaseDate);
					string caughtString = "Caught: " + ConvertDate (entry.EncounterDate);

					JournalEntries [i].GetComponent<SetJournalEntry> ().SetJournalEntryElements (entry.Species, caughtString, 
						entry.Health1, entry.Health2, entry.Health3, releasedString, 
						entry.ReleaseHealth1, entry.ReleaseHealth2, entry.ReleaseHealth3);
				}
				else
				{
					string encounterString = entry.EncounterType.ToString () + " " + ConvertDate (entry.EncounterDate);
					JournalEntries [i].GetComponent<SetJournalEntry> ().SetJournalEntryElements (entry.Species, encounterString,
						entry.Health1, entry.Health2, entry.Health3);
				}
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
