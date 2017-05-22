using UnityEngine;
using System.Collections;

public class KomodoDragonUserController : MonoBehaviour {
	KomodoDragonCharacter komodoDragonCharacter;
	
	void Start () {
		komodoDragonCharacter = GetComponent < KomodoDragonCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			komodoDragonCharacter.Attack();
		}
		
		if (Input.GetKeyDown (KeyCode.H)) {
			komodoDragonCharacter.Hit();
		}
		
		if (Input.GetKeyDown (KeyCode.E)) {
			komodoDragonCharacter.Eat();
		}
		
		if (Input.GetKeyDown (KeyCode.K)) {
			komodoDragonCharacter.Death();
		}
		
		if (Input.GetKeyDown (KeyCode.R)) {
			komodoDragonCharacter.Rebirth();
		}		

		if (Input.GetKeyDown (KeyCode.I)) {
			komodoDragonCharacter.SwimStart();
		}	
		if (Input.GetKeyDown (KeyCode.M)) {
			komodoDragonCharacter.SwimEnd();
		}	



	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		komodoDragonCharacter.Move (v,h,v);
	}
}
