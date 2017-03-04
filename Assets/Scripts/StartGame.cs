using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	public static Player CurrentPlayer;

	void Start () {
		CurrentPlayer = new Player ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
