using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class IDCard : MonoBehaviour
{
	public Button ProfileButton;

	private readonly string GIRL_PROFILE = "GirlProfileButton";
	private readonly string BOY_PROFILE = "BoyProfileButton";

	void Start()
	{
		ProfileButton.image.overrideSprite = Service.Request.Player ().Avatar == Avatar.Girl ? 
			Resources.Load<Sprite> (UIConstants.BUTTON_ICON_PATH + GIRL_PROFILE) : 
			Resources.Load<Sprite> (UIConstants.BUTTON_ICON_PATH + BOY_PROFILE);
	}

	public void Click()
	{
		Event.Request.TriggerEvent (GameEvent.SwitchScreen, ScreenType.IDCard);
	}
}
