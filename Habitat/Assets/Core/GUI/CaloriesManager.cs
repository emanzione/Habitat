using System;
using UnityEngine;
using UnityEngine.UI;

public class CaloriesManager : MonoBehaviour
{
	public Text calories;

	public void SetCalories(int calories)
	{
		this.calories.text = calories + "kcal";
	}
}