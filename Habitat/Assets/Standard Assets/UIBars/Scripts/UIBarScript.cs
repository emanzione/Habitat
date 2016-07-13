//This is the main controlling script foe the UIBars

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UIBarScript : MonoBehaviour {

	//these are all the variables

	//these are the GameObjects variables
	private GameObject Filler;
	private GameObject Mask;
	private GameObject PercentTxt;
	private GameObject RatioTxt;

	//fill style
	public enum FillStyles{horizontal =0, vertical = 1};
	public FillStyles FillStyle = FillStyles.horizontal;

	//Mask offsets
	private Vector3 Mask0;
	private Vector3 Mask1;
	public float MaskOffset;

	//the HP that is, and the HP it will update to
	private float Value = 0.5f;
	public float NewValue = 0.5f;

	//the gradients
	public Gradient HPColor;
	public Gradient TextColor;

	//speed
	public float Speed = 10f;

	//Text bools
	public bool DisplayPercentTxt;
	public bool DisplayRatioTxt;

	[HideInInspector]//this hides the StartUpdate variable
	public bool StartAnimate = false;

	//Categories
	public enum Categories {increase = 0, decrease =1, NA = 2}
	[HideInInspector]
	public Categories UpdateCategory = Categories.decrease;
	
	//Rule Lists
	public List<CriteriaRule> CriteriaRules = new List<CriteriaRule>();
	public List<UpdateAnimationRule> UpdateAnimationRules = new List<UpdateAnimationRule>();

	//Sets the variables
	void Awake () 
	{
		Mask = gameObject.transform.FindChild("Mask").gameObject;
		Filler = Mask.transform.FindChild("Filler").gameObject;
		PercentTxt = gameObject.transform.FindChild("PercentTxt").gameObject;
		RatioTxt = gameObject.transform.FindChild("RatioTxt").gameObject;

		RectTransform FRT = (Filler.transform as RectTransform);

		//this is the location of the filler object when the HP is at 1
		Mask0 = new Vector3(FRT.position.x,FRT.position.y,FRT.position.z);

		//the location of the filler object when the HP is at 0 depends on the FillStyle
		if (FillStyle == FillStyles.horizontal)
		{
			Mask1 = new Vector3(FRT.position.x + FRT.rect.width,FRT.position.y,FRT.position.z );
		}
		else
		{
			Mask1 = new Vector3(FRT.position.x ,FRT.position.y + FRT.rect.height ,FRT.position.z );
		}
	}

	void Update () 
	{
		//set the Update Category (is the HP going up, down, or not moving)
		if (Mathf.Round(Value * 100f)/100f == Mathf.Round(NewValue * 100f)/100f)
		{
			UpdateCategory = Categories.NA;
		}
		else if (Value > NewValue)
		{
			UpdateCategory = Categories.decrease;
		}
		else if  (Value < NewValue)
		{
			UpdateCategory = Categories.increase;
		}

		//Update the Mask locations (needed if you are going to move stuff arround)
		RectTransform MRT = (Mask.transform as RectTransform);
		if (FillStyle == FillStyles.horizontal)
		{
			Mask1 = new Vector3(MRT.position.x,MRT.position.y,MRT.position.z);
			Mask0 = new Vector3(MRT.position.x - MRT.rect.width + MaskOffset,MRT.position.y,MRT.position.z );
		}
		else
		{
			Mask1 = new Vector3(MRT.position.x,MRT.position.y,MRT.position.z);
			Mask0 = new Vector3(MRT.position.x ,MRT.position.y - MRT.rect.height + MaskOffset,MRT.position.z );
		}

		//move the Current Value to the NewValue
		Value = Mathf.Lerp(Value,NewValue, Speed * Time.deltaTime);
		Value = Mathf.Clamp(Value,0f,1f);//make sure the Value is between 0 and 1

		//move the Filler position to display the Correct Percent
		RectTransform FRT = (Filler.transform as RectTransform);
		FRT.position = Vector3.Lerp (Mask0,Mask1,Value);

		//set the color for the Fill Image, and the Text Objects
		Filler.GetComponent<Image>().color = HPColor.Evaluate(Value);
		PercentTxt.GetComponent<Text>().color = TextColor.Evaluate(Value);
		RatioTxt.GetComponent<Text>().color = TextColor.Evaluate(Value);

		//Execute Each Criteria Rule
		foreach (CriteriaRule CR in CriteriaRules)
		{
			if (CR.isImage())
			{
				CR.DefaultColor = HPColor.Evaluate(Value);
			}
			else
			{
				CR.DefaultColor = TextColor.Evaluate(Value);
			}

			CR.Use(Mathf.Round(Value * 100f)/100f);
		}

		//Execute Each Update Animation Rule
		foreach (UpdateAnimationRule UAR in UpdateAnimationRules)
		{

			if (StartAnimate)
			{
				if (UAR.Category.ToString() == UpdateCategory.ToString() )
				{
					UAR.StartAnimation = true;
				}

			}

			UAR.Use();
		}

		//reset the StartAnimate variable
		StartAnimate = false;

		//activate or inactivate the text objects
		PercentTxt.SetActive(DisplayPercentTxt);
		RatioTxt.SetActive(DisplayRatioTxt);

		//update the PercentTxt 
		PercentTxt.GetComponent<Text>().text = (Mathf.Round(Value * 100f)).ToString() + "%";

	}

	//one way to update the UIBar
	public void UpdateValue(int HP, int MaxHP)
	{
		//this will set the RatioTxt
		RatioTxt.GetComponent<Text>().text = HP.ToString() + "/" + MaxHP.ToString(); 


		//NewValue
		NewValue = (float) HP/MaxHP;

		//trigger the start of the animation
		StartAnimate = true;
	}
	
	//an other way to update the UIBar
	public void UpdateValue(float Percent)
	{
		//do not display ratio
		DisplayRatioTxt = false; 

		//NewValue
		NewValue = Percent;

		//Trigger the start of the animation
		StartAnimate = true;
	}
	

}
