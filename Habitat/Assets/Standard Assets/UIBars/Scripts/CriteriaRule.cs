//This Script is for the criteria rules 

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class CriteriaRule {

	//these are the variables

	public GameObject Object;

	[HideInInspector]
	public int Type = 0;
	
	public bool ChangeSize;
	[HideInInspector]
	public Vector3 DefaultSize;
	public Vector3 AltSize;
	
	public bool ChangeColor;
	[HideInInspector]
	public Color DefaultColor;
	public Color AltColor;
	
	public float Speed;

	public float Min;
	public float Max;

	private bool DefaultSizeSet = false;

	//this allows me to make sure we are using the right gradient colors (see the UIBarScript)
	public bool isImage()
	{
		if (Object.GetComponent<Image>() != null)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	//this will update the object if the value of the UIBar is between the Min and Max Values
	public void Use(float CurrentValue)
	{
		//make sure we have the dafault values to go back.
		if (DefaultSizeSet == false)
		{
			DefaultSize = Object.transform.localScale;
			DefaultSizeSet = true;
		}

		//make sure the value meets the criteria
		if (CurrentValue >= Min
		    && CurrentValue <= Max)
		{
			if (Object.GetComponent<Image>() != null)
			{
				//change the size
				if (ChangeSize)
				{
					Object.transform.localScale = Vector3.Lerp(DefaultSize,AltSize,Mathf.Sin (Time.time * Speed));
				}

				//change the color
				if (ChangeColor)
				{
					Object.GetComponent<Image>().color = Color.Lerp(DefaultColor,AltColor,Mathf.Sin (Time.time * Speed) );
				}
			}
			else if (Object.GetComponent<Text>() != null )
			{
				//change the size
				if (ChangeSize)
				{
					Object.transform.localScale = Vector3.Lerp(DefaultSize,AltSize,Mathf.Sin (Time.time * Speed) );
				}

				//change the color
				if (ChangeColor)
				{
					Object.GetComponent<Text>().color = Color.Lerp(DefaultColor,AltColor,Mathf.Sin (Time.time  * Speed) );
				}
			}
		}
		else
		{

		}
		
	}

}
