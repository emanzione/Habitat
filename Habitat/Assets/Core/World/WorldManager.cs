using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityThreading;
using UnityStandardAssets.Utility;

public class WorldManager : MonoBehaviour 
{
	Dictionary<string, Chunk> chunks;

	public Player player;
	public GameObject cameraObject;
	public GUIManager guiManager;
	public AudioManager audioManager;

	void Awake()
	{
		chunks = new Dictionary<string, Chunk>();
		//chunkInstancerEvent = new AutoResetEvent (false);

		/*Thread thread = new Thread (new ThreadStart (ChunkInstancer));
		thread.IsBackground = true;
		thread.Start ();*/


		/*if (UserSession.IsNewGame) 
		{*/
			CreateNewGame ();
			/*}*/
		//UnityThreadHelper.CreateThread (ChunkInstancer, true);
	}

	void Start() 
	{
	
	}
	
	void Update () 
	{

	}

	void CreateNewGame()
	{
		// Instantiate first chunk
		InstantiateChunk (0, 0);

		// Instantiate player
		InstantiatePlayerOnChunk (0, 0);
	}

	public void InstantiateChunk(int x, int y)
	{
		//UnityThreadHelper.Dispatcher.Dispatch (() => {
			GameObject chunk = (GameObject)GameObject.Instantiate (Resources.Load (Settings.GetChunkPrefabName ()));
			Chunk chunkObject = chunk.GetComponent<Chunk> ();
			chunkObject.worldManager = this;
			chunkObject.SetGridPosition (x, y);

			chunks.Add (chunkObject.gridPosition.ToString (), chunkObject);
		//}); 
	}

	public void InstantiatePlayerOnChunk(int x, int y)
	{
		GameObject player = (GameObject)GameObject.Instantiate (Resources.Load (Settings.GetPlayerPrefabName ()));
		GameObject camera = (GameObject)GameObject.Instantiate (Resources.Load (Settings.GetMainCameraPrefabName ()));
		this.cameraObject = camera;
		Chunk chunk = GetChunk (x, y);

		Player playerObject = player.GetComponent<Player> ();
		playerObject.managerReference = this;
		playerObject.guiManager = this.guiManager;
		playerObject.audioManager = this.audioManager;
		this.player = playerObject;
		guiManager.player = this.player;
		playerObject.SetCurrentHostingChunk(chunk);

		player.transform.position = new Vector3(player.transform.position.x, GetYAxisForPlayer(), player.transform.position.z);
		
		FollowTarget cmrObj = camera.GetComponent<FollowTarget> ();
		cmrObj.target = playerObject.target;
	}

	public void InstantiatePoop()
	{
		GameObject poop = (GameObject)GameObject.Instantiate (Resources.Load (Settings.GetPoopPrefabName ()));
		poop.transform.position = new Vector3(player.transform.position.x, player.CurrentHostingChunk.A.y, player.transform.position.z);
		poop.transform.localScale = new Vector3 (poop.transform.localScale.x + player.AdditionalScale / 10f, poop.transform.localScale.y + player.AdditionalScale / 10f, poop.transform.localScale.z + player.AdditionalScale / 10f);
	}

	public Chunk GetChunk(int x, int y)
	{
		string key = ChunkGridPosition.GenerateKey (x, y);
		if (this.chunks.ContainsKey (key)) {
			return this.chunks [key];
		} else {
			return null;
		}
	}

