using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest 
{
	public int LocationID { private set; get; }
	public string LocationName { private set; get; }
	public string Description { private set; get; }
	public Vector2 Coordinate { private set; get; }

	public PointOfInterest(int location_id, string location_name, string description, double x, double y)
	{
		LocationID = location_id;
		LocationName = location_name;
		Description = description;
		Coordinate = new Vector2((float) x, (float) y);
	}
}
