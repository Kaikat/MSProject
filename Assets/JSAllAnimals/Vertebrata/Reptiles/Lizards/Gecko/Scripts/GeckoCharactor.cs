using UnityEngine;
using System.Collections;

public class GeckoCharactor : MonoBehaviour {

	Animator geckoAnimator;
	Rigidbody geckoRigid;
	public float groundCheckDistance=.5f;
	public bool isGrounded=false;
	public float jumpSpeed=3f;
	public bool jumpUp=false;

	void Start () {
		geckoAnimator = GetComponent<Animator> ();
		geckoRigid = GetComponent<Rigidbody> ();
	}
	
	void Update () {
		GroundedCheck ();			
	}

	public void Attack(){
		geckoAnimator.SetTrigger("Attack");
	}

	public void AttackJump(){
		geckoAnimator.SetTrigger("AttackJump");
	}

	public void Hit(){
		geckoAnimator.SetTrigger("Hit");
	}


	public void Jump(){
		if (isGrounded) {
			geckoAnimator.applyRootMotion=false;
			geckoRigid.AddForce((transform.forward+transform.up)*jumpSpeed,ForceMode.Impulse);
			jumpUp=true;
			isGrounded=false;
			geckoAnimator.SetTrigger("Jump");
		}
	}
	
	void GroundedCheck(){
		if (geckoAnimator.GetCurrentAnimatorClipInfo (0) [0].clip.name == "Fall") {
			jumpUp=false;
		}
		if(!jumpUp){
	
			if (Physics.Raycast (transform.position+transform.up*.001f, Vector3.down,groundCheckDistance)) {
				isGrounded = true;
				geckoAnimator.SetBool ("IsGrounded", true);
				geckoAnimator.applyRootMotion=true;
			}else{
				isGrounded = false;
				geckoAnimator.SetBool ("IsGrounded", false);
			}

		}
	}
	
	public void Move(float v,float h){
		geckoAnimator.SetFloat ("Forward", v);
		geckoAnimator.SetFloat ("Turn", h);
	}
}
