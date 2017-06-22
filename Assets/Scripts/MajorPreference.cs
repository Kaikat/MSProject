using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorPreference 
{
	public Major Major;
	public double Value;

	public MajorPreference()
	{
	}

	public MajorPreference(Major major, double value)
	{
		Major = major;
		Value = value;
	}
}
