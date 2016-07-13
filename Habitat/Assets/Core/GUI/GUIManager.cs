using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class GUIManager : MonoBehaviour
{
	public Player player;
	public AudioSource audioSource;

	void Awake()
	{
		audioSource = GetComponent<AudioSource> ();
	}

	void Start() 
	{
	
	}

	void Update() 
	{
		if(scrollView != null)
			CheckNarration();

		if (Menu != null && Input.GetKeyDown (KeyCode.Escape))
			MenuToggle ();
	}


	/*
	 * First screen management
	 */
	public RectTransform FirstScreen;
	public void OnStartButton_click()
	{
		if (FirstScreen != null) 
		{
			FirstScreen.gameObject.SetActive(false);
			NewGame.gameObject.SetActive(true);
		}
	}
	/*
	 * End first screen management
	 */

	/*
	* NewGame screen management
	*/
	public RectTransform NewGame;
	public InputField Username;
	public void OnNewGameButton_click()
	{
		if (NewGame != null) 
		{
			NewGame.gameObject.SetActive(false);
			UserSession.StartNewSession(Username.text);
		}
	}
	/*
	 * End first screen management
	 */

	/*
	* Narration screen management
	*/
	public RectTransform scrollView;
	public ScrollRect scrollViewComponent;
	public Text textContent;
	public Text textSpeaker;
	protected Narration currentNarration;
	protected Queue<Narration> narrations = new Queue<Narration>();

	public void Narrate(string who, string text, float seconds, bool disappearWhenOk, bool freezePlayer = false, Action callback = null)
	{
		narrations.Enqueue (new Narration (who, text, seconds, disappearWhenOk, freezePlayer, callback));
	}

	private void CheckNarration()
	{
		if (scrollViewComponent == null)
			scrollViewComponent = scrollView.GetComponent<ScrollRect> ();
		if (currentNarration == null) {
			if (narrations.Count > 0) {
				currentNarration = narrations.Dequeue();
				ActivateNarration(currentNarration);
			}
		} else {
			if (!currentNarration.DisappearIfEnterPressed && DateTime.UtcNow > currentNarration.Expiration) {
				RemoveNarration ();
			}
			if (Input.GetKeyUp(KeyCode.Return)/* || Input.GetMouseButtonUp(1)*/)
				RemoveNarration ();
		}

		CheckNarrationGUI();
	}

	protected void CheckNarrationGUI()
	{
		if (currentNarration == null) {
			if(scrollView.gameObject.activeSelf != false)
				scrollView.gameObject.SetActive(false);
		} else {
			if(scrollView.gameObject.activeSelf != true)
				scrollView.gameObject.SetActive(true);
		}
	}

	public void RemoveNarration()
	{
		player.CanMove = true;
		currentNarration = null;
	}

	public void ActivateNarration(Narration narration)
	{
		textSpeaker.text = narration.Who;
		textContent.text = narration.Text;
		player.CanMove = !narration.FreezePlayer;
		audioSource.Play ();
		scrollViewComponent.verticalNormalizedPosition = 1f;
		narration.Activate();
	}
	/*
	 * End narration screen management
	 */

	/*
	* Bars management
	*/
	public UIBarScript stomach;
	public CaloriesManager calories;
	public DNAManager dna;

	public void EnableStomachBar()
	{
		stomach.gameObject.SetActive(true);
		SetStomachBar((int)player.CurrentStomachContent, (int)player.MaxStomachCapacity);
	}

	public void EnableCaloriesBar()
	{
		calories.gameObject.SetActive(true);
		SetCalories((int)player.TotalCaloriesInStomach);
	}

	public void EnableDNABar()
	{
		dna.gameObject.SetActive(true);
		SetDNA(player.dnaPoints);
	}

	public void SetStomachBar(int value, int max)
	{
		stomach.UpdateValue (value, max);
	}

	public void SetCalories(int calories)
	{
		this.calories.SetCalories (calories);
	}

	public void SetDNA(int dna)
	{
		this.dna.SetPoint (dna);
	}
	/*
	* End bars management
	*/

	/*
	* GameOver screen management
	*/
	public RectTransform GameOver;
	public Text GameOverScore;

	public void GameOverTrigger()
	{
		GameOver.gameObject.SetActive (true);
		GameOverScore.text = "Your score: " + player.dnaPoints + "/" + Settings.DNA_POINTS_TO_WIN;
	}

	public void OnGameOverButton_click()
	{
		if (GameOver != null) 
		{
			GameOver.gameObject.SetActive(false);
			UserSession.StartNewSession("");
		}
	}
	/*
	 * End game over screen management
	 */

	/*
	* GameCompleted screen management
	*/
	public RectTransform GameCompleted;
	public RectTransform GameCompletedButDead;
	public Text FinalScore;
	public Text FinalScoreButDead;
	public void GameCompletedTrigger()
	{
		GameCompleted.gameObject.SetActive (true);
		FinalScore.text = "Eaten food: " + UserSession.EatenFood + "\n" +
			"Eaten grass: " + UserSession.EatenGrass + "\n" +
			"Eaten mushrooms: " + UserSession.EatenMushroom + "\n" +
			"Eaten poisoned mushrooms: " + UserSession.EatenPoisonedMushroom + "\n" +
			"Eaten special mushrooms: " + UserSession.EatenSpecialMushroom + "\n" +
			"Eaten enemies: " + UserSession.EatenEnemies;
	}

	public void GameCompletedButDeadTrigger()
	{
		GameCompletedButDead.gameObject.SetActive (true);
		FinalScoreButDead.text = "Eaten food: " + UserSession.EatenFood + "\n" +
			"Eaten grass: " + UserSession.EatenGrass + "\n" +
			"Eaten mushrooms: " + UserSession.EatenMushroom + "\n" +
			"Eaten poisoned mushrooms: " + UserSession.EatenPoisonedMushroom + "\n" +
			"Eaten special mushrooms: " + UserSession.EatenSpecialMushroom + "\n" +
			"Eaten enemies: " + UserSession.EatenEnemies;
	}

	public void OnRestartGameButton_click()
	{
		GameCompletedButDead.gameObject.SetActive(false);
		UserSession.StartNewSession("");
	}

	public void OnGameCompletedButton_click()
	{
		if (GameCompleted != null) 
		{
			GameCompleted.gameObject.SetActive(false);
		}
	}
	/*
	 * End game completed screen management
	 */

	/*
	* Menu screen management
	*/
	public RectTransform Menu;

	public void MenuToggle()
	{
		Menu.gameObject.SetActive (!Menu.gameObject.activeSelf);
		if (Menu.gameObject.activeSelf == true) {
			if (player.storylineSteps ["FIRST_PAUSE"] == false) {
				Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("FIRST_PAUSE"), 8f, true, true);
				player.storylineSteps ["FIRST_PAUSE"] = true;
			} else {
				Narrate (Narration.NARRATOR_NAME, Narration.GetNarration ("PAUSE_" + ((int)Randomizer.Range (1f, 3f)).ToString ()), 8f, true, true);
			}
			player.CanMove = false;
		}
		else
			player.CanMove = true;
	}

	public void OnQuitButton_click()
	{
		Application.Quit ();
	}
	/*
	 * End menu screen management
	 */
}
