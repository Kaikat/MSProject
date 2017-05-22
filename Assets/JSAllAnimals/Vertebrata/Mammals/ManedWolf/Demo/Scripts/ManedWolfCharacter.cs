using UnityEngine;
using System.Collections;

public class ManedWolfCharacter : MonoBehaviour {
	Animator manedWolfAnimator;
	public bool jumpUp=false;
	public float groundCheckDistance = 0.1f;
	public float groundCheckOffset=0.01f;
	public GameObject leftFoot;
	public GameObject rightFoot;
	public GameObject leftHand;
	public GameObject rightHand;
	public bool leftFootIsGrounded;
	public bool rightFootIsGrounded;
	public bool leftHandIsGrounded;
	public bool rightHandIsGrounded;
	public bool isGrounded=true;
	public float jumpSpeed=1f;
	Rigidbody manedWolfRigid;
	public float forwardSpeed;
	public float turnSpeed;
	public float maxForwardSpeed=1f;
	public float maxTurnSpeed=.5f;

	public float jumpStartTime=0f;

	void Start () {
		manedWolfAnimator = GetComponent<Animator> ();
		manedWolfRigid=GetComponent<Rigidbody>();
	}
	
	void FixedUpdate(){
		CheckGroundStatus ();
		Move ();
		if (jumpUp) {
			jumpStartTime+=Time.deltaTime;
		}
	}
	
	public void Attack(){
		manedWolfAnimator.SetTrigger("Attack");
	}
	
	public void Bite(){
		manedWolfAnimator.SetTrigger("Bite");
	}


	public void Hit(){
		manedWolfAnimator.SetTrigger("Hit");
	}
	
	public void Eat(){
		manedWolfAnimator.SetTrigger("Eat");
	}
	
	public void Death(){
		manedWolfAnimator.SetTrigger("Death");
	}
	
	public void Rebirth(){
		manedWolfAnimator.SetTrigger("Rebirth");
	}
	
	public void Roar(){
		manedWolfAnimator.SetTrigger("Roar");
	}

	public void SitDown(){
		manedWolfAnimator.SetTrigger("SitDown");
	}
	
	public void Lie(){
		manedWolfAnimator.SetTrigger("Lie");
	}
	
	public void Sleep(){
		manedWolfAnimator.SetTrigger("Sleep");
	}
	
	public void Idle(){
		manedWolfAnimator.SetTrigger("Idle");
	}

	public void Jump(){
		if (isGrounded) {
			manedWolfAnimator.SetTrigger ("Jump");
			jumpUp = true;
			jumpStartTime=0f;
			isGrounded=false;
			manedWolfAnimator.SetBool("IsGrounded",false);
			if(rightHandIsGrounded){
				manedWolfAnimator.SetBool("JumpL",true);
			}else{
				manedWolfAnimator.SetBool("JumpL",false);
			}
		}
	}

	public void Walk(){
		maxForwardSpeed = .2f;
	}

	public void Trot(){
		maxForwardSpeed = .4f;
	}

	public void Canter(){
		maxForwardSpeed = .6f;
	}

	public void Gallop(){
		maxForwardSpeed = .8f;
	}

	public void GallopFast(){
		maxForwardSpeed = 1f;
	}
	
	public void Sneak(){
	}

	public void SetForwardSpeed(float f){
		forwardSpeed = f;
	}

	public void SetTurnSpeed(float t){
		turnSpeed = t;
	}

	public void SetMaxTurnSpeed(float t){
		maxTurnSpeed = t;
	}

	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		leftFootIsGrounded = Physics.Raycast (leftFoot.transform.position + (Vector3.right * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);
		rightFootIsGrounded = Physics.Raycast (rightFoot.transform.position + (rightFoot.transform.right * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);
		leftHandIsGrounded = Physics.Raycast (leftHand.transform.position + (Vector3.right * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);
		rightHandIsGrounded = Physics.Raycast (rightHand.transform.position + (Vector3.right * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);
		isGrounded = leftFootIsGrounded || rightFootIsGrounded || leftHandIsGrounded || rightHandIsGrounded;

		if (jumpUp) {
			if(jumpStartTime>.25f){
				jumpUp=false;
				manedWolfRigid.AddForce(transform.up*jumpSpeed+transform.forward*manedWolfRigid.velocity.sqrMagnitude,ForceMode.Impulse);
				manedWolfAnimator.applyRootMotion = false;
				manedWolfAnimator.SetBool("IsGrounded",false);
			}
		}

		if (isGrounded && !jumpUp) {
			manedWolfAnimator.applyRootMotion = true;
			manedWolfAnimator.SetBool ("IsGrounded", true);
		} else {
			if(!jumpUp){
				manedWolfAnimator.applyRootMotion = false;
				manedWolfAnimator.SetBool ("IsGrounded", false);
			}
		}
	}
	
	public void Move(){
		manedWolfAnimator.SetFloat ("Forward", forwardSpeed*maxForwardSpeed);
		manedWolfAnimator.SetFloat ("Turn", turnSpeed*maxTurnSpeed);
	}
}
