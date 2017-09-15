using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToGenderScreen : MonoBehaviour 
{
	public Text RabbitChatBubble;
	private int chatIndex = 0;
	private readonly string[] RabbitChats = {
		"This game will guide you through the UCSB campus and introduce you to some of the native environment.",
		"Banners are set up around campus at locations associated with different UCSB Majors. At each banner, " +
		"you also will encounter animals or other elements of the environment. You can \"scan\" them to check " +
		"on their health, which is related to environmental conditions.",
		"You'll be able to \"help\" most animals just by checking on them, but some will have a " +
		"\"biomagnification\" score that is so high, they can't recover.",
		"Choose a banner on the map and walk toward it. Let me be your guide!"
	};

	public void Click()
	{
		RabbitChatBubble.text = RabbitChats[chatIndex];
		chatIndex++;

		if (chatIndex == RabbitChats.Length && Service.Request.Player ().Avatar == Avatar.Default)
		{
			chatIndex = 0;
			EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.Gender);
		} 
		else if (chatIndex == RabbitChats.Length && Service.Request.Player ().Avatar != Avatar.Default)
		{
			chatIndex = 0;
			EventManager.TriggerEvent (GameEvent.SwitchScreen, ScreenType.GoMapHome);
		}
	}
}
