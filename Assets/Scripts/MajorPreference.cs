using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MajorPreference 
{
	public string Major;
	public double Value;

	public MajorPreference()
	{
	}

	public MajorPreference(string major, double value)
	{
		Major = major;
		Value = value;
	}
}
