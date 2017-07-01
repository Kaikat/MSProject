using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousScreenData 
{
	public ScreenType Screen;
	public object Data;

	public PreviousScreenData(ScreenType screen, object data)
	{
		Screen = screen;
		Data = data;
	}
}
