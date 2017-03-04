using UnityEngine;
using System.Collections;

public static class WebManager 
{
	public static T GetHttpResponse<T> (string url)
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
}