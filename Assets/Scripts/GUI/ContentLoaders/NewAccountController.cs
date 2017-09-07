using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public class NewAccountController : MonoBehaviour, IShowHideListener
{
    public GameObject NewAccountScreen;
    public Dropdown DayDropdown;
    public Dropdown MonthDropdown;
    public Dropdown YearDropdown;

    private void Awake()
    {
        NewAccountScreen.GetComponent<TaggedShowHide>().listener = this;
    }

    public void OnShow()
    {
        DayDropdown.ClearOptions();
        MonthDropdown.ClearOptions();
        YearDropdown.ClearOptions();

		List<string> Months = new List<string> ();
		Months.Add ("Month");
		Months.AddRange(Enumerable.Range(1, 12).Select(x => x.ToString()));
		MonthDropdown.AddOptions (Months);

		List<string> Days = new List<string> ();
		Days.Add ("Day");
		Days.AddRange(Enumerable.Range(1, 31).Select(x => x.ToString()));
		DayDropdown.AddOptions (Days);

		List<string> Years = new List<string> ();
		Years.Add ("Year");
		Years.AddRange(Enumerable.Range(1900, 117).Select(x => x.ToString()).Reverse());
		YearDropdown.AddOptions(Years);
    }

    public void OnHide()
    {

    }
}