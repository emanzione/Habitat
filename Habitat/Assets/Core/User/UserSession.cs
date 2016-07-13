using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;

public class UserSession 
{
	public static string Username = "Guest";
	public static bool IsNewGame = true;

	public static uint EatenFood = 0;
	public static uint EatenGrass = 0;
	public static uint EatenMushroom = 0;
	public static uint EatenPoisonedMushroom = 0;
	public static uint EatenEnemies = 0;
	public static uint EatenSpecialMushroom = 0;


	public static string[] GetSavedGames()
	{
		return Directory.GetDirectories(WorldStorage.SavingLocation, "*", SearchOption.AllDirectories);
	}

	public static void StartNewSession(string user)
	{
		Username = user;
		IsNewGame = true;
		EatenFood = 0;
		EatenEnemies = 0;
		EatenGrass = 0;
		EatenMushroom = 0;
		EatenPoisonedMushroom = 0;
		EatenSpecialMushroom = 0;
		SceneManager.LoadScene(1);
	}
}
