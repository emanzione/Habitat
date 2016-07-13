using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System;

public class Chunk : MonoBehaviour 
{
	/* This is the chunk shape
	 * C ----- D
	 * |       |
	 * |       |
	 * A ----- B 
	 */
	public Vector3 A;
	public Vector3 B;
	public Vector3 C;
	public Vector3 D;

	List<GameObject> grassObjects;
	List<GameObject> mushroomObjects;
	List<GameObject> treeObjects;
	List<GameObject> toxicMushroomObjects;
	List<GameObject> stoneObjects;
	List<GameObject> enemyObjects;
	List<GameObject> specialMushroomObjects;
	List<GameObject> specialToxicMushroomObjects;
	List<GameObject> giantGrassObjects;

	ushort TreeAmount;
	ushort MushroomAmount;
	ushort ToxicMushroomAmount;
	ushort GrassAmount;
	ushort GiantGrassAmount;
	ushort StoneAmount;
	ushort EnemyAmount;
	ushort SpecialMushroomAmount;
	ushort SpecialToxicMushroomAmount;

	public ChunkGridPosition gridPosition;
	public bool IsPlayerContained = false;

	public WorldManager worldManager;

	void Awake()
	{
		SetVertex ();

		grassObjects = new List<GameObject>();
		giantGrassObjects = new List<GameObject>();
		mushroomObjects = new List<GameObject>();
		treeObjects = new List<GameObject>();
		toxicMushroomObjects = new List<GameObject>();
		stoneObjects = new List<GameObject>();
		enemyObjects = new List<GameObject> ();
		specialToxicMushroomObjects = new List<GameObject>();
		specialMushroomObjects = new List<GameObject>();

		TreeAmount = (ushort)Randomizer.Range(Settings.TREE_MIN_AMOUNT_PER_CHUNK, Settings.TREE_MAX_AMOUNT_PER_CHUNK);
		GrassAmount = (ushort)Randomizer.Range(Settings.GRASS_MIN_AMOUNT_PER_CHUNK, Settings.GRASS_MAX_AMOUNT_PER_CHUNK);
		GiantGrassAmount = (ushort)Randomizer.Range(Settings.BIG_GRASS_MIN_AMOUNT_PER_CHUNK, Settings.BIG_GRASS_MAX_AMOUNT_PER_CHUNK);
		MushroomAmount = (ushort)Randomizer.Range(Settings.MUSHROOM_MIN_AMOUNT_PER_CHUNK, Settings.MUSHROOM_MAX_AMOUNT_PER_CHUNK);
		ToxicMushroomAmount = (ushort)Randomizer.Range(Settings.TOXIC_MUSHROOM_MIN_AMOUNT_PER_CHUNK, Settings.TOXIC_MUSHROOM_MAX_AMOUNT_PER_CHUNK);
		StoneAmount = (ushort)Randomizer.Range(Settings.STONE_MIN_AMOUNT_PER_CHUNK, Settings.STONE_MAX_AMOUNT_PER_CHUNK);
		EnemyAmount = (ushort)Randomizer.Range(Settings.ENEMY_MIN_AMOUNT_PER_CHUNK, Settings.ENEMY_MAX_AMOUNT_PER_CHUNK);
		SpecialMushroomAmount = (ushort)Randomizer.Range(Settings.SPECIAL_MUSHROOM_MIN_AMOUNT_PER_CHUNK, Settings.SPECIAL_MUSHROOM_MAX_AMOUNT_PER_CHUNK + 0.1f);
		SpecialToxicMushroomAmount = (ushort)Randomizer.Range(Settings.SPECIAL_TOXIC_MUSHROOM_MIN_AMOUNT_PER_CHUNK, Settings.SPECIAL_TOXIC_MUSHROOM_MAX_AMOUNT_PER_CHUNK);

		gridPosition = new ChunkGridPosition (0, 0);
	}

