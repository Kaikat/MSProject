using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoginScreen : BasicScreen {

	Object LoginUI;
	GameObject LoginScreenInstance;

	//Button LoginButton;
	//Button CreateNewAccountButton;
	//Text ErrorLabel;

	public LoginScreen() : base(ScreenType.Login)
	{
		//LoginUI = AssetManager.LoadPrefab ("ScreenPrefabs", "LoginUI");
		LoginScreenInstance = GameObject.Find("LoginUIObject");

		//.GetComponent<GameObject>();

		//LoginScreenInstance.SetActive (false);

		//Text ErrorLabel = GameObject.Find ("LoginErrorLabel").GetComponent ();
		//LoginButton = GameObject.Find ("LoginButton").GetComponent ();
		//LoginButton.GetComponent<Button>().onClick.AddListener(() => MyMethod(index));
		//CreateNewAccountButton = GameObject.Find ("CreateNewAccountButton").GetComponent ();
	}

	public void Init()
	{

	}

	public override void Show()
	{
		//LoginScreenInstance = (GameObject)GameObject.Instantiate (LoginUI);
		LoginScreenInstance.SetActive(true);
	}

	public override void Destroy()
	{
		//GameObject.Destroy (LoginScreenInstance);
		LoginScreenInstance.SetActive(false);
	}
}
