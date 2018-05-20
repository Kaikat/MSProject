using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Text.RegularExpressions;
using System.IO;
using System.Text;

public class GetMapZenKey : MonoBehaviour {

	public GoMap.GOMap Map;

	void Awake() 
	{
		Map.mapzen_api_key = Keys.MapZenKey;
	}
}
