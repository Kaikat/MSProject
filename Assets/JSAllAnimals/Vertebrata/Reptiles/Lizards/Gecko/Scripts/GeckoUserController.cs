using UnityEngine;
using System.Collections;

public class GeckoUserController : MonoBehaviour {
	GeckoCharactor geckoCharacter;
	float runSpeed=1f;

	void Start () {
		geckoCharacter = GetComponent < GeckoCharactor> ();
	}
	
	void Update () {

		if (Input.GetButtonDown ("Fire1")) {
			geckoCharacter.Attack();
		}

		if (Input.GetButtonDown ("Fire2")) {
			geckoCharacter.AttackJump();
		}

		if (Input.GetKeyDown (KeyCode.H)) {
			geckoCharacter.Hit();
		}

		if (Input.GetButtonDown ("Jump")) {
			geckoCharacter.Jump();
		}
		if (Input.GetKeyDown (KeyCode.R)) {
			runSpeed=2f;
		}
		if (Input.GetKeyUp (KeyCode.R)) {
			runSpeed=1f;
		}
	}
	
	private void FixedUpdate()
	{
		float h = Input.GetAxis ("Horizontal");
		float v = Input.GetAxis ("Vertical");
		geckoCharacter.Move (v*runSpeed,h*runSpeed);
	}

}
