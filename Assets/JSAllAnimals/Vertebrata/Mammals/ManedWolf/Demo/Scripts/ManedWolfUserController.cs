using UnityEngine;
using System.Collections;

public class ManedWolfUserController : MonoBehaviour {
	ManedWolfCharacter manedWolfCharacter;
	
	void Start () {
		manedWolfCharacter = GetComponent < ManedWolfCharacter> ();
	}
	
	void Update () {	
		if (Input.GetButtonDown ("Fire1")) {
			manedWolfCharacter.Attack();
		}
		if (Input.GetButtonDown ("Jump")) {
			manedWolfCharacter.Jump();
		}
		if (Input.GetKeyDown (KeyCode.F)) {
			manedWolfCharacter.Bite();
		}	
		if (Input.GetKeyDown (KeyCode.H)) {
			manedWolfCharacter.Hit();
		}
		if (Input.GetKeyDown (KeyCode.E)) {
			manedWolfCharacter.Eat();
		}
		if (Input.GetKeyDown (KeyCode.K)) {
			manedWolfCharacter.Death();
		}
		if (Input.GetKeyDown (KeyCode.L)) {
			manedWolfCharacter.Rebirth();
		}		
		if (Input.GetKeyDown (KeyCode.R)) {
			manedWolfCharacter.Roar();
		}		
		if (Input.GetKeyDown (KeyCode.J)) {
			manedWolfCharacter.SitDown();
		}		
		if (Input.GetKeyDown (KeyCode.N)) {
			manedWolfCharacter.Lie();
		}		
		if (Input.GetKeyDown (KeyCode.I)) {
			manedWolfCharacter.Idle();
		}		
		if (Input.GetKeyDown (KeyCode.M)) {
			manedWolfCharacter.Sleep();
		}		
		if (Input.GetKeyDown (KeyCode.Z)) {
			manedWolfCharacter.Walk();
		}		
		if (Input.GetKeyDown (KeyCode.X)) {
			manedWolfCharacter.Trot();
		}		
		if (Input.GetKeyDown (KeyCode.C)) {
			manedWolfCharacter.Canter();
		}		
		if (Input.GetKeyDown (KeyCode.V)) {
			manedWolfCharacter.Gallop();
		}		
		if (Input.GetKeyDown (KeyCode.B)) {
			manedWolfCharacter.GallopFast();
		}	
		if (Input.GetKeyDown (KeyCode.Q)) {
			manedWolfCharacter.SetMaxTurnSpeed(1f);
		}	
		if (Input.GetKeyUp (KeyCode.Q)) {
			manedWolfCharacter.SetMaxTurnSpeed(.5f);
		}	
	}
	
	private void FixedUpdate()
	{
		manedWolfCharacter.SetForwardSpeed(Input.GetAxis ("Vertical"));
		manedWolfCharacter.SetTurnSpeed(Input.GetAxis ("Horizontal"));
	}
}
