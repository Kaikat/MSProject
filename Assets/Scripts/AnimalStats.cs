using UnityEngine;
using System.Collections;

public class AnimalStats {

	public float Health1 { private set; get; }
	public float Health2 { private set; get; }
	public float Health3 { private set; get; }
	public float Age { private set; get; }
	public float Size { private set; get; }
	public float Weight { private set; get; }

	public AnimalStats(float health1, float health2, float health3, float age, float size, float weight)
	{
		Health1 = health1;
		Health2 = health2;
		Health3 = health3;
		Age = age;
		Size = size;
		Weight = weight;
	}
}
