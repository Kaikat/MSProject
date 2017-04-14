using UnityEngine;
using UnityEngine.Events;
using System.Collections;

using System;
using System.Collections.Generic;

public class EventManager : MonoBehaviour 
{
	private Dictionary<string, List<Delegate>> listeners;
	private Dictionary<string, List<Delegate>> toRemove;

	private static EventManager eventManager;

	public static EventManager instance
	{
		get 
		{
			if (!eventManager) 
			{
				eventManager = FindObjectOfType (typeof(EventManager)) as EventManager;

				if (!eventManager) 
				{
					Debug.LogError ("There needs to be one active EventManager script on a GameObject in your scene.");
				} 
				else 
				{
					eventManager.Init (); 
				}
			}

			return eventManager;
		}
	}

	void Init()
	{
		listeners = new Dictionary<string, List<Delegate>>();
		toRemove = new Dictionary<string, List<Delegate>>();
	}

	public static void RegisterEvent (GameEvent eventName, Callback callback)
	{
		string message = eventName.ToString ();
		if (!instance.listeners.ContainsKey(message))
		{
			instance.listeners[message] = new List<Delegate>();
			instance.toRemove[message] = new List<Delegate>();
		}
		instance.listeners[message].Add(callback);
	}

	//public void AddListener<A>(string message, Callback<A> callback)

	public static void RegisterEvent<A> (GameEvent eventName, Callback<A> callback)
	{
		string message = eventName.ToString ();
		if (!instance.listeners.ContainsKey(message))
		{
			instance.listeners[message] = new List<Delegate>();
			instance.toRemove[message] = new List<Delegate>();
		}
		instance.listeners[message].Add(callback);
	}

	public void RegisterEvent<A, B>(GameEvent eventName, Callback<A, B> callback)
	{
		string message = eventName.ToString ();

		if (!listeners.ContainsKey(message))
		{
			listeners[message] = new List<Delegate>();
			toRemove[message] = new List<Delegate>();
		}
		listeners[message].Add(callback);
	}

	public void RegisterEvent<A, B, C>(GameEvent eventName, Callback<A, B, C> callback)
	{
		string message = eventName.ToString ();

		if (!listeners.ContainsKey(message))
		{
			listeners[message] = new List<Delegate>();
			toRemove[message] = new List<Delegate>();
		}
		listeners[message].Add(callback);
	}

	public void RegisterEvent<A, B, C, D>(GameEvent eventName, Callback<A, B, C, D> callback)
	{
		string message = eventName.ToString ();

		if (!listeners.ContainsKey(message))
		{
			listeners[message] = new List<Delegate>();
		}
		listeners[message].Add(callback);
	}

	public static void UnregisterEvent (GameEvent eventName, Callback callback)
	{
		string message = eventName.ToString ();
		if (instance.listeners.ContainsKey(message))
		{
			instance.toRemove[message].Add(callback);
		}
	}

	public static void UnregisterEvent<A> (GameEvent eventName, Callback<A> callback)
	{
		string message = eventName.ToString ();

		if (instance.listeners.ContainsKey(message))
		{
			instance.toRemove[message].Add(callback);
		}
	}

	public void UnregisterEvent<A, B>(GameEvent eventName, Callback<A, B> callback)
	{
		string message = eventName.ToString ();

		if (listeners.ContainsKey(message))
		{
			toRemove[message].Add(callback);
		}
	}

	public void UnregisterEvent<A, B, C>(GameEvent eventName, Callback<A, B, C> callback)
	{
		string message = eventName.ToString ();

		if (listeners.ContainsKey(message))
		{
			toRemove[message].Add(callback);
		}
	}

	public void UnregisterEvent<A, B, C, D>(GameEvent eventName, Callback<A, B, C, D> callback)
	{
		string message = eventName.ToString ();

		if (listeners.ContainsKey(message))
		{
			toRemove[message].Add(callback);
		}
	}

	public static void TriggerEvent (GameEvent eventName)
	{
		string message = eventName.ToString ();

		if (instance.listeners.ContainsKey(message))
		{
			foreach (Delegate callbackDelegate in instance.listeners[message])
			{
				Callback callback = callbackDelegate as Callback;
				if (callback != null)
				{
					callback();
				}
			}
			CleanUp(message);
		}
	}

	public static void TriggerEvent<A>(GameEvent eventName, A parameterOne)
	{
		string message = eventName.ToString ();

		if (instance.listeners.ContainsKey(message))
		{
			foreach (Delegate callbackDelegate in instance.listeners[message])
			{
				Callback<A> callback = callbackDelegate as Callback<A>;
				if (callback != null)
				{
					callback(parameterOne);
				}
			}
			CleanUp(message);
		}
	}

