using UnityEngine;
using System.Collections;

public class LeaftailedgeckoCharacter : MonoBehaviour {
	Animator leaftailedgeckoAnimator;
	public bool jumpStart=false;
	public float groundCheckDistance = 0.1f;
	public float groundCheckOffset=0.01f;
	public bool isGrounded=true;
	public float jumpSpeed=3f;
	Rigidbody leaftailedgeckoRigid;
	public float forwardSpeed;
	public float turnSpeed;
	public float walkMode=1f;
	public float jumpStartTime=0f;
	
	void Start () {
		leaftailedgeckoAnimator = GetComponent<Animator> ();
		leaftailedgeckoRigid=GetComponent<Rigidbody>();
	}
	
	void FixedUpdate(){
		CheckGroundStatus ();
		Move ();
		jumpStartTime+=Time.deltaTime;
	}
	
	public void Attack(){
		leaftailedgeckoAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		leaftailedgeckoAnimator.SetTrigger("Hit");
	}

	public void JumpAttack1(){
		leaftailedgeckoAnimator.SetTrigger("JumpAttack1");
	}

	public void JumpAttack2(){
		leaftailedgeckoAnimator.SetTrigger("JumpAttack2");
	}

	public void Death(){
		leaftailedgeckoAnimator.SetBool("IsLived",false);
	}
	
	public void Rebirth(){
		leaftailedgeckoAnimator.SetBool("IsLived",true);
	}

	public void Trot(){
		walkMode = 2f;
	}
	
	public void Walk(){
		walkMode = 1f;
	}
	
	public void Jump(){
		if (isGrounded) {
			leaftailedgeckoAnimator.SetBool ("JumpStart",true);
			jumpStart = true;
			jumpStartTime=0f;
			isGrounded=false;
			leaftailedgeckoAnimator.SetBool("IsGrounded",false);
		}
	}
	
	void CheckGroundStatus()
	{
		RaycastHit hitInfo;
		isGrounded = Physics.Raycast (transform.position + (transform.up * groundCheckOffset), Vector3.down, out hitInfo, groundCheckDistance);
		
		if (jumpStart) {
			if(jumpStartTime>.15f){
				jumpStart=false;
				leaftailedgeckoAnimator.SetBool ("JumpStart",false);
				leaftailedgeckoRigid.AddForce((transform.up+transform.forward*forwardSpeed)*jumpSpeed,ForceMode.Impulse);
				leaftailedgeckoAnimator.applyRootMotion = false;
				leaftailedgeckoAnimator.SetBool("IsGrounded",false);
			}
		}
		
		if (isGrounded && !jumpStart && jumpStartTime>.3f) {
			leaftailedgeckoAnimator.applyRootMotion = true;
			leaftailedgeckoAnimator.SetBool ("IsGrounded", true);
		} else {
			if(!jumpStart){
				leaftailedgeckoAnimator.applyRootMotion = false;
				leaftailedgeckoAnimator.SetBool ("IsGrounded", false);
			}
		}
	}
	
	public void Move(){
		leaftailedgeckoAnimator.SetFloat ("Forward", forwardSpeed*walkMode);
		leaftailedgeckoAnimator.SetFloat ("Turn", turnSpeed);
	}
}
