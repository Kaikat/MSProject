using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TryReloadButton : MonoBehaviour 
{
	//May have to remove
	public void Click ()
	{
		Service.Request.InitAgain ();
	}
}
