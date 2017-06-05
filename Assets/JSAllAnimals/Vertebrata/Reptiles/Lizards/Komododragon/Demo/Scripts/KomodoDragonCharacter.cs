using UnityEngine;
using System.Collections;

public class KomodoDragonCharacter : MonoBehaviour {
	Animator komodoDragonAnimator;
	public bool isSwimming=false;
	Rigidbody komodoRigid;

	void Start () {
		komodoDragonAnimator = GetComponent<Animator> ();
		komodoRigid = GetComponent<Rigidbody> ();
	}
	
	public void Attack(){
		komodoDragonAnimator.SetTrigger("Attack");
	}
	
	public void Hit(){
		komodoDragonAnimator.SetTrigger("Hit");
	}
	
	public void Eat(){
		komodoDragonAnimator.SetTrigger("Eat");
	}
	
	public void Death(){
		komodoDragonAnimator.SetTrigger("Death");
	}
	
	public void Rebirth(){
		komodoDragonAnimator.SetTrigger("Rebirth");
	}
	
	public void SwimStart(){
		komodoDragonAnimator.SetBool("IsSwimming",true);
		isSwimming = true;
		komodoRigid.useGravity = false;
		komodoDragonAnimator.applyRootMotion = false;
		komodoRigid.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
	}	

	public void SwimEnd(){
		komodoDragonAnimator.SetBool("IsSwimming",false);
		isSwimming = false;
		komodoRigid.useGravity = true;
		komodoDragonAnimator.applyRootMotion = true;
		komodoRigid.constraints = RigidbodyConstraints.FreezeRotation;
	}	


	public void Move(float v,float h,float u){
		komodoDragonAnimator.SetFloat ("Forward", v);
		komodoDragonAnimator.SetFloat ("Turn", h);
		if (isSwimming) {
			komodoDragonAnimator.SetFloat ("UpSpeed", u);
			komodoRigid.AddForce(transform.up*u+transform.forward*8f);
			komodoRigid.AddTorque(transform.up*h);
		}
	}
}