	public void CheckChunkNeighbors(Chunk chunk)
	{
		Chunk tmp = null;

		for (int i = 1; i <= 1; i++) {
			// Check for north chunk
			tmp = GetChunk (chunk.gridPosition.X, chunk.gridPosition.Y + (1 * i));
			if (tmp == null) {
				InstantiateChunk (chunk.gridPosition.X, chunk.gridPosition.Y + (1 * i));
			}
			tmp = null;

			// Check for north-east chunk
			tmp = GetChunk (chunk.gridPosition.X + (1 * i), chunk.gridPosition.Y + (1 * i));
			if (tmp == null) {
				InstantiateChunk (chunk.gridPosition.X + (1 * i), chunk.gridPosition.Y + (1 * i));
			}
			tmp = null;

			// Check for east chunk
			tmp = GetChunk (chunk.gridPosition.X + (1 * i), chunk.gridPosition.Y);
			if (tmp == null) {
				InstantiateChunk (chunk.gridPosition.X + (1 * i), chunk.gridPosition.Y);
			}
			tmp = null;

			// Check for south-east chunk
			tmp = GetChunk (chunk.gridPosition.X + (1 * i), chunk.gridPosition.Y - (1 * i));
			if (tmp == null) {
				InstantiateChunk (chunk.gridPosition.X + (1 * i), chunk.gridPosition.Y - (1 * i));
			}
			tmp = null;

			// Check for south chunk
			tmp = GetChunk (chunk.gridPosition.X, chunk.gridPosition.Y - (1 * i));
			if (tmp == null) {
				InstantiateChunk (chunk.gridPosition.X, chunk.gridPosition.Y - (1 * i));
			}
			tmp = null;

			// Check for south-west chunk
			tmp = GetChunk (chunk.gridPosition.X - (1 * i), chunk.gridPosition.Y - (1 * i));
			if (tmp == null) {
				InstantiateChunk (chunk.gridPosition.X - (1 * i), chunk.gridPosition.Y - (1 * i));
			}
			tmp = null;

			// Check for west chunk
			tmp = GetChunk (chunk.gridPosition.X - (1 * i), chunk.gridPosition.Y);
			if (tmp == null) {
				InstantiateChunk (chunk.gridPosition.X - (1 * i), chunk.gridPosition.Y);
			}
			tmp = null;

			// Check for north-west chunk
			tmp = GetChunk (chunk.gridPosition.X - (1 * i), chunk.gridPosition.Y + (1 * i));
			if (tmp == null) {
				InstantiateChunk (chunk.gridPosition.X - (1 * i), chunk.gridPosition.Y + (1 * i));
			}
			tmp = null;
		}
		/*
		tmp = GetChunk (chunk.gridPosition.X + 1, chunk.gridPosition.Y + 2);
		if (tmp == null) {
			InstantiateChunk (chunk.gridPosition.X + 1, chunk.gridPosition.Y + 2);
		}
		tmp = null;

		tmp = GetChunk (chunk.gridPosition.X - 1, chunk.gridPosition.Y + 2);
		if (tmp == null) {
			InstantiateChunk (chunk.gridPosition.X - 1, chunk.gridPosition.Y + 2);
		}
		tmp = null;

		tmp = GetChunk (chunk.gridPosition.X + 2, chunk.gridPosition.Y + 1);
		if (tmp == null) {
			InstantiateChunk (chunk.gridPosition.X + 2, chunk.gridPosition.Y + 1);
		}
		tmp = null;

		tmp = GetChunk (chunk.gridPosition.X - 2, chunk.gridPosition.Y + 1);
		if (tmp == null) {
			InstantiateChunk (chunk.gridPosition.X - 2, chunk.gridPosition.Y + 1);
		}
		tmp = null;


		tmp = GetChunk (chunk.gridPosition.X - 1, chunk.gridPosition.Y - 2);
		if (tmp == null) {
			InstantiateChunk (chunk.gridPosition.X - 1, chunk.gridPosition.Y - 2);
		}
		tmp = null;

		tmp = GetChunk (chunk.gridPosition.X - 2, chunk.gridPosition.Y - 1);
		if (tmp == null) {
			InstantiateChunk (chunk.gridPosition.X - 2, chunk.gridPosition.Y - 1);
		}
		tmp = null;

		tmp = GetChunk (chunk.gridPosition.X + 2, chunk.gridPosition.Y - 1);
		if (tmp == null) {
			InstantiateChunk (chunk.gridPosition.X + 2, chunk.gridPosition.Y - 1);
		}
		tmp = null;

		tmp = GetChunk (chunk.gridPosition.X + 1, chunk.gridPosition.Y - 2);
		if (tmp == null) {
			InstantiateChunk (chunk.gridPosition.X + 1, chunk.gridPosition.Y - 2);
		}
		tmp = null;*/
	}

	public float GetYAxisForPlayer()
	{
		return Utility.GetHighestPointOnMesh(player.CurrentHostingChunk);
	}
}
