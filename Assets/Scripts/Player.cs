using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class Player {

	private string Username;
	private string Name;
	private int Currency;
	private int ReleasedNum;
	private int NursingNum;
	private int SeenNum;

	private Dictionary<AnimalSpecies, List<Animal>> Animals;

	GameObject tigerAsset;

	public Player ()
	{
		Animals = new Dictionary<AnimalSpecies, List<Animal>> ();
		PopulateAnimalList ();

		EventManager.RegisterEvent(GameEvent.Spawn, SpawnFunction);
		EventManager.RegisterEvent (GameEvent.Destroy, DestroyFunction);
		EventManager.RegisterEvent (GameEvent.Test, TestFunction);
		EventManager.RegisterEvent (GameEvent.Junk, JunkFunction);

		ReleasedNum = 0;
		NursingNum = 0;
		SeenNum = 0;
		Name = RealService.Name;
	}
		
	public void Destroy()
	{
		EventManager.UnregisterEvent (GameEvent.Spawn, SpawnFunction);
		EventManager.UnregisterEvent (GameEvent.Destroy, DestroyFunction);
	}

	//add get functions for UI
	public string GetName() {
		return Name;
	}

	public int GetReleased() {
		return ReleasedNum;
	}

	public void SetReleased(int num) {
		ReleasedNum += num;
	}

	public int GetNursing() {
		return NursingNum;
	}

	public void SetNursing(int num) {
		NursingNum += num;
	}

	public int GetSeen() {
		return SeenNum;
	}

	public void SetSeen(int num) {
		SeenNum += num;
	}

	//To get current animal according to speicies
	public Dictionary<AnimalSpecies, List<Animal>> GetAnimals() {
		return Animals;
	}
	
	private void PopulateAnimalList()
	{
		List<Animal> playerAnimals = Service.Request.GetPlayerAnimals ();
		for (int i = 0; i < playerAnimals.Count; i++) 
		{
			if (Animals.ContainsKey (playerAnimals [i].Species)) 
			{
				Animals [playerAnimals [i].Species].Add (playerAnimals[i]);
			} 
			else 
			{
				Animals.Add (playerAnimals [i].Species, new List<Animal>());
				Animals[playerAnimals[i].Species].Add(playerAnimals [i]);
			}
		}
	}


	private void SpawnFunction()
	{
		Debug.Log ("Spawning");
		tigerAsset = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (AnimalSpecies.Tiger));


		//tigerAsset = (GameObject)GameObject.Instantiate (AssetManager.GetAnimalPrefab (AnimalSpecies.Tiger), new Vector3(0.0f, 0.0f, -5.0f), Quaternion.identity);

		//Animal animal = new Animal ("tiger1", AnimalSpecies.Tiger, "Tigecito", AnimalEncounterType.Caught, HabitatLevelType.Middle);

		//tigerAsset = (GameObject)GameObject.Instantiate(Resources.Load("AnimalPrefabs/Tiger"));//, new Vector3 (0f, 0f, 0f), Quaternion.identity);
	}

	private void DestroyFunction()
	{
		Debug.Log ("Destroying");
		GameObject.Destroy (tigerAsset);
	}

	private void TestFunction()
	{
		Debug.Log ("Testing!");
	}

	private void JunkFunction()
	{
		Debug.Log ("JUNK");
	}
}
