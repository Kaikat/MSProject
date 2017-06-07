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
