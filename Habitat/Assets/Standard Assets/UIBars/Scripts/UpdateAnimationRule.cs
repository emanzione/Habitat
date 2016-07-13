//This Script is for the Update Animation Rules

using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class UpdateAnimationRule  {

	//these are the variables

	public GameObject Object;

	public enum Categories {increase = 0, decrease =1}
	public Categories Category = Categories.decrease;

	[HideInInspector]
	public int Type = 0;

	public bool ChangeSize;
	private Vector3 DefaultSize;
	public Vector3 AltSize;

	public bool ChangeColor;
	private Color DefaultColor;
	public Color AltColor;

	[HideInInspector]
	public bool StartAnimation;
	private float AnimateValue = 0f;

	public float Speed;

	// set the default size for later use
	public void Start()
	{
		DefaultSize = Object.transform.localScale;
	}

	//this will animate the object
	public void Use()
	{

		if (StartAnimation)
		{
			if (Object.GetComponent<Image>() != null)
			{
				//change the size
				if (ChangeSize)
				{
					Object.transform.localScale = Vector3.Lerp (DefaultSize,AltSize,Mathf.Sin(AnimateValue));
				}

				//change the color
				if (ChangeColor)
				{
					Object.GetComponent<Image>().color = Color.Lerp (DefaultColor,AltColor,Mathf.Sin(AnimateValue));
				}
			}

			if(Object.GetComponent<Text>() != null)
			{
				//change the size
				if (ChangeSize)
				{
					Object.transform.localScale = Vector3.Lerp (DefaultSize,AltSize,Mathf.Sin(AnimateValue));
				}

				//change the color
				if (ChangeColor)
				{
					Object.GetComponent<Text>().color = Color.Lerp (DefaultColor,AltColor,Mathf.Sin(AnimateValue));
				}
			}


			//stop the animation if the AnimateValue >= Pi
			if (AnimateValue >= Mathf.PI)
			{
				StartAnimation = false;
				AnimateValue = 0f;
			}
			else
			{
				//increase the AnimateValue
				AnimateValue =  AnimateValue + (Time.deltaTime * Speed);
			}
		}
		else
		{
			//if we don't animate set the dafault size and color
			if (Object.GetComponent<Image>() != null)
			{
				DefaultSize = Object.transform.localScale;
				DefaultColor = Object.GetComponent<Image>().color;
			}

			//if we don't animate set the dafault size and color
			if(Object.GetComponent<Text>() != null)
			{
				DefaultSize = Object.transform.localScale;
				DefaultColor = Object.GetComponent<Text>().color;
			}
		}

	}


}
