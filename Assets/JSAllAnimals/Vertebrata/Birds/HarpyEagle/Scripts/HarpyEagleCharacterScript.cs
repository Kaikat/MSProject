using UnityEngine;
using System.Collections;

public class HarpyEagleCharacterScript : MonoBehaviour {
	public Animator harpyEagleAnimator;
	Rigidbody harpyEagleRigid;
	public bool isFlying=false;
	public float upDown=0f;
	public float forwardAcceleration=0f;
	public float yawVelocity=0f;
	public float groundCheckDistance=5f;
	public bool soaring=false;
	public bool isGrounded=true;
	public float forwardSpeed=0f;
	public float maxForwardSpeed=3f;
	public float meanForwardSpeed=1.5f;
	public float speedDumpingTime=.1f;
	public float groundedCheckOffset=1f;
	public float runSpeed=1f;
	
	void Start(){
		harpyEagleAnimator = GetComponent<Animator> ();
		harpyEagleRigid = GetComponent<Rigidbody> ();
	}	   
	
	void Update(){
		Move ();
		if (harpyEagleAnimator.GetCurrentAnimatorClipInfo (0) [0].clip.name == "GlideForward" ) {
			if(soaring){
				soaring=false;
				harpyEagleAnimator.SetBool ("IsSoaring", false);
				harpyEagleAnimator.applyRootMotion = false;
			}
		}else if(harpyEagleAnimator.GetCurrentAnimatorClipInfo (0) [0].clip.name == "HoverOnce" ){
			forwardSpeed=meanForwardSpeed;
			harpyEagleAnimator.applyRootMotion = false;
			isFlying = true;
		}
		GroundedCheck ();
	}
	
	void GroundedCheck(){
		RaycastHit hit;
		if (Physics.Raycast (transform.position+Vector3.up*groundedCheckOffset, Vector3.down, out hit, groundCheckDistance)) {
			if (!soaring && !isGrounded ) {
				Landing ();
				isGrounded = true;		
			}
		} else {
			isGrounded=false;
		}
	}
	
	public void Landing(){
		harpyEagleAnimator.SetBool ("Landing",true);
		harpyEagleAnimator.applyRootMotion = true;
		harpyEagleRigid.useGravity = true;
		isFlying = false;
	}
	
	public void Soar(){
		if(isGrounded){
			harpyEagleAnimator.SetBool ("Landing",false);
			harpyEagleAnimator.SetBool ("IsSoaring", true);
			harpyEagleRigid.useGravity = false;
			
			soaring = true;
			isGrounded = false;
		}
	}
	
	public void Attack(){
		harpyEagleAnimator.SetTrigger ("Attack");
	}
	
	public void Jump(){
		harpyEagleAnimator.SetTrigger ("JumpLeftFoot");
	}
	
	public void Hit(){
		harpyEagleAnimator.SetTrigger ("Hit");
	}
	
	public void SitDown(){
		harpyEagleAnimator.SetTrigger ("SitDown");
	}
	
	public void StandUp(){
		harpyEagleAnimator.SetTrigger ("StandUp");
	}
	
	public void Down(){
		harpyEagleAnimator.SetTrigger ("Down");
	}
	
	public void Rebirth(){
		harpyEagleAnimator.SetTrigger ("Rebirth");
	}
	
	public void Grooming(){
		harpyEagleAnimator.SetTrigger ("Grooming");
	}

	public void Attack2(){
		harpyEagleAnimator.SetTrigger ("Attack2");
	}
	
	public void Call(){
		harpyEagleAnimator.SetTrigger ("Call");
	}
	
	public void Eat(){
		harpyEagleAnimator.SetTrigger ("Eat");
	}
	
	public void CrouchStart(){
		harpyEagleAnimator.SetBool ("Crouch",true);
	}
	
	public void CrouchEnd(){
		harpyEagleAnimator.SetBool ("Crouch",false);
	}
	
	public void RunStart(){
		runSpeed = 2f;
	}
	
	public void RunEnd(){
		runSpeed = 1f;
	}
	
	
	public void Move(){
		harpyEagleAnimator.SetFloat ("Forward",forwardAcceleration*runSpeed);
		harpyEagleAnimator.SetFloat ("Turn",yawVelocity);
		harpyEagleAnimator.SetFloat ("UpDown",upDown);
		harpyEagleAnimator.SetFloat ("UpVelocity",harpyEagleRigid.velocity.y);
		
		if(isFlying ) {
			
			if(forwardAcceleration<0f){
				harpyEagleRigid.velocity=transform.up*upDown*2f+transform.forward*forwardSpeed;	
			}else if(forwardAcceleration>0f){
				harpyEagleRigid.velocity=transform.up*(upDown*2f+(forwardSpeed-meanForwardSpeed))+transform.forward*forwardSpeed;				
			}else{
				harpyEagleRigid.velocity=transform.up*(upDown*2f+(forwardSpeed-maxForwardSpeed))+transform.forward*forwardSpeed;
			}
			
			transform.RotateAround(transform.position,Vector3.up,Time.deltaTime*yawVelocity*100f);
			forwardSpeed=Mathf.Lerp(forwardSpeed,Mathf.Min(meanForwardSpeed,forwardSpeed),Time.deltaTime*speedDumpingTime);
			forwardSpeed=Mathf.Clamp(forwardSpeed+forwardAcceleration*Time.deltaTime,0f,maxForwardSpeed);
			upDown=Mathf.Lerp(upDown,0,Time.deltaTime*3f);	
		}
	}
}
