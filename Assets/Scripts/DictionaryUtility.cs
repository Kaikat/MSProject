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
}
