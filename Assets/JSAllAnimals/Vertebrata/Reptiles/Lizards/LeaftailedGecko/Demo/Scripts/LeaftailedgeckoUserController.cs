using UnityEngine;
using System.Collections;

public class LeaftailedgeckoUserController : MonoBehaviour {
	LeaftailedgeckoCharacter leaftailedgeckoCharacter;
	
	void Start () {
		leaftailedgeckoCharacter = GetComponent <LeaftailedgeckoCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			leaftailedgeckoCharacter.Attack();
		}
		if (Input.GetButtonDown ("Jump")) {
			leaftailedgeckoCharacter.Jump();
		}
		if (Input.GetKeyDown (KeyCode.H)) {
			leaftailedgeckoCharacter.Hit();
		}

		if (Input.GetKeyDown (KeyCode.V)) {
			leaftailedgeckoCharacter.JumpAttack1();
		}
		if (Input.GetKeyDown (KeyCode.B)) {
			leaftailedgeckoCharacter.JumpAttack2();
		}		

		if (Input.GetKeyDown (KeyCode.K)) {
			leaftailedgeckoCharacter.Death();
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			leaftailedgeckoCharacter.Rebirth();
		}		
		
		if (Input.GetKeyDown (KeyCode.R)) {
			leaftailedgeckoCharacter.Trot();
		}	
		if (Input.GetKeyUp (KeyCode.R)) {
			leaftailedgeckoCharacter.Walk();
		}	

		leaftailedgeckoCharacter.forwardSpeed=leaftailedgeckoCharacter.walkMode*Input.GetAxis ("Vertical");
		leaftailedgeckoCharacter.turnSpeed= Input.GetAxis ("Horizontal");
	}
}
