using System.Collections;
using System.Collections.Generic;
using System.IO;  
using System.Text;
using UnityEngine;
using System;

public static class TextFile 
{
	public static string Read(string filename)
	{
		string path = Application.persistentDataPath + "/" + filename;
		return System.IO.File.Exists (path) ? System.IO.File.ReadAllText (path) : string.Empty;
	}

	public static void Write(string filename, string contents)
	{
		string path = Application.persistentDataPath + "/" + filename;
		System.IO.File.WriteAllText (path, contents);
	}
}