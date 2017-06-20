using UnityEngine;
using UnityEngine.UI;
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

        DayDropdown.AddOptions(Enumerable.Range(1, 31)
                                         .Select(x => new Dropdown.OptionData(x.ToString()))
                                         .ToList());
        MonthDropdown.AddOptions(Enumerable.Range(1, 12)
                                           .Select(x => new Dropdown.OptionData(x.ToString()))
                                           .ToList());
        YearDropdown.AddOptions(Enumerable.Range(1900, 117)
                                           .Reverse()
                                           .Select(x => new Dropdown.OptionData(x.ToString()))
                                           .ToList());
    }

    public void OnHide()
    {

    }
}