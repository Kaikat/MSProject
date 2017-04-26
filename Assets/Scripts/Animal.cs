using UnityEngine;
using System;
using System.Collections;

public class Animal : BasicAnimal {

	private string Nickname;
	private DateTime CaughtDate;
	private DateTime ReleaseDate;
	private AnimalStats InitialStats;
	private AnimalStats CurrentStats;
	private AnimalEncounterType EncounterType;

	public Animal (string ID, AnimalSpecies species, string nickname, AnimalEncounterType encounter, HabitatLevelType habitatType) : base (species, habitatType)
	{
		Nickname = nickname;
		EncounterType = encounter;
	}
}

/*
AnimalSpecies
Nickname
Health
Size
Age
Colorfile
*/