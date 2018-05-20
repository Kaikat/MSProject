using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public static class DictionaryUtility {

	static DictionaryUtility()
	{
	}

	public static T ToEnum<T>(this string enumString) where T : struct, IConvertible
	{
		return (T)Enum.Parse (typeof(T), System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(enumString));
	}

	public static T ToExactEnum<T>(this string enumString) where T : struct, IConvertible
	{
		return (T)Enum.Parse(typeof(T), enumString);
	}

	public static GameVersion GetGameVersion(this string username)
	{
		if (username [1] != '-')
		{
			return GameVersion.Default;
		}

		switch (username [0])
		{
			case '2':
				return GameVersion.TrackVisits;
			case '3':
				return GameVersion.ColorCodedMajors;
			default:
				return GameVersion.Default;
		}
	}
}
