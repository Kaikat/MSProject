using UnityEngine;
using System.Collections;

public class WebManager : MonoBehaviour {
	public static WebManager instance;
	public void Start()
	{
		instance = this;
	}

	public T GetHttpResponse<T> (string url)
	{
		T response;
		WWW request = new WWW(url);
		while (!request.isDone)
		{
			new WaitForSeconds (1);
		}
		string json = request.text;
		response = JsonUtility.FromJson<T>(json);
		return response;
	}
}