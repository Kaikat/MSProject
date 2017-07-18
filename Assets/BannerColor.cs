using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BannerColor : MonoBehaviour {

	public GameObject Banner;

	public void SetBannerColor(Texture bannerColor)
	{
		Banner.GetComponent<MeshRenderer> ().material.mainTexture = bannerColor;
	}
}
