using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPlayerSelection : MonoBehaviour {

	public Toggle Likert1;
	public Toggle Likert2;
	public Toggle Likert3;
	public Toggle Likert4;
	public Toggle Likert5;
	public Toggle Likert6;
	public Toggle Likert7;

	public int LikertSelection()
	{
		if (Likert1.isOn) 
		{
			return 1;
		} 
		else if (Likert2.isOn) 
		{
			return 2;
		} 
		else if (Likert3.isOn) 
		{
			return 3;
		} 
		else if (Likert4.isOn) 
		{
			return 4;
		} 
		else if (Likert5.isOn) 
		{
			return 5;
		} 
		else if (Likert6.isOn) 
		{
			return 6;
		} 
		else if (Likert7.isOn) 
		{
			return 7;
		}
		else
		{
			return -1;
		}	
	}
}
