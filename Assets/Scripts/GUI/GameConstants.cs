using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameConstants 
{
	//STEM = 10, Social Science = 4, Humanities = 11
	public static List<Major> STEM = new List<Major> { Major.Architecture, Major.Biology, Major.Chemistry, 
		Major.ComputerScience, Major.EarthSciences, Major.EnvironmentalScience,
		Major.MarineBiology, Major.MarineScience, Major.MediaArtsAndTechnology,
		Major.Physics };
	public static List<Major> SocialSciences = new List<Major> { Major.Anthropology, Major.Counseling, 
		Major.Psychology, Major.Sociology };
	public static List<Major> Humanities = new List<Major> { Major.Art, Major.ArtHistory, Major.Athletics, Major.Communications, 
		Major.Dance, Major.Education, Major.EthnicStudies, Major.History, Major.Literature, 
		Major.Music, Major.Theater };
}
