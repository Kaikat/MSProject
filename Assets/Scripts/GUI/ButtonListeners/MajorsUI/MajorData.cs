using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MajorData 
{
	public string Name { private set; get; }
	public string Description { private set; get; }

	public MajorData(string name, string description)
	{
		Name = name;
		Description = description;
	}
}
