using UnityEngine;
using System.Collections;

public class Mushroom : Food
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
			this.gameObject.SetActive (false);
			UserSession.EatenFood++;
			UserSession.EatenMushroom++;
		}
	}
}
