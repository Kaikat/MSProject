using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace JsonResponse  
{
	[System.Serializable]
	public class BasicResponse //JsonResponse
	{
		public string id;
		public string message;
		public bool error;
	}

	[System.Serializable]
	public class PlayerDataResponse
	{
		public string name;
		public string avatar;
		public int currency;
	}

	[System.Serializable]
	public class EchoResponse
	{
		public string[] ResultingData;
	}

	[System.Serializable]
	public class ListResponse //DictResponse
	{
		public List<DataAnimal> AnimalData;
	}

	[System.Serializable]
	public class DataAnimal
	{
		public string animal_id;
		public string description;
		public string habitat_level;
		public string sensor_id;
		public float min_size;
		public float max_size;
		public float min_age;
		public float max_age;
		public string colorkey_map_file;
	}

	[System.Serializable]
	public class OwnedAnimalResponse
	{		
		public bool empty;
		public List<OwnedAnimalData> OwnedAnimalData;
	}

	[System.Serializable]
	public class OwnedAnimalData
	{
		public string animal_id;
		public string nickname;
		public string health;
		public float size;
		public float age;
		public string color_file;
	}
}
