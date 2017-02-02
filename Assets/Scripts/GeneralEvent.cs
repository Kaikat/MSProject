using UnityEngine;
using UnityEngine.Events;
//using System.Collections;

public class GeneralEvent : UnityEvent<object> 
{
	private object field;
	public object Field {
		get { 
			return field; 
		}
		set {
			field = value; 
		}
	}
} //empty class; just needs to exist


/*
public FloatEvent onSomeFloatChange = new FloatEvent();

void SomethingThatInvokesTheEvent(){
	onSomeFloatChange.Invoke(3.14f);
}

//Elsewhere:
onSomeFloatChange.AddListener(SomeListener);

void SomeListener(float f){
	Debug.Log("Listened to change on value " + f); //prints "Listened to change on value 3.14"
}
*/