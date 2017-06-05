using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetAvatarImage : MonoBehaviour 
{
	public RawImage PlayerAvatar;

	void Start () 
	{ 
		PlayerAvatar.texture = Resources.Load<Texture> (Service.Request.Player().Avatar.ToString());
	}
}
