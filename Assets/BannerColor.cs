using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerColor : MonoBehaviour {

	public GameObject Banner;
	public AnimalSpecies Animal;

	void Awake()
	{
		EventManager.RegisterEvent <Animal> (GameEvent.AnimalCaught, SetBannerColorToGray);
	}

	public void SetBannerData(Texture bannerColor, AnimalSpecies animal)
	{
		Banner.GetComponent<MeshRenderer> ().material.mainTexture = bannerColor;
		Animal = animal;
	}

	public void SetBannerColorToGray(Animal animal)
	{
		if (Animal == animal.Species)
		{
			Banner.GetComponent<MeshRenderer> ().material.mainTexture = Resources.Load<Texture> ("gray");
		}
	}

	void Destroy()
	{
		EventManager.UnregisterEvent <Animal> (GameEvent.AnimalCaught, SetBannerColorToGray);
	}
}