	void Update() 
	{
		if(grassObjects.Count < GrassAmount)
		{
			GameObject grass = (GameObject)GameObject.Instantiate(Resources.Load (Settings.GetGrassPrefabName()));
			grass.transform.position = this.GetRandomPosition(); //grass.GetComponent<Grass>().AdjustPosition(this.GetRandomPosition());
			grass.transform.rotation = this.GetRandomRotation();
			grass.transform.SetParent(transform);
			grassObjects.Add(grass);
		}

		if(giantGrassObjects.Count < GiantGrassAmount)
		{
			GameObject grass = (GameObject)GameObject.Instantiate(Resources.Load (Settings.GetGiantGrassPrefabName()));
			grass.transform.position = this.GetRandomPosition(); //grass.GetComponent<Grass>().AdjustPosition(this.GetRandomPosition());
			grass.transform.rotation = this.GetRandomRotation();
			grass.transform.SetParent(transform);
			giantGrassObjects.Add(grass);
		}

		if(mushroomObjects.Count < MushroomAmount)
		{
			GameObject mushroom = (GameObject)GameObject.Instantiate(Resources.Load (Settings.GetMushroomPrefabName()));
			mushroom.transform.position = this.GetRandomPosition (); //mushroom.GetComponent<Mushroom>().AdjustPosition(this.GetRandomPosition());
			mushroom.transform.rotation = this.GetRandomRotation();
			mushroom.transform.SetParent(transform);
			mushroomObjects.Add(mushroom);
		}

		if(specialMushroomObjects.Count < SpecialMushroomAmount)
		{
			GameObject mushroom = (GameObject)GameObject.Instantiate(Resources.Load (Settings.GetSpecialMushroomPrefabName()));
			mushroom.transform.position = this.GetRandomPosition (); //mushroom.GetComponent<Mushroom>().AdjustPosition(this.GetRandomPosition());
			mushroom.transform.rotation = this.GetRandomRotation();
			mushroom.transform.SetParent(transform);
			specialMushroomObjects.Add(mushroom);
		}

		if (this.gridPosition.X != 0 || this.gridPosition.Y != 0) {
			if (treeObjects.Count < TreeAmount) {
				GameObject tree = (GameObject)GameObject.Instantiate (Resources.Load (Settings.GetTreePrefabName ()));
				tree.transform.position = this.GetRandomPosition (); //tree.GetComponent<Tree>().AdjustPosition(this.GetRandomPosition());
				tree.transform.rotation = this.GetRandomRotation ();
				tree.transform.SetParent (transform);
				treeObjects.Add (tree);
			}
		}

		if(toxicMushroomObjects.Count < ToxicMushroomAmount)
		{
			GameObject mushroom = (GameObject)GameObject.Instantiate(Resources.Load (Settings.GetToxicMushroomPrefabName()));
			mushroom.transform.position = this.GetRandomPosition (); //mushroom.GetComponent<Mushroom>().AdjustPosition(this.GetRandomPosition());
			mushroom.transform.rotation = this.GetRandomRotation();
			mushroom.transform.SetParent(transform);
			toxicMushroomObjects.Add(mushroom);
		}

		if(specialToxicMushroomObjects.Count < SpecialToxicMushroomAmount)
		{
			GameObject mushroom = (GameObject)GameObject.Instantiate(Resources.Load (Settings.GetSpecialToxicMushroomPrefabName()));
			mushroom.transform.position = this.GetRandomPosition (); //mushroom.GetComponent<Mushroom>().AdjustPosition(this.GetRandomPosition());
			mushroom.transform.rotation = this.GetRandomRotation();
			mushroom.transform.SetParent(transform);
			specialToxicMushroomObjects.Add(mushroom);
		}

		if (this.gridPosition.X != 0 || this.gridPosition.Y != 0) {
			if (stoneObjects.Count < StoneAmount) {
				GameObject stone = (GameObject)GameObject.Instantiate (Resources.Load (Settings.GetStonePrefabName ()));
				stone.transform.position = this.GetRandomPosition (); //tree.GetComponent<Tree>().AdjustPosition(this.GetRandomPosition());
				stone.transform.rotation = this.GetRandomRotation ();
				stone.transform.SetParent (transform);
				stoneObjects.Add (stone);
			}
		}

		if (this.gridPosition.X != 0 || this.gridPosition.Y != 0) {
			if (enemyObjects.Count < EnemyAmount) {
				GameObject enemy = (GameObject)GameObject.Instantiate (Resources.Load (Settings.GetEnemyPrefabName ()));
				enemy.transform.position = this.GetRandomPosition (); //tree.GetComponent<Tree>().AdjustPosition(this.GetRandomPosition());
				enemy.transform.rotation = this.GetRandomRotation ();
				enemy.transform.localScale = this.GetRandomScaleBasedOnPlayer (50f);
				enemy.GetComponent<Enemy> ().player = this.worldManager.player;
				//enemy.transform.SetParent (transform);
				enemyObjects.Add (enemy);
			}
		}
	}

	protected Vector3 GetRandomPosition()
	{
		Vector3 pos = new Vector3(
			(float)Randomizer.Range(A.x, B.x),
			A.y,
			(float)Randomizer.Range(A.z, C.z)
		);
		return pos;
	}

	protected Quaternion GetRandomRotation()
	{
		return Quaternion.Euler(0, (float)Randomizer.Range(-180, 180), 0);
	}

	protected Vector3 GetRandomScaleBasedOnPlayer(float percentage)
	{
		float randomScale = (float)Randomizer.Range (worldManager.player.transform.localScale.x - ((percentage * worldManager.player.transform.localScale.x) / 100f), worldManager.player.transform.localScale.x + ((percentage * worldManager.player.transform.localScale.x) / 100f));
		return new Vector3 (randomScale, randomScale, randomScale);
	}

	public void SetGridPosition(int x, int y)
	{
		gridPosition.X = x;
		gridPosition.Y = y;
		transform.position = new Vector3 (
			gridPosition.X * Settings.CHUNK_SIZE_X, 
			0,
			gridPosition.Y * Settings.CHUNK_SIZE_Z
		);

		SetVertex ();
	}

	private void SetVertex()
	{
		Vector3 currentPosition = transform.position;
		A = new Vector3(
			(currentPosition.x - (Settings.CHUNK_SIZE_X / 2)),
			(currentPosition.y + (Settings.CHUNK_SIZE_Y / 2)),
			(currentPosition.z - (Settings.CHUNK_SIZE_Z / 2))
		);
		B = new Vector3(
			(currentPosition.x + (Settings.CHUNK_SIZE_X / 2)),
			(currentPosition.y + (Settings.CHUNK_SIZE_Y / 2)),
			(currentPosition.z - (Settings.CHUNK_SIZE_Z / 2))
		);
		C = new Vector3(
			(currentPosition.x - (Settings.CHUNK_SIZE_X / 2)),
			(currentPosition.y + (Settings.CHUNK_SIZE_Y / 2)),
			(currentPosition.z + (Settings.CHUNK_SIZE_Z / 2))
		);
		D = new Vector3(
			(currentPosition.x + (Settings.CHUNK_SIZE_X / 2)),
			(currentPosition.y + (Settings.CHUNK_SIZE_Y / 2)),
			(currentPosition.z + (Settings.CHUNK_SIZE_Z / 2))
		);
	}

}
