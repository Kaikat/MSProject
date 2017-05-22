#pragma strict
var dragonfly : Animator;


function Start () {

}

function Update () {
if(Input.GetKey(KeyCode.Alpha1)){
dragonfly.SetBool("Idle",true);
dragonfly.SetBool("Idle2",false);
}
if(Input.GetKey(KeyCode.Alpha2)){
dragonfly.SetBool("Idle2",true);
dragonfly.SetBool("Idle",false);
}
if(Input.GetKey(KeyCode.Alpha3)){
dragonfly.SetBool("Takeoff",true);
dragonfly.SetBool("Idle2",false);
dragonfly.SetBool("Walk",false);
}
if(Input.GetKey(KeyCode.Alpha4)){
dragonfly.SetBool("Fly",true);
dragonfly.SetBool("Takeoff",false);
}
if(Input.GetKey(KeyCode.Alpha5)){
dragonfly.SetBool("Landing",true);
dragonfly.SetBool("Fly",false);
}
if(Input.GetKey(KeyCode.Alpha6)){
dragonfly.SetBool("Walk",true);
dragonfly.SetBool("Landing",false);
}
if(Input.GetKey(KeyCode.Alpha7)){
dragonfly.SetBool("Die",true);
dragonfly.SetBool("Walk",false);
dragonfly.SetBool("Fly",false);
}
}