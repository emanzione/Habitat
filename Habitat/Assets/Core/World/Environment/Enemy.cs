using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour 
{
	public Player player;
	// Use this for initialization
	void Start () {
		if (!rigid)
		{
			rigid = GetComponent<Rigidbody>();
		}

		rigid.mass *= transform.localScale.y;
	}

	float LookAtThreshold = 4.5f;
	float AttackingRange = 3f;

	public int CaloriesGained = 10;

	public int DNAPointsGained = 1;

	// Update is called once per frame
	void Update () 
	{
		float distance = DistanceFromPlayer ();
		if (distance <= LookAtThreshold) {
			//LookAtPlayer ();
			if (distance <= AttackingRange) {
				if (IsEnemyBigEnough ()) {
					MoveToPlayer ();
				} 
				else if (!IsEnemyBigEnough() && !IsPlayerBigEnough()) {
					RandomWalk();
				}
				else {
					RunAway ();
				}
			}

			if (player.storylineSteps ["FIRST_ENEMY_SPOTTED"] == false) {
				player.guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("FIRST_ENEMY_SPOTTED_01"), 8f, true, true);
				player.guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("FIRST_ENEMY_SPOTTED_02"), 8f, true, true);
				player.guiManager.Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("FIRST_ENEMY_SPOTTED_03"), 8f, true, true, new System.Action (player.guiManager.EnableDNABar));
				player.storylineSteps ["FIRST_ENEMY_SPOTTED"] = true;
			}
		} else {
			RandomWalk ();
		}
	}

	protected float DistanceFromPlayer()
	{
		return (transform.position - player.transform.position).sqrMagnitude;
	}

	public float walkspeed = 0.5f;
	public float acceleration = 1f;
	private float vel;

	void MoveToPlayer()
	{
		if (player.CanMove) {
			/*float vel = walkspeed * Time.fixedDeltaTime;
			transform.position = Vector3.MoveTowards (transform.position, player.transform.position, vel);*/
			Move (player.transform.position);
		}
	}

	private Rigidbody rigid;
	void Move(Vector3 pos)
	{
		Vector3 dir = (transform.position - pos);
		dir.y = 0; 
		dir = dir.normalized;

		//transform.rotation = Quaternion.LookRotation (dir);

		vel = Mathf.MoveTowards (vel, walkspeed*dir.sqrMagnitude, acceleration*Time.fixedDeltaTime);
		if (!rigid)
		{
			rigid = GetComponent<Rigidbody>();
		}

		rigid.maxAngularVelocity = walkspeed / this.transform.localScale.y;



		/*Vector3 cameraF, cameraR;
		cameraF = -new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
		cameraF.Normalize();
		cameraR = new Vector3(Camera.main.transform.right.x, 0, Camera.main.transform.right.z);
		cameraR.Normalize();*/

		rigid.AddTorque(new Vector3(-dir.z, 0 , dir.x) * vel * walkspeed,ForceMode.Force);
		//rigid.AddTorque(transform.right * vel * walkspeed,ForceMode.Force);
	}

	void MoveTo(Vector3 pos)
	{
		if (player.CanMove) {
			/*float vel = walkspeed * Time.fixedDeltaTime;
			transform.position = Vector3.MoveTowards (transform.position, pos, vel);*/
			Move (pos);
		}
	}

	public Vector3 runPosition;
	void RunAway()
	{
		if (runPosition == Vector3.zero)
			runPosition = transform.position;
		if ((runPosition - transform.position).sqrMagnitude <= 0.1f) {
			runPosition = GetRunAwayPosition ();
		}

		if ((runPosition - transform.position).sqrMagnitude > 0.1f) {
			MoveTo (runPosition);
		}
	}

	public Vector3 randomPosition;
	public DateTime lastMovement = DateTime.UtcNow;
	public float randomDelay = (float)Randomizer.Range(Settings.MIN_MOVEMENTS_DELAY, Settings.MAX_MOVEMENTS_DELAY);
	void RandomWalk()
	{
		if (randomPosition == Vector3.zero)
			randomPosition = transform.position;
		
		if ((randomPosition - transform.position).sqrMagnitude <= 0.1f) {
			if(lastMovement.AddSeconds(randomDelay) <= DateTime.UtcNow)
			{
				randomPosition = GetRandomPosition (transform.transform.position);
				randomDelay = (float)Randomizer.Range(Settings.MIN_MOVEMENTS_DELAY, Settings.MAX_MOVEMENTS_DELAY);
				lastMovement = DateTime.UtcNow;
			}
		}

		if ((randomPosition - transform.position).sqrMagnitude > 0.1f) {
			MoveTo (randomPosition);
		}
	}

	protected Vector3 GetRunAwayPosition()
	{
		return this.transform.position + ((this.transform.position - player.transform.position).normalized);
	}

	protected Vector3 GetRandomPosition(Vector3 current)
	{
		Vector2 circle = UnityEngine.Random.insideUnitCircle;
		return new Vector3 (current.x + circle.x, this.player.CurrentHostingChunk.A.y, current.y + circle.y);
	}

	protected void LookAtPlayer()
	{
		Vector3 heading = player.transform.position - transform.position;
		heading.y = 0;
		if (heading == Vector3.zero)
			return;
		Quaternion targetRotation = Quaternion.LookRotation(heading);
		Vector3 vTargetRotation = targetRotation.eulerAngles;

		// Smoothly rotate towards the target point.
		//transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * Time.deltaTime);
		transform.eulerAngles = new Vector3(transform.eulerAngles.x, vTargetRotation.y, transform.eulerAngles.z);
	}

	public bool IsPlayerBigEnough()
	{
		return (player.transform.localScale.y > (this.transform.localScale.y + ((this.transform.localScale.y * 5f) / 100f)));
	}

	public bool IsEnemyBigEnough()
	{
		return (player.transform.localScale.y < (this.transform.localScale.y - ((this.transform.localScale.y * 5f) / 100f)));
	}

	public void Fight(Player agent)
	{
		if (IsPlayerBigEnough()) {
			agent.DefeatEnemy (this);
			this.gameObject.SetActive (false);
		}

		if (IsEnemyBigEnough()) {
			agent.GameOver (this);
		}
	}
}
