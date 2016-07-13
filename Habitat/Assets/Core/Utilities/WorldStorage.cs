using UnityEngine;
using System.IO;
using System.Xml.Serialization;
using System;

public class WorldStorage
{
	public static readonly string SavingLocation = Path.Combine(Application.persistentDataPath, "Saves");
	public static readonly string FileName = "data.json";

	public static void SaveToFile<T>(T dataToStore)
	{
		string fileName = Path.Combine(Path.Combine(SavingLocation, UserSession.Username), FileName);

		try
		{
			string serializedData = JsonUtility.ToJson(dataToStore, true);
			File.WriteAllText(fileName, serializedData);
		}
		catch(Exception ex)
		{
			Debug.LogWarning("File writing error: " + ex.Message);
		}
	}

	public static T LoadFromFIle<T>()
	{
		T storedData = default(T);

		string fileName = Path.Combine(Path.Combine(SavingLocation, UserSession.Username), FileName);

		try
		{
			string serializedData = File.ReadAllText(fileName);
			storedData = JsonUtility.FromJson<T>(serializedData);
		}
		catch (Exception ex)
		{
			Debug.LogWarning("File reading error: " + ex.Message);
		}

		return storedData;
	}

	public static void RemoveData()
	{
		string fileName = Path.Combine(Path.Combine(SavingLocation, UserSession.Username), FileName);

		try
		{
			File.Delete(fileName);
		}
		catch (System.Exception ex)
		{
			Debug.LogWarning(fileName + " deleting error: " + ex.Message);
		}        
	}
}