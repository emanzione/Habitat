using UnityEngine;
using System.Collections;

public class PoisonMushroom : Food
{
	public override void Start()
	{
		base.Start ();
		/*Calories = 10;
		Mass = 3;*/
	}

	public override void Consume (Player agent)
	{
		if (agent.CanEat (this)) {
			agent.Eat(this, TotalMass, -TotalCalories);
			this.gameObject.SetActive (false);
			UserSession.EatenFood++;
			UserSession.EatenPoisonedMushroom++;
		}
	}
}
