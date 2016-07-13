using UnityEngine;
using System.Collections;

public class SpecialMushroom : Food
{
	public override void Start()
	{
		base.Start ();
		/*Calories = 30;
		Mass = 4;*/
	}

	public override void Consume (Player agent)
	{
		if (agent.CanEat (this)) {
			agent.Eat(this, TotalMass, TotalCalories);
			agent.dnaPoints += 10;
			agent.guiManager.SetDNA (agent.dnaPoints);
			this.gameObject.SetActive (false);
			UserSession.EatenFood++;
			UserSession.EatenSpecialMushroom++;
		}
	}
}
