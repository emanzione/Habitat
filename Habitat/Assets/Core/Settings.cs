using System.Collections;
using UnityEngine;

public class Settings : MonoBehaviour
{
	public static float CHUNK_SIZE_X = 10f;
	public static float CHUNK_SIZE_Y = 1f;
	public static float CHUNK_SIZE_Z = 10f;

	public static ushort GRASS_MAX_AMOUNT_PER_CHUNK = 120;
	public static ushort GRASS_MIN_AMOUNT_PER_CHUNK = 40;

	public static ushort BIG_GRASS_MAX_AMOUNT_PER_CHUNK = 45;
	public static ushort BIG_GRASS_MIN_AMOUNT_PER_CHUNK = 15;

	public static ushort MUSHROOM_MAX_AMOUNT_PER_CHUNK = 40;
	public static ushort MUSHROOM_MIN_AMOUNT_PER_CHUNK = 0;

	public static ushort TOXIC_MUSHROOM_MAX_AMOUNT_PER_CHUNK = 40;
	public static ushort TOXIC_MUSHROOM_MIN_AMOUNT_PER_CHUNK = 0;

	public static ushort SPECIAL_MUSHROOM_MAX_AMOUNT_PER_CHUNK = 1;
	public static ushort SPECIAL_MUSHROOM_MIN_AMOUNT_PER_CHUNK = 0;

	public static ushort SPECIAL_TOXIC_MUSHROOM_MAX_AMOUNT_PER_CHUNK = 8;
	public static ushort SPECIAL_TOXIC_MUSHROOM_MIN_AMOUNT_PER_CHUNK = 0;

	public static ushort TREE_MAX_AMOUNT_PER_CHUNK = 15;
	public static ushort TREE_MIN_AMOUNT_PER_CHUNK = 1;

	public static ushort STONE_MAX_AMOUNT_PER_CHUNK = 18;
	public static ushort STONE_MIN_AMOUNT_PER_CHUNK = 2;

	public static ushort ENEMY_MAX_AMOUNT_PER_CHUNK = 8;
	public static ushort ENEMY_MIN_AMOUNT_PER_CHUNK = 0;

	public static float MIN_CALORIES_NEEDED_FOR_POO = 620f;
	public static float MAX_CALORIES_NEEDED_FOR_POO = 920f;

	public static float MIN_MOVEMENTS_DELAY = 4f;
	public static float MAX_MOVEMENTS_DELAY = 11f;

	public static int DNA_POINTS_TO_WIN = (int)Randomizer.Range(35f, 110f);

	public static string GetGrassPrefabName()
	{
		return "Environment/Bush_0" + ((int)Randomizer.Range(1, 7)).ToString();
	}

	public static string GetGiantGrassPrefabName()
	{
		return "Environment/GiantBush_0" + ((int)Randomizer.Range(1, 3)).ToString();
	}

	public static string GetStonePrefabName()
	{
		return "Environment/Rock_0" + ((int)Randomizer.Range(1, 3)).ToString();
	}

	public static string GetTreePrefabName()
	{
		return "Environment/Tree_0" + ((int)Randomizer.Range(1, 5)).ToString();
	}

	public static string GetRockPrefabName()
	{
		return "Environment/Rock_0" + ((int)Randomizer.Range(1, 3)).ToString();
	}

	public static string GetMushroomPrefabName()
	{
		return "Environment/Mushroom_0" + ((int)Randomizer.Range(1, 4)).ToString();
	}

	public static string GetToxicMushroomPrefabName()
	{
		return "Environment/ToxicMushroom_0" + ((int)Randomizer.Range(1, 4)).ToString();
	}

	public static string GetSpecialMushroomPrefabName()
	{
		return "Environment/SpecialMushroom_0" + ((int)Randomizer.Range(1, 1)).ToString();
	}

	public static string GetSpecialToxicMushroomPrefabName()
	{
		return "Environment/SpecialToxicMushroom_0" + ((int)Randomizer.Range(1, 1)).ToString();
	}

	public static string GetChunkPrefabName()
	{
		return "Environment/Chunk";
	}

	public static string GetPoopPrefabName()
	{
		return "Environment/poop";
	}

	public static string GetPlayerPrefabName()
	{
		return "Player";
	}

	public static string GetMainCameraPrefabName()
	{
		return "MainCamera";
	}

	public static string GetEnemyPrefabName()
	{
		return "Environment/Enemy_" + ((int)Randomizer.Range(1, 4)).ToString();
	}
}