	public void TriggerEvent<A, B>(GameEvent eventName, A parameterOne, B parameterTwo)
	{
		string message = eventName.ToString ();
		if (listeners.ContainsKey(message))
		{
			foreach (Delegate callbackDelegate in listeners[message])
			{
				Callback<A, B> callback = callbackDelegate as Callback<A, B>;
				if (callback != null)
				{
					callback(parameterOne, parameterTwo);
				}
			}
			CleanUp(message);
		}
	}

	public void TriggerEvent<A, B, C>(GameEvent eventName, A parameterOne, B parameterTwo, C parameterThree)
	{
		string message = eventName.ToString ();

		if (listeners.ContainsKey(message))
		{
			foreach (Delegate callbackDelegate in listeners[message])
			{
				Callback<A, B, C> callback = callbackDelegate as Callback<A, B, C>;
				if (callback != null)
				{
					callback(parameterOne, parameterTwo, parameterThree);
				}
			}
			CleanUp(message);
		}
	}

	public void TriggerEvent<A, B, C, D>(GameEvent eventName, A parameterOne, B parameterTwo, C parameterThree, D parameterFour)
	{
		string message = eventName.ToString ();

		if (listeners.ContainsKey(message))
		{
			foreach (Delegate callbackDelegate in listeners[message])
			{
				Callback<A, B, C, D> callback = callbackDelegate as Callback<A, B, C, D>;
				if (callback != null)
				{
					callback(parameterOne, parameterTwo, parameterThree, parameterFour);
				}
			}
			CleanUp(message);
		}
	}

	public static void CleanUp()
	{
		foreach (string message in instance.listeners.Keys)
		{
			CleanUp(message);
		}
	}


	private static void CleanUp(string message)
	{
		foreach (Delegate callback in instance.toRemove[message])
		{
			instance.listeners[message].Remove(callback);
		}
		instance.toRemove[message].Clear();
	}
}




/*
public class EventManager : MonoBehaviour 
{
	private Dictionary <GameEvent, UnityEvent> eventDictionary;

	private static EventManager eventManager;

	public static EventManager instance
	{
		get 
		{
			if (!eventManager) 
			{
				eventManager = FindObjectOfType (typeof(EventManager)) as EventManager;

				if (!eventManager) 
				{
					Debug.LogError ("There needs to be one active EventManager script on a GameObject in your scene.");
				} 
				else 
				{
					eventManager.Init (); 
				}
			}

			return eventManager;
		}
	}

	void Init()
	{
		if (eventDictionary == null)
		{
			eventDictionary = new Dictionary<GameEvent, UnityEvent> ();
		}
	}

	public static void RegisterEvent (GameEvent eventName, UnityAction listener)
	{
		UnityEvent thisEvent = null;

		if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
		{
			thisEvent.AddListener(listener);
		}
		else
		{
			thisEvent = new UnityEvent ();

			thisEvent.AddListener (listener);
			instance.eventDictionary.Add (eventName, thisEvent);
		}
	}

	public static void UnregisterEvent (GameEvent eventName, UnityAction listener)
	{
		if (eventManager == null) 
		{
			return;
		}

		UnityEvent thisEvent = null;

		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) 
		{
			thisEvent.RemoveListener (listener);
		}
	}

	public static void TriggerEvent (GameEvent eventName)
	{
		UnityEvent thisEvent = null;

		if (instance.eventDictionary.TryGetValue (eventName, out thisEvent)) 
		{
			thisEvent.Invoke ();
		}
	}
}
*************/








/*
using UnityEngine;
using UnityEngine.Events;
using System;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
	private static MessageCenter MessageCenter;

	public static void Init()
	{
		MessageCenter = new MessageCenter ();
	}

	public static void RegisterEvent(GameEvent eventName, Callback callback)
	{
		MessageCenter.AddListener (eventName.ToString (), callback);
	}

	public static void RegisterEvent<A>(GameEvent eventName, Callback<A> callback)
	{
		MessageCenter.AddListener (eventName.ToString (), callback);
	}

	public static void UnregisterEvent(GameEvent eventName, Callback callback)
	{
		MessageCenter.RemoveListener (eventName.ToString (), callback);
	}

	public static void UnregisterEvent<A>(GameEvent eventName, Callback<A> callback)
	{
		MessageCenter.RemoveListener (eventName.ToString(), callback);
	}

	public static void TriggerEvent(GameEvent eventName)
	{
		MessageCenter.Broadcast (eventName.ToString ());
	}
}
*/