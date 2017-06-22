using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorLocation 
{
	public MajorPreference MajorPreference;
	public string Location;

	public MajorLocation()
	{
	}

	public MajorLocation(MajorPreference majorPreference, string location)
	{
		MajorPreference = majorPreference;
		Location = location;
	}
}
