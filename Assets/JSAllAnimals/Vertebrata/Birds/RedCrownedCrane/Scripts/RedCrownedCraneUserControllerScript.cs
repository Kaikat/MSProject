using UnityEngine;
using System.Collections;

public class RedCrownedCraneUserControllerScript : MonoBehaviour {
	public RedCrownedCraneCharacterScript redCrownedCraneCharacter;
	
	void Start () {
		redCrownedCraneCharacter = GetComponent<RedCrownedCraneCharacterScript> ();	
	}
	
	void Update(){
		if (Input.GetButtonDown ("Jump")) {
			redCrownedCraneCharacter.Soar ();
		}
	}
	
	void FixedUpdate(){
		redCrownedCraneCharacter.forwardSpeed = Input.GetAxis ("Vertical");
		redCrownedCraneCharacter.turnSpeed = Input.GetAxis ("Horizontal");
	}
}
