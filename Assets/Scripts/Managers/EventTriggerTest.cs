using UnityEngine;
using System.Collections;

public class EventTriggerTest : MonoBehaviour {


	void Update () {
		if (Input.GetKeyDown ("t"))
		{
			//EventManager.TriggerEvent(GameEvent.Test);
		}

		if (Input.GetKeyDown ("s"))
		{
			//EventManager.TriggerEvent (GameEvent.Destroy);
			//EventManager.TriggerEvent (GameEvent.Spawn);
		}

		if (Input.GetKeyDown ("d"))
		{
			//EventManager.TriggerEvent (GameEvent.Destroy);
		}

		if (Input.GetKeyDown ("x"))
		{
			//EventManager.TriggerEvent(GameEvent.Junk);
			//Service.Request.GetPlayerAnimals ();
			//Service.Request.GetAllAnimals();
		}
	}
}
