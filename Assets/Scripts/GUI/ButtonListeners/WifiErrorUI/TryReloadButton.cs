using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryReloadButton : MonoBehaviour 
{
	public void Click ()
	{
		Service.Request.InitAgain ();
	}
}
