using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GetName : MonoBehaviour
{
	public Text PlayerName;
	const string title = "\nThe Field Biologist";

	void Start ()
	{
		PlayerName.text = Service.Request.Player().Name + title;
	}
}

