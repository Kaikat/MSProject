// Written by Samuel Dong
// https://github.com/AdmiralSam

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PolylineDecoder 
{
	public static List<Vector2> ExtractPolyLine(string encodedLine)
	{
		var polyLine = new List<Vector2>();
		float x = 0.0f;
		float y = 0.0f;
		while(encodedLine.Length > 0)
		{
			var point = new Vector2();
			x += ExtractNumber(encodedLine, out encodedLine);
			y += ExtractNumber(encodedLine, out encodedLine);
			point.x = x;
			point.y = y;
			polyLine.Add(point);
		}
		return polyLine;
	}

	private static float ExtractNumber(string encodedLine, out string remainder)
	{
		for (int i = 0; i < encodedLine.Length; i++)
		{
			char character = encodedLine[i];
			int bitChunk = character - 63;
			if ((bitChunk & 0x20) == 0)
			{
				remainder = encodedLine.Substring(i + 1);
				return ParseEncoding(encodedLine.Substring(0, i + 1));
			}
		}

		remainder = "error";
		return 0.0f; //this is an error
		//throw new FormatException("Encoded line is incorrectly formatted");
	}

	private static float ParseEncoding(string encodedNumber)
	{
		int[] bitChunks = encodedNumber.Select(character => character - 63).ToArray();
		for (int i = 0; i < bitChunks.Length - 1; i++)
		{
			bitChunks[i] = bitChunks[i] & (~0x20);
		}
		int roundedResult = 0;
		int shift = 0;
		foreach (int bitChunk in bitChunks)
		{
			roundedResult |= bitChunk << shift;
			shift += 5;
		}
		if ((roundedResult & 0x1) == 1)
		{
			roundedResult = ~roundedResult;
		}
		roundedResult >>= 1;
		return roundedResult / 1000000.0f;
	}
}