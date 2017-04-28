using UnityEngine;
using System;
using System.Collections;

public class Animal 
{
	public AnimalSpecies Species { private set; get; }
	public int AnimalID { private set; get; }
	public string Nickname { private set; get; }
	public AnimalStats Stats { private set; get; }
	public string Colorfile { private set; get; }

	public Animal (AnimalSpecies species, int animalID, AnimalStats stats, string colorfile)
	{
		Species = species;
		AnimalID = animalID;
		Stats = stats;
		Colorfile = colorfile;
	}

	public Animal (AnimalSpecies species, int animalID, string nickname, AnimalStats stats, string colorfile)
	{
		Species = species;
		AnimalID = animalID;
		Nickname = nickname;
		Stats = stats;
		Colorfile = colorfile;
	}

	public void SetNickname(string nickname)
	{
		Nickname = nickname;
	}
}