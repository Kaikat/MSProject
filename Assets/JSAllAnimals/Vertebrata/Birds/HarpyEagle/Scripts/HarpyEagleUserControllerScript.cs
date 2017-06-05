using UnityEngine;
using System.Collections;

public class HarpyEagleUserControllerScript : MonoBehaviour {
	public HarpyEagleCharacterScript harpyEagleCharacter;
	public float upDownInputSpeed=3f;
	
	void Start () {
		harpyEagleCharacter = GetComponent<HarpyEagleCharacterScript> ();	
	}
	
	void Update(){
		if (Input.GetButtonDown ("Jump")) {
			harpyEagleCharacter.Soar ();
		}
		
		if (Input.GetKeyDown (KeyCode.J)) {
			harpyEagleCharacter.Jump ();
		}
		
		if (Input.GetKeyDown (KeyCode.H)) {
			harpyEagleCharacter.Hit ();
		}
		if (Input.GetKeyDown (KeyCode.N)) {
			harpyEagleCharacter.SitDown ();
		}
		
		if (Input.GetKeyDown (KeyCode.U)) {
			harpyEagleCharacter.StandUp ();
		}
		
		if (Input.GetKeyDown (KeyCode.K)) {
			harpyEagleCharacter.Down ();
		}
		
		if (Input.GetKeyDown (KeyCode.R)) {
			harpyEagleCharacter.Rebirth ();
		}
		if (Input.GetKeyDown (KeyCode.G)) {
			harpyEagleCharacter.Grooming ();
		}
		
		if (Input.GetButtonDown ("Fire2")) {
			harpyEagleCharacter.Attack2 ();
		}
		
		if (Input.GetKeyDown (KeyCode.V)) {
			harpyEagleCharacter.Call ();
		}
		
		if (Input.GetKeyDown (KeyCode.E)) {
			harpyEagleCharacter.Eat ();
		}
		if (Input.GetKeyDown (KeyCode.C)) {
			harpyEagleCharacter.CrouchStart ();
		}
		
		if (Input.GetKeyUp (KeyCode.C)) {
			harpyEagleCharacter.CrouchEnd ();
		}
		
		if (Input.GetKeyDown (KeyCode.R)) {
			harpyEagleCharacter.RunStart ();
		}
		
		if (Input.GetKeyUp (KeyCode.R)) {
			harpyEagleCharacter.RunEnd ();
		}
		
		if (Input.GetButtonDown ("Fire1")) {
			harpyEagleCharacter.Attack ();
		}
		if (Input.GetKey (KeyCode.N)) {
			harpyEagleCharacter.upDown=Mathf.Clamp(harpyEagleCharacter.upDown-Time.deltaTime*upDownInputSpeed,-1f,1f);
		}
		if (Input.GetKey (KeyCode.U)) {
			harpyEagleCharacter.upDown=Mathf.Clamp(harpyEagleCharacter.upDown+Time.deltaTime*upDownInputSpeed,-1f,1f);
		}
	}
	
	void FixedUpdate(){
		float v = Input.GetAxis ("Vertical");
		float h = Input.GetAxis ("Horizontal");	
		
		harpyEagleCharacter.forwardAcceleration = v;
		harpyEagleCharacter.yawVelocity = h;
	}
}

