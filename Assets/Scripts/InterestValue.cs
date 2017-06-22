using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InterestValue
{
	public Interest Interest;
	public int Value;

	public InterestValue(Interest interest, int value)
	{
		Interest = interest;
		Value = value;
	}
}
