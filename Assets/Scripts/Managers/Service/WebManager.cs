using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public static class WebManager 
{
	//Uses old WWW that is no longer recommended
	//Use the Get and Post functions instead
	public static T HttpResponse<T> (string url)
	{
		T response;
		WWW request = new WWW(url);
		while (!request.isDone)
		{
			new WaitForSeconds (1);
		}

		string json = request.text;
		response = JsonUtility.FromJson<T> (json);
		return response;
	}

	public static T GetHttpResponse<T> (string url)
	{
		T response;
		UnityWebRequest request = UnityWebRequest.Get (url);
		request.SetRequestHeader("Content-Type", "application/json");
		request.downloadHandler = new DownloadHandlerBuffer ();

		request.Send();
		while (!request.isDone)
		{
			new WaitForSeconds (1);
		}

		string json = request.downloadHandler.text;
		response = JsonUtility.FromJson<T> (json);
		return response;
	}

	public static T PostHttpResponse<T> (string url, string jsonPostData)
	{
		T response;
		UnityWebRequest request = UnityWebRequest.Post (url, jsonPostData);
		request.SetRequestHeader("Content-Type", "application/json");
		request.downloadHandler = new DownloadHandlerBuffer ();

		request.Send();
		while (!request.isDone)
		{
			new WaitForSeconds (1);
		}

		string json = request.downloadHandler.text;
		response = JsonUtility.FromJson<T> (json);
		return response;
	}
}