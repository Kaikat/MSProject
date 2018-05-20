using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorLocationData 
{
	public List<MajorPreference> MajorPreferences { get; set; }
	public string Location;
	public double AverageValue { get; private set; }
	public int Index { get; set; }

	public MajorLocationData(MajorPreference majorPreferences, string location)
	{
		MajorPreferences = new List<MajorPreference> ();
		MajorPreferences.Add(majorPreferences);
		Location = location;
	}

	public void CalculateAverageValue()
	{
		double sum = 0.0;
		foreach (MajorPreference preference in MajorPreferences) 
		{
			sum += preference.Value;
		}

		AverageValue = sum / MajorPreferences.Count;
	}
}
