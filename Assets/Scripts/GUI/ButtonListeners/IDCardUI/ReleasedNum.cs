using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ReleasedNum : MonoBehaviour
{
	Text t;

	// Use this for initialization
	void Start () {
		t = GetComponent<Text> ();
	}

	// Update is called once per frame
	void Update () {
		t.text = "2";
		//t.text = StartGame.CurrentPlayer.GetReleased.ToString();
	}
}

