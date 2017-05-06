using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coordinate 
{
	public double X { private set; get; }
	public double Y { private set; get; }

	public Coordinate(double x, double y)
	{
		X = x;
		Y = y;
	}
}
