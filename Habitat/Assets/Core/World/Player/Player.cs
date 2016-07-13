using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour 
{
	Chunk currentHostingChunk = null;
	//Chunk oldHostingChunk = null;

	public WorldManager managerReference;
	public GUIManager guiManager;
	public AudioManager audioManager;

	public bool CanMove = true;
	public Transform target;

	public Chunk CurrentHostingChunk
	{
		get{ return currentHostingChunk; }
		private set{ }
	}

	public void SetCurrentHostingChunk(Chunk chunk)
	{
		//oldHostingChunk = currentHostingChunk;
		if(currentHostingChunk != null)
			currentHostingChunk.IsPlayerContained = false;
		currentHostingChunk = chunk;
		if(currentHostingChunk != null)
			currentHostingChunk.IsPlayerContained = true;
		
		//WorldManager.chunkInstancerEvent.Set();
		if(CurrentHostingChunk != null)
			managerReference.CheckChunkNeighbors (CurrentHostingChunk);
	}

	public float walkspeed = 1f;
	public float acceleration = 1f;
	private float vel;
	private Vector2 axis;
	//private Vector2 axis;

	public float AdditionalScale = 0f;

	public Dictionary<string, bool> storylineSteps = new Dictionary<string, bool> () {
		{"INTRODUCTION_DONE", false},
		{"FIRST_FOOD_EAT", false},
		{"FIRST_POISONED_FOOD_EAT", false},
		{"FIRST_ENEMY_SPOTTED", false},
		{"FIRST_ENEMY_EATEN", false},
		{"GAME_COMPLETED", false},
		{"FIRST_PAUSE", false},
	};

	public float MaxStomachCapacity = 100f;
	public float CurrentStomachContent = 0f;
	public float TotalCaloriesInStomach = 0f;

	public float PoopCounter = 0f;
	public float PoopThreshold = (float)Randomizer.Range(Settings.MIN_CALORIES_NEEDED_FOR_POO, Settings.MAX_CALORIES_NEEDED_FOR_POO);

	public float GrowCounter = 0f;
	public float GrowThreshold = 1000f;
	public float GrowScaleValue = 0.1f;

	public int dnaPoints = 0;
	
	/*void Move()
	{
		axis.Set (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		Vector3 dir = (transform.forward * axis.y + transform.right * axis.x).normalized;
		vel = Mathf.MoveTowards (vel, walkspeed*dir.sqrMagnitude, acceleration*Time.fixedDeltaTime);
		transform.position += dir * vel * Time.fixedDeltaTime;
	}*/

	/*void Move()
	{
		axis.Set (0, 0f, -(Input.GetAxis ("Vertical")));
		Vector3 dir = (transform.forward * axis.z + transform.right * axis.x).normalized;
		dir = transform.TransformVector (dir).normalized;
		vel = Mathf.MoveTowards (vel, walkspeed*dir.sqrMagnitude, acceleration*Time.fixedDeltaTime);
		transform.position += dir * vel * Time.fixedDeltaTime;
	}*/

	private Rigidbody rigid;
	void Move()
	{
		axis.Set (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"));
		Vector3 dir = (Camera.main.transform.forward * axis.y + Camera.main.transform.right * axis.x);
		dir.y = 0; 
		dir = dir.normalized;
		vel = Mathf.MoveTowards (vel, walkspeed*dir.sqrMagnitude, acceleration*Time.fixedDeltaTime);
		if (!rigid)
		{
			rigid = GetComponent<Rigidbody>();
		}

		rigid.maxAngularVelocity = 25f / this.transform.localScale.y;

		Vector3 cameraF, cameraR;
		cameraF = -new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
		cameraF.Normalize();
		cameraR = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
		cameraR.Normalize();



		rigid.AddTorque(cameraF * axis.x * vel * walkspeed,ForceMode.Force);
		rigid.AddTorque(cameraR * axis.y * vel * walkspeed,ForceMode.Force);
	}

	void Awake()
	{
		if (target == null)
			this.target = transform;
	}

	void Start(){
		if (storylineSteps ["INTRODUCTION_DONE"] == false) {
			guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("INTRODUCTION_01"), 8f, true, true);
			guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("INTRODUCTION_02"), 8f, true, true);
			guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("INTRODUCTION_03"), 8f, true, true);
			guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("INTRODUCTION_04"), 8f, true, true);
			guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("INTRODUCTION_05"), 8f, true, true, new Action(guiManager.EnableStomachBar));
			guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("INTRODUCTION_06"), 8f, true, true, new Action(guiManager.EnableCaloriesBar));
			storylineSteps ["INTRODUCTION_DONE"] = true;
		}
	}

	private DateTime lastUpdate = DateTime.UtcNow.AddSeconds(-1);
	void Update()
	{
		if (CanMove && lastUpdate.AddSeconds (1) <= DateTime.UtcNow) {
			Consume ();
			lastUpdate = DateTime.UtcNow;
		}

		if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}
	}

	void FixedUpdate() 
	{ 
		if(CanMove)
			Move();

		/*if (Input.GetKeyDown (KeyCode.Space)) {
			Jump ();
		}*/
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Chunk") {
			Chunk chunk = col.GetComponent<Chunk> ();
			if (chunk != null) {
				managerReference.CheckChunkNeighbors (chunk);
			}
		}

		if (col.tag == "Food") {
			Food food = col.transform.GetComponent<Food> ();
			food.Consume (this);
		}

		if (col.tag == "Enemy") {
			Enemy enemy = col.transform.GetComponent<Enemy> ();
			enemy.Fight (this);
		}
	}

	public bool CanEat(Food food)
	{
		return ((CurrentStomachContent + food.TotalMass) <= MaxStomachCapacity);
	}

	public void SetStomachContent()
	{
		guiManager.SetStomachBar ((int)CurrentStomachContent, (int)MaxStomachCapacity);
	}

	public void SetCalories()
	{
		guiManager.SetCalories ((int)TotalCaloriesInStomach);
	}

	public void Eat(Food food, float mass, float calories)
	{
		CurrentStomachContent += mass;
		TotalCaloriesInStomach += calories;
		this.audioManager.Play(this.audioManager.pickup);
		SetStomachContent ();
		if (storylineSteps ["FIRST_FOOD_EAT"] == false) {
			guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("FIRST_FOOD_EAT"), 8f, true, true);
			storylineSteps ["FIRST_FOOD_EAT"] = true;
		}

		if (storylineSteps ["FIRST_POISONED_FOOD_EAT"] == false) {
			if (food.GetType () == typeof(PoisonMushroom)) {
				guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("FIRST_POISONED_FOOD_EAT"), 8f, true, true);
				storylineSteps ["FIRST_POISONED_FOOD_EAT"] = true;
			}
		}
	}

	protected void Consume()
	{
		float onePercent = (MaxStomachCapacity) / 100f;
		float con = (onePercent * TotalCaloriesInStomach) / MaxStomachCapacity;

		if (onePercent < 1f)
			onePercent = 1f;
		if (con < 1f)
			con = 1f;

		if (CurrentStomachContent > 0f) {
			CurrentStomachContent -= onePercent;
			SetStomachContent ();
		}
		if (TotalCaloriesInStomach > 0f) {
			TotalCaloriesInStomach -= con;
			PoopCounter += con;
			GrowCounter += con;
			CheckForPoop ();
			CheckForGrow ();
			SetCalories ();
		}
	}

	public void CheckForPoop()
	{
		if (PoopCounter >= PoopThreshold) {
			Defecate ();
			PoopCounter = 0f;
		}
	}

	public void Defecate()
	{
		this.audioManager.Play (this.audioManager.poo);
		this.managerReference.InstantiatePoop ();
		PoopThreshold = (float)Randomizer.Range(Settings.MIN_CALORIES_NEEDED_FOR_POO, Settings.MAX_CALORIES_NEEDED_FOR_POO);
	}

	public void CheckForGrow()
	{
		if (GrowCounter >= GrowThreshold) {
			Grow ();
			GrowCounter = 0f;
		}
	}

	public void Grow()
	{
		AdditionalScale += GrowScaleValue;
		rigid.mass *= (1 + GrowScaleValue);
		transform.localScale = new Vector3 (transform.localScale.x + GrowScaleValue, transform.localScale.y + GrowScaleValue, transform.localScale.z + GrowScaleValue);
		transform.position = new Vector3 (transform.position.x, managerReference.GetYAxisForPlayer(), transform.position.z);
		MaxStomachCapacity += MaxStomachCapacity * GrowScaleValue;
		SetStomachContent ();
	}

	public void DefeatEnemy(Enemy enemy)
	{
		dnaPoints += enemy.DNAPointsGained;
		guiManager.SetDNA (dnaPoints);
		UserSession.EatenEnemies++;
		TotalCaloriesInStomach += (enemy.CaloriesGained * enemy.transform.localScale.y);
		SetCalories ();
		audioManager.Play (audioManager.enemyEaten);
		if (storylineSteps ["FIRST_ENEMY_EATEN"] == false) {
			guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("FIRST_ENEMY_EATEN"), 8f, true, true);
			storylineSteps ["FIRST_ENEMY_EATEN"] = true;
		}
			
		if (dnaPoints >= Settings.DNA_POINTS_TO_WIN && storylineSteps["GAME_COMPLETED"] == false) {
			GameCompleted ();
		}
	}

	public void GameOver(Enemy enemy)
	{
		audioManager.Play (audioManager.playerDeath);
		CanMove = false;
		this.gameObject.SetActive (false);
		if (storylineSteps ["GAME_COMPLETED"] == false) {
			guiManager.GameOverTrigger ();
			guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("GAMEOVER_" + ((int)Randomizer.Range (1f, 6f)).ToString ()), 8f, true, true);
		} else {
			guiManager.GameCompletedButDeadTrigger ();
		}
	}

	public void GameCompleted()
	{
		guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("GAME_COMPLETED_1"), 8f, true, true);
		guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("GAME_COMPLETED_2"), 8f, true, true);
		guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("GAME_COMPLETED_3"), 8f, true, true);
		guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("GAME_COMPLETED_4"), 8f, true, true);
		guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("GAME_COMPLETED_5"), 8f, true, true);
		guiManager.GameCompletedTrigger ();
		storylineSteps ["GAME_COMPLETED"] = true;
	}

	public void Jump()
	{
		if (!rigid)
		{
			rigid = GetComponent<Rigidbody>();
		}

		if(rigid.velocity.y <= 0.2f && rigid.velocity.y >= -0.2f)
			rigid.AddForce (Vector3.up * 3f, ForceMode.VelocityChange);
	}
}
