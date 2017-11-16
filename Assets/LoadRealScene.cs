using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadRealScene : MonoBehaviour 
{
	private readonly string MAIN_SCENE = "ScreenManagerChange";
	private float timer = 1.0f;

	void Update ()
	{
		timer -= Time.deltaTime;
		if (timer <= 0.0f)
		{
			SceneManager.LoadScene(MAIN_SCENE, LoadSceneMode.Single);
		}
	}
}
