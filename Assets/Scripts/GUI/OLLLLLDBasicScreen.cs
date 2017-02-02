using UnityEngine;
using System.Collections;

public abstract class BasicScreen
{
	public ScreenType Type { get; private set; }
	protected BasicScreen(ScreenType type)
	{
		Type = type;
	}

	public abstract void Show();
	public abstract void Destroy();
}
