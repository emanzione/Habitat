//this script is just used for the demo...nothing to see here move along.

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ControlBar : MonoBehaviour {

	//a list of all the UIScript_HP
	public List<UIBarScript> HPScriptList = new List<UIBarScript>();

	void Start()
	{
		foreach (UIBarScript HPS in HPScriptList)
		{
			HPS.UpdateValue(50,100);
		}
	}

	// Update is called once per frame
	void UpdateBar () 
	{
		//get the string in the input boxes
		string StrHP = gameObject.transform.FindChild("HP").gameObject.GetComponent<InputField>().text;
		string StrMaxHP = gameObject.transform.FindChild("MaxHP").gameObject.GetComponent<InputField>().text;

		//convert to int
		int HP = int.Parse(StrHP);
		int MaxHP = int.Parse(StrMaxHP);

		//for every UIScript_HP update it.
		foreach (UIBarScript HPS in HPScriptList)
		{
			HPS.UpdateValue(HP,MaxHP);
		}

	}
}
