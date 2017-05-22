using UnityEngine;
using System.Collections;

public class RedCrownedCraneCharacterScript : MonoBehaviour {
	public Animator redCrownedCraneAnimator;
	public float redCrownedCraneSpeed=1f;
	Rigidbody redCrownedCraneRigid;
	public bool isFlying=false;
	public float groundCheckDistance=.3f;
	public float groundCheckOffset=.1f;
	public bool isGrounded=true;
	public float rotateSpeed=.2f;
	public float turnSpeed=0f;
	public float forwardSpeed=0f;


	
	void Start () {
		redCrownedCraneAnimator = GetComponent<Animator> ();
		redCrownedCraneAnimator.speed = redCrownedCraneSpeed;
		redCrownedCraneRigid = GetComponent<Rigidbody> ();
		if (isFlying) {
			redCrownedCraneAnimator.SetTrigger ("Soar");
			redCrownedCraneAnimator.applyRootMotion = false;
			isFlying = true;
		}
	}	

	void Update(){
		Move ();
		GroundedCheck ();
	}

	public void SpeedSet(float animSpeed){
		redCrownedCraneAnimator.speed = animSpeed;
	}
	
	public void Landing(){
		if (isFlying) {
			redCrownedCraneAnimator.SetTrigger ("Landing");
			redCrownedCraneAnimator.applyRootMotion = true;
			isFlying = false;
		}
	}
	
	public void Soar(){
		if (!isFlying) {
			redCrownedCraneAnimator.SetTrigger ("Soar");
			redCrownedCraneAnimator.applyRootMotion = false;
			isFlying = true;
		}
	}

	void GroundedCheck(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position+Vector3.up*groundCheckOffset, Vector3.down, out hit, groundCheckDistance)) {
			if (forwardSpeed==0f && turnSpeed==0f) {
				Landing ();
				isGrounded = true;
			}
		} else {
			isGrounded=false;
		}
	}

	public void Move(){
		redCrownedCraneAnimator.SetFloat ("Forward",forwardSpeed);
		redCrownedCraneAnimator.SetFloat ("Turn",turnSpeed);
		if(isFlying) {
			if (forwardSpeed > 0.1f) {
				redCrownedCraneRigid.AddForce ((transform.forward * 5f +transform.up*10f)* forwardSpeed);
			}else if(forwardSpeed<0.1f) {
				redCrownedCraneRigid.AddForce ((transform.forward * 2f +transform.up * 11f) * (-forwardSpeed));
			}else{
				redCrownedCraneRigid.AddForce (transform.up * 9f);
			}
			
			redCrownedCraneRigid.AddTorque(transform.up*turnSpeed*rotateSpeed);
			
		}
	}
}
