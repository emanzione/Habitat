using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour 
{
	public float Calories = 1;
	public float Mass = 1;

	public virtual void Start()
	{
	}

	public float TotalMass
	{
		get {
			return  Mass;
		}
	}

	public float TotalCalories 
	{
		get
		{
			return Calories * TotalMass;
		}
	}
	
	public Vector3 AdjustPosition(Vector3 pos)
	{
		//pos.y += (Settings.GRASS_SIZE_Y / 2);
		return pos;
	}

	public virtual void Consume(Player agent)
	{
		if (agent.CanEat (this)) {
			agent.Eat(this, TotalMass, TotalCalories);
			this.gameObject.SetActive (false);
			UserSession.EatenFood++;
		}
	}
}
